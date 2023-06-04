#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JoanofArcGames.PackageConstructor
{
    public class IndentAttribute : PropertyAttribute
    {
	    public readonly int indent = 0;

	    public IndentAttribute(int indent)
	    {
		    this.indent = indent;
	    }
    }

    [CustomPropertyDrawer(typeof(IndentAttribute))]
    public class IndentDrawer : PropertyDrawer
    {
	    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	    {
		    EditorGUI.indentLevel = ((IndentAttribute)attribute).indent;
		    EditorGUI.PropertyField(position, property, label);
		    EditorGUI.indentLevel = 0;
	    }
    }
}
#endif