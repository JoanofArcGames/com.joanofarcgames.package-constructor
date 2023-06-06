#if UNITY_EDITOR
using UnityEngine;

namespace JoanofArcGames.PackageConstructor
{
	public static class LogSystem
	{
		public static bool enabled;
		
		public static void Log(string message)
		{
			if (!enabled) return;
			Debug.Log(message);
		}

		public static void LogWarning(string message)
		{
			if (!enabled) return;
			Debug.LogWarning(message);
		}

		public static void LogError(string message)
		{
			if (!enabled) return;
			Debug.LogError(message);
		}
	}
}
#endif