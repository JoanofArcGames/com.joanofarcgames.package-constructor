#if UNITY_EDITOR
using System.Text.RegularExpressions;
using UnityEngine;

namespace JoanofArcGames.PackageConstructor
{
	public static class ConfigValidationSystem
	{
		private const string PascalCasePattern = @"^(?:[A-Z][a-z0-9]*)+(\.([A-Z][a-z0-9]*)+)*$";
		private const string SemVerPattern = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?(?:\+([0-9a-zA-Z-]+(?:\.[0-9a-zA-Z-]+)*))?$";
		private const string UnityVersionPattern = @"^([1-9]\d{3})\.([1-9]\d{0})$";
		private const string UnityReleasePattern = @"^(\d{0,2})([abf])(\d{0,2})$";

		public static bool Validate(ref ConfigData config)
		{
			bool valid = true;
			string f = "Config validation failed.\n";

			if (!Regex.IsMatch(config.companyName, PascalCasePattern))
			{
				if (config.companyName == "")
				{
					Debug.LogError($"{f}Company name is a required field");
				}
				else
				{
					Debug.LogError($"{f}Company name: \"{config.companyName}\" isn't PascalCase");
				}
				valid = false;
			}
			
			if (!Regex.IsMatch(config.packageName, PascalCasePattern))
			{
				if (config.packageName == "")
				{
					Debug.LogError($"{f}Package name is a required field");
				}
				else
				{
					Debug.LogError($"{f}Package name: \"{config.packageName}\" isn't PascalCase");
				}
				valid = false;
			}
			
			if (!Regex.IsMatch(config.version, SemVerPattern))
			{
				if (config.version == "")
				{
					Debug.LogError($"{f}Version is a required field");
				}
				else
				{
					Debug.LogError($"{f}Package version: \"{config.version}\" is not semver-compatible, which is not allowed");
				}
				valid = false;
			}
			
			if (!Regex.IsMatch(config.unityVersion, UnityVersionPattern))
			{
				if (config.unityVersion != "")
				{
					Debug.LogError($"{f}Provided unity version: \"{config.unityVersion}\" is invalid");
					valid = false;
				}
			}
			
			if (!Regex.IsMatch(config.unityRelease, UnityReleasePattern))
			{
				if (config.unityRelease != "")
				{
					Debug.LogError($"{f}Provided unity release: \"{config.unityRelease}\" is invalid");
					valid = false;
				}
			}
			
			if ((config.author.email != "" || config.author.url != "") && config.author.name == "")
			{
				Debug.LogError($"{f}Author url and/or email are present, but name field is empty, which is not allowed");
				valid = false;
			}

			if (!config.editor && config.testsEditor)
			{
				LogSystem.LogWarning($"Warning: /Editor/ directory is not created, so /Tests/Editor/ directory will not be created");
				config.testsEditor = false;
			}
			
			if (!config.runtime && config.testsRuntime)
			{
				LogSystem.LogWarning($"Warning: /Runtime/ directory is not created, so /Tests/Runtime/ directory will not be created");
				config.testsRuntime = false;
			}

			return valid;
		}
	}
}
#endif