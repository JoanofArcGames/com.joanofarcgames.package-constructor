#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace JoanofArcGames.PackageConstructor
{
	public class ButtonInvokeAttribute : PropertyAttribute
	{
		public readonly string customLabel;
		public readonly string method;
		public readonly object methodParameter;
		public readonly string passDataMethod;
		public readonly Type type;
		public readonly Type passDataType;

		public ButtonInvokeAttribute(Type type, string method, object methodParameter = null, Type passDataType = null, string passDataMethod = "", string customLabel = "")
		{
			this.type = type;
			this.method = method;
			this.methodParameter = methodParameter;
			this.customLabel = customLabel;
			this.passDataMethod = passDataMethod;
			this.passDataType = passDataType;
		}
	}
	
	[CustomPropertyDrawer(typeof(ButtonInvokeAttribute))]
	public class ButtonInvokeDrawer : PropertyDrawer
	{
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			ButtonInvokeAttribute settings = (ButtonInvokeAttribute) attribute;
			string buttonLabel = !string.IsNullOrEmpty(settings.customLabel) ? settings.customLabel : label.text;
			Object target = property.serializedObject.targetObject;
			
			if (GUI.Button(position, buttonLabel))
			{
				ConfigData config = (ConfigData)settings.passDataType?.GetMethod(settings.passDataMethod)?.Invoke(target, new object[]{});
				settings.type.GetMethod(settings.method)?.Invoke(target, new object[]{ config });
			}
		}
	}
}
#endif