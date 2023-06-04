#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JoanofArcGames.PackageConstructor
{
	public class HelpBoxAttribute : PropertyAttribute
	{
		public readonly string text;
		public readonly MessageType messageType;

		public HelpBoxAttribute(string text, MessageType messageType)
		{
			this.text = text;
			this.messageType = messageType;
		}
	}

	[CustomPropertyDrawer(typeof(HelpBoxAttribute))]
	public class HelpBoxDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			HelpBoxAttribute attr = (HelpBoxAttribute)attribute;
			EditorGUILayout.HelpBox(attr.text, attr.messageType);
		    EditorGUILayout.PropertyField(property, label);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return -2f;
		}
	}
}
#endif