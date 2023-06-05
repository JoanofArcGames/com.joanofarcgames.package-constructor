#if UNITY_EDITOR
using System;

namespace JoanofArcGames.PackageConstructor
{
	[Serializable]
	public struct Dependency
	{
		public string name;
		public string version;
	}
}
#endif