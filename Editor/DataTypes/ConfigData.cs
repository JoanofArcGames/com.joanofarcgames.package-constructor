#if UNITY_EDITOR
namespace JoanofArcGames.PackageConstructor
{
	public struct ConfigData
	{
		public string version;
		public string companyName;
		public string packageName;
		public string displayName;
		public string description;
		public string unityVersion;
		public string unityRelease;
		public string docsUrl;
		public string changelogUrl;
		public string licensesUrl;
		public string license;
		public bool hideInEditor;
		public Dependency[] dependencies;
		public string[] keywords;
		public Author author;
		public bool editor;
		public bool runtime;
		public bool testsEditor;
		public bool testsRuntime;
		public bool samples;
		public bool documentation;
	}
}
#endif