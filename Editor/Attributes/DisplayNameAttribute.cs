#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JoanofArcGames.PackageConstructor
{
	public class DisplayNameAttribute : PropertyAttribute
	{
		public readonly string name;

		public DisplayNameAttribute(string name)
		{
			this.name = name;
		}
	}

	[CustomPropertyDrawer(typeof(DisplayNameAttribute))]
	public class DisplayNameDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.PropertyField(position, property, new GUIContent(((DisplayNameAttribute)attribute).name), true);
		}
	}
}
#endif