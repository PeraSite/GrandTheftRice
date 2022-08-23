using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace PeraCore.Runtime {
	public abstract class MonoSingleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour {
		protected virtual bool KeepAlive => true;

		private static T _instance;

		public static T Instance {
			get {
				if (_instance.SafeIsUnityNull() || _destroyed) {
					_instance = FindObjectOfType<T>(true);
					_destroyed = false;
				}
				return _instance;
			}
		}

		private static bool _destroyed;

		protected virtual void Awake() {
			_instance = this as T;
			SingletonManager.list.Add(() => { _destroyed = true; });
			if (KeepAlive) {
				transform.SetParent(null);
				DontDestroyOnLoad(gameObject);
			}
		}

		protected virtual void OnDestroy() {
			_instance = null;
		}
	}

	public static class SingletonManager {
		public static readonly List<Action> list = new();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void ResetInstance() {
			list.ForEach(action => action());
			list.Clear();
		}
	}
}
