#if UNITY_EDITOR
namespace JoanofArcGames.PackageConstructor
{
	public struct ManifestData
	{
		public string name;
		public string version;
		public string displayName;
		public string description;
		public string unity;
		public string unityRelease;
		public string documentationUrl;
		public string changelogUrl;
		public string licensesUrl;
		public string license;
		public bool hideInEditor;

		public Dependency[] dependencies;

		public string[] keywords;

		public Author author;
	}
}
#endif