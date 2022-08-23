using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PeraCore.Runtime {
	public class BaseScriptableObject : SerializedScriptableObject {
#if UNITY_EDITOR
		private static readonly HashSet<BaseScriptableObject> _instances = new();
#endif

		private void OnEnable() {
#if UNITY_EDITOR
			if (EditorSettings.enterPlayModeOptionsEnabled) {
				if (Application.isPlaying) {
					Debug.LogWarning(name + " is adding to the instances in play-time!");
					return;
				}
				_instances.Add(this);
				EditorApplication.playModeStateChanged -= HandlePlayModeStateChange;
				EditorApplication.playModeStateChanged += HandlePlayModeStateChange;
			}
#else
		OnEnterPlay();
#endif
		}

		private void OnDisable() {
#if !UNITY_EDITOR
		OnExitPlay();
#endif
		}

#if UNITY_EDITOR

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void OnBeforeSceneLoad() {
			foreach (var instance in _instances) {
				instance.OnEnterPlay();
			}
		}

		private static void HandlePlayModeStateChange(PlayModeStateChange state) {
			if (state == PlayModeStateChange.EnteredEditMode) {
				foreach (var instance in _instances) {
					instance.OnExitPlay();
				}
			}
		}
#endif

		protected virtual void OnEnterPlay() { }

		protected virtual void OnExitPlay() { }
	}
}
