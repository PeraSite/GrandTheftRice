using UnityEditor;
using UnityEngine;

namespace PeraCore.Editor {
	[CustomPropertyDrawer(typeof(Optional<>))]
	public class OptionalPropertyDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			var valueProperty = property.FindPropertyRelative("value");
			var enabledProperty = property.FindPropertyRelative("enabled");

			const int checkBoxWidth = 24;

			position.width -= checkBoxWidth;
			EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
			{
				EditorGUI.PropertyField(position, valueProperty, label, true);
			}
			EditorGUI.EndDisabledGroup();

			position.x += position.width + checkBoxWidth;
			position.width = position.height = EditorGUI.GetPropertyHeight(enabledProperty);
			position.x -= position.width;
			EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			var valueProperty = property.FindPropertyRelative("value");
			return EditorGUI.GetPropertyHeight(valueProperty);
		}
	}
}
