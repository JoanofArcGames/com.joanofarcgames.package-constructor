#if UNITY_EDITOR
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace JoanofArcGames.PackageConstructor
{
	public static class ConstructionSystem
	{
		private const string PascalCasePattern = @"^(?:[A-Z][a-z0-9]*)+(\.([A-Z][a-z0-9]*)+)*$";
		private const string SemVerPattern = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";
		private const string UnityVersionPattern = @"^([1-9]\d{3})\.([1-9]\d{0})$";
		private const string UnityReleasePattern = @"^(\d{0,2})([abf])(\d{0,2})$";
		private const string PackagePattern = @"^([a-z0-9\-_]+)(\.[a-z0-9\-_]+)(\.[a-z0-9\-_]+)+$";
		
		public static void Construct(ConfigData config)
		{
			Debug.Log("Started construction");

			if (!ValidateConfig(ref config))
			{
				Debug.LogError("Fatal: provided config contains invalid data.\nCorrect config file and try again.\nAborting construction");
				return;
			}

			ManifestData data = ConfigToManifest(ref config);
			
			if (!CreateRoot(data.name, out string root))
			{
				Debug.LogError($"Fatal: package with constructed name: {data.name} already exists.\nAborting construction");
				return;
			}

			CreateDirectoryLayout(root, ref config);
			
			Debug.Log("Construction has finished successfully");
		}

		private static void CreateDirectoryLayout(string root, ref ConfigData config)
		{
			if (config.editor) Directory.CreateDirectory(Path.Combine(root, "Editor"));
			if (config.runtime) Directory.CreateDirectory(Path.Combine(root, "Runtime"));

			string tests = Path.Combine(root, "Tests");
			if (config.testsEditor) Directory.CreateDirectory(Path.Combine(tests, "Editor"));
			if (config.testsRuntime) Directory.CreateDirectory(Path.Combine(tests, "Runtime"));

			if (config.samples) Directory.CreateDirectory(Path.Combine(root, "Samples~"));
			if (config.documentation) Directory.CreateDirectory(Path.Combine(root, "Documentation~"));
		}

		private static bool CreateRoot(string name, out string root)
		{
			string packagesPath = Path.Combine(Directory.GetParent(Application.dataPath)!.FullName, "Packages");
			root = Path.Combine(packagesPath, name);
			if (Directory.Exists(root))
			{
				return false;
			}
			Directory.CreateDirectory(root);
			return true;
		}

		private static bool ValidateConfig(ref ConfigData config)
		{
			bool valid = true;



			return valid;
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