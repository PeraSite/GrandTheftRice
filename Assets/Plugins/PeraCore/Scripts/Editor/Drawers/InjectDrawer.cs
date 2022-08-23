using System;
using System.IO;
using System.Reflection;
using DG.DemiEditor;
using PeraCore.Runtime;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PeraCore.Editor {
	[CustomPropertyDrawer(typeof(InjectAttribute))]
	public class InjectDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			//Try to inject
			var value = property.GetValue() as Object;
			if (value.SafeIsUnityNull()) {
				var name = property.name;
				foreach (var guid in AssetDatabase.FindAssets(name)) {
					var assetPath = AssetDatabase.GUIDToAssetPath(guid);
					var fileName = Path.GetFileNameWithoutExtension(assetPath);
					if (fileName != name) continue;

					var asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));
					if (asset.SafeIsUnityNull()) continue;

					if (asset.GetType().GetNiceName() != property.GetProperTypeName()) continue;
					property.SetValue(asset);
				}
			}


			var propertyDrawer = GetPropertyDrawer();
			if (propertyDrawer != null) {
				propertyDrawer.OnGUI(position, property, label);
			} else {
				EditorGUI.ObjectField(position, property);
			}
		}

		private PropertyDrawer GetPropertyDrawer() {
			var fieldType = fieldInfo.FieldType;

			var propertyDrawerType = (Type) Type.GetType("UnityEditor.ScriptAttributeUtility,UnityEditor")
				?.GetMethod("GetDrawerTypeForType", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
				?.Invoke(null, new object[] {fieldType});
			if (propertyDrawerType == null) return null;

			PropertyDrawer propertyDrawer = null;
			if (typeof(PropertyDrawer).IsAssignableFrom(propertyDrawerType))
				propertyDrawer = (PropertyDrawer) Activator.CreateInstance(propertyDrawerType);

			return propertyDrawer;
		}
	}
}
