#if UNITY_EDITOR
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace JoanofArcGames.PackageConstructor
{
	public static class ConstructionSystem
	{
		public static void Construct(ConfigData config)
		{
			LogSystem.enabled = config.enableLogging;
			LogSystem.Log("Started construction");

			if (!ConfigValidationSystem.Validate(ref config))
			{
				Debug.LogError("Fatal: config validation failed.\nCorrect config file and try again.\nAborting construction");
				return;
			}

			ManifestData data = ConfigToManifest(ref config);
			string projectRoot = Directory.GetParent(Application.dataPath)!.FullName;
			string templatesPath = Path.Combine(projectRoot, "Packages", "com.joanofarcgames.package-constructor", "Editor", "Templates");
			
			if (!CreateRoot(projectRoot, data.name, out string root))
			{
				Debug.LogError($"Fatal: package with constructed name: {data.name} already exists.\nAborting construction");
				return;
			}
			
			string testsPath = Path.Combine(root, "Tests");

			CreateDirectoryLayout(root, testsPath, ref config);
			CreateSamplesDirectory(root, ref config);
			CreateTemplateDocumentation(root, templatesPath, ref config);
			CreateBlankScripts(root, testsPath, ref config);
			CreateAsmdefs(root, templatesPath, ref config);
			CreatePackageManifest(root, ref data);
			CreateMarkdownFiles(root, templatesPath, ref config);
			
			AssetDatabase.Refresh();
			UnityEditor.PackageManager.Client.Resolve();
			
			LogSystem.Log("Construction has finished successfully");
		}

		private static void CreateMarkdownFiles(string root, string templatesPath, ref ConfigData config)
		{
			if (config.readmeMD) File.Copy(Path.Combine(templatesPath, "template-readme.md"), Path.Combine(root, "README.md"));
			if (config.changelogMD) File.Copy(Path.Combine(templatesPath, "template-changelog.md"), Path.Combine(root, "CHANGELOG.md"));
			if (config.licenseMD) File.Copy(Path.Combine(templatesPath, "template-license.md"), Path.Combine(root, "LICENSE.md"));
			if (config.thirdpartyMD) File.Copy(Path.Combine(templatesPath, "template-thirdparty.md"), Path.Combine(root, "Third Party Notices.md"));
		}

		private static void CreatePackageManifest(string root, ref ManifestData data)
		{
			string manifest = ManifestSerializationSystem.GetJsonString(data);
			File.WriteAllText(Path.Combine(root, "package.json"), manifest);
		}

		private static void CreateSamplesDirectory(string root, ref ConfigData config)
		{
			if (config.samples) Directory.CreateDirectory(Path.Combine(root, "Samples~"));
		}

		private static void CreateAsmdefs(string root, string templatesPath, ref ConfigData config)
		{
			if (config.editor)
			{
				string file = File.ReadAllText(Path.Combine(templatesPath, "editor-asmdef", "editor.asmdef"));
				string name = $"{config.companyName}.{config.packageName}.Editor";
				file = file.Replace("editor", name);
				File.WriteAllText(Path.Combine(root, "Editor", $"{name}.asmdef"), file);
			}
			if (config.runtime)
			{
				string file = File.ReadAllText(Path.Combine(templatesPath, "runtime-asmdef", "runtime.asmdef"));
				string name = $"{config.companyName}.{config.packageName}";
				file = file.Replace("runtime", name);
				File.WriteAllText(Path.Combine(root, "Runtime", $"{name}.asmdef"), file);
			}
			if (config.testsEditor)
			{
				string file = File.ReadAllText(Path.Combine(templatesPath, "tests-editor-asmdef", "tests-editor.asmdef"));
				string name = $"{config.companyName}.{config.packageName}.Editor.Tests";
				file = file.Replace("tests-editor", name);
				File.WriteAllText(Path.Combine(root, "Tests", "Editor", $"{name}.asmdef"), file);
			}
			if (config.testsRuntime)
			{
				string file = File.ReadAllText(Path.Combine(templatesPath, "tests-runtime-asmdef", "tests-runtime.asmdef"));
				string name = $"{config.companyName}.{config.packageName}.Tests";
				file = file.Replace("tests-runtime", name);
				File.WriteAllText(Path.Combine(root, "Tests", "Runtime", $"{name}.asmdef"), file);
			}
		}

		private static void CreateBlankScripts(string root, string testsPath, ref ConfigData config)
		{
			if (config.blankScripts)
			{
				string dir = Path.Combine(root, "Editor");
				if (Directory.Exists(dir)) File.WriteAllText(Path.Combine(dir, "BlankScript.cs"), "");
				dir = Path.Combine(root, "Runtime");
				if (Directory.Exists(dir)) File.WriteAllText(Path.Combine(dir, "BlankScript.cs"), "");
				dir = Path.Combine(testsPath, "Editor");
				if (Directory.Exists(dir)) File.WriteAllText(Path.Combine(dir, "BlankScript.cs"), "");
				dir = Path.Combine(testsPath, "Runtime");
				if (Directory.Exists(dir)) File.WriteAllText(Path.Combine(dir, "BlankScript.cs"), "");
			}
		}

		private static void CreateTemplateDocumentation(string root, string templatesPath, ref ConfigData config)
		{
			string docsPath = Path.Combine(root, "Documentation~");
			if (config.templateDocs && Directory.Exists(docsPath))
			{
				File.Copy
				(
					Path.Combine(templatesPath, "template-documentation.md"),
					Path.Combine(docsPath, $"{config.packageName}.md")
				);
			}
		}

		private static void CreateDirectoryLayout(string root, string testsPath, ref ConfigData config)
		{
			if (config.editor) Directory.CreateDirectory(Path.Combine(root, "Editor"));
			if (config.runtime) Directory.CreateDirectory(Path.Combine(root, "Runtime"));
			
			if (config.testsEditor) Directory.CreateDirectory(Path.Combine(testsPath, "Editor"));
			if (config.testsRuntime) Directory.CreateDirectory(Path.Combine(testsPath, "Runtime"));

			if (config.samples) Directory.CreateDirectory(Path.Combine(root, "Samples~"));
			if (config.documentation) Directory.CreateDirectory(Path.Combine(root, "Documentation~"));
		}

		private static bool CreateRoot(string projectRoot, string name, out string root)
		{
			string packagesPath = Path.Combine(projectRoot, "Packages");
			root = Path.Combine(packagesPath, name);
			if (Directory.Exists(root))
			{
				return false;
			}
			Directory.CreateDirectory(root);
			return true;
		}

		private static ManifestData ConfigToManifest(ref ConfigData config)
		{
			return new ManifestData
			{
				name = "com." + config.companyName.PascalToKebabCase() + "." + config.packageName.PascalToKebabCase(),
				version = config.version,
				displayName = config.displayName,
				description = config.description,
				unity = config.unityVersion,
				unityRelease = config.unityRelease,
				documentationUrl = config.docsUrl,
				changelogUrl = config.changelogUrl,
				licensesUrl = config.licensesUrl,
				license = config.license,
				hideInEditor = config.hideInEditor,
				dependencies = config.dependencies,
				keywords = config.keywords,
				author = config.author
			};
		}
		
		private static string PascalToKebabCase(this string value)
		{
			string output = Regex.Replace(
					value,
					"(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z0-9])",
					"-$1",
					RegexOptions.Compiled)
				.Trim()
				.ToLower();

			char previous = '\0';
			for (int i = 0; i < output.Length; i++)
			{
				if (output[i] == '-' && previous == '.')
				{
					output = output.Remove(i, 1);
				}
				else
				{
					previous = output[i];
				}
			}

			if (output[^1] == '.')
			{
				output = output.Remove(output.Length - 1, 1);
			}

			return output;
		}
	}
}
#endif