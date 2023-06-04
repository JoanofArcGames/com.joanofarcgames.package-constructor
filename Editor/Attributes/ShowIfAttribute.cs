#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JoanofArcGames.PackageConstructor
{
	public class ShowIfAttribute : PropertyAttribute
	{
		public readonly string condition;
		public readonly bool invert;

		public ShowIfAttribute(string condition)
		{
			this.condition = condition;
			this.invert = false;
		}

		public ShowIfAttribute(string condition, bool invert)
		{
			this.condition = condition;
			this.invert = invert;
		}
	}

	[CustomPropertyDrawer(typeof(ShowIfAttribute))]
	public class ConditionalPropertyDrawer : PropertyDrawer 
	{
		private bool ShouldShow(SerializedProperty property)
		{
			var showIfAttribute = (ShowIfAttribute)attribute;
			string conditionPath = showIfAttribute.condition;
			bool invert = showIfAttribute.invert;
		    
			string thisPropertyPath = property.propertyPath;
			int last = thisPropertyPath.LastIndexOf('.');
			if (last > 0)
			{            
				string containerPath = thisPropertyPath[..(last + 1)];
				conditionPath = containerPath + conditionPath;
			}
		    
			var conditionProperty = property.serializedObject.FindProperty(conditionPath);

			if (conditionProperty == null || conditionProperty.type != "bool")
			{
				return true;
			}

			bool theValue = conditionProperty.boolValue;
			if (invert)
			{
				theValue = !theValue;
			}
			return theValue;
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			if (ShouldShow(property))
			{
				EditorGUI.PropertyField(position, property, label, true);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
		{
			if (ShouldShow(property)) 
			{
				return EditorGUI.GetPropertyHeight(property, label, true);
			}
			return -EditorGUIUtility.standardVerticalSpacing;
		}
	}
}
#endif