using System.Linq;
using PeraCore.Runtime;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class SceneSelectorDrawer : OdinAttributeDrawer<SceneSelectorAttribute, string> {
	protected override void DrawPropertyLayout(GUIContent label) {
		var rect = EditorGUILayout.GetControlRect(GUILayout.Height(EditorGUIUtility.singleLineHeight * 2));
		if (label != null) {
			rect = EditorGUI.PrefixLabel(rect, label);
		}

		var selected = ValueEntry.SmartValue;
		ValueEntry.SmartValue = GUI.TextField(rect.SplitVertical(0, 2), ValueEntry.SmartValue);
		if (GUI.Button(rect.SplitVertical(1, 2), selected, SirenixGUIStyles.DropDownMiniButton)) {
			var scenesGUIDs = AssetDatabase.FindAssets("t:scene");
			var scenesPaths = scenesGUIDs.Select(AssetDatabase.GUIDToAssetPath)
				.Where(path => path.StartsWith("Assets/"))
				.Select(path => path.Replace(".unity", "").Replace("Assets/", ""))
				.Where(path => path.Contains(Attribute.Filter));

			var selector = new GenericSelector<string>(scenesPaths) {
				FlattenedTree = true
			};
			selector.SelectionConfirmed += result => {
				var list = result.ToList();
				if (CollectionUtil.IsNullOrEmpty(list)) return;
				ValueEntry.SmartValue = list.First().Split("/").Last();
			};
			selector.ShowInPopup(rect.position + new Vector2(0, 20));
		}
	}
}
