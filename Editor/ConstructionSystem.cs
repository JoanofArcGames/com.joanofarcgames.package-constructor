#if UNITY_EDITOR
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

			if (!ValidateConfig(config))
			{
				Debug.LogError("Aborting construction. Correct errors in config file and try again");
				return;
			}

			ManifestData data = ConfigToManifest(config);
			
			
			Debug.Log("Construction has finished successfully");
		}

		private static void CreateRoot(string name)
		{
			
		}

		private static bool ValidateConfig(ConfigData config)
		{
			bool valid = true;



			return valid;
		}

		private static ManifestData ConfigToManifest(ConfigData config)
		{
			return new ManifestData
			{

			};
		}
	}
}
#endif