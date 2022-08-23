using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace PeraCore.Runtime {
	public abstract class RuntimeSet<T> : SerializedScriptableObject {
		public List<T> List = new();

		public UnityEvent<T> onAdded;
		public UnityEvent<T> onRemoved;

		public void Clear() {
			List.Clear();
		}

		public T this[int index] {
			get => GetValue(index);
			set => SetValue(index, value);
		}

		public T GetValue(int index) => List[index];
		public void SetValue(int index, T value) => List[index] = value;

		public bool Add(T element) {
			if (List.Contains(element)) return false;
			List.Add(element);
			onAdded?.Invoke(element);
			return true;
		}

		public bool Remove(T element) {
			if (!List.Contains(element)) return false;
			List.Remove(element);
			onRemoved?.Invoke(element);
			return true;
		}

		public T Find(Predicate<T> filter) {
			return List.Find(filter);
		}

		public bool Any(Func<T, bool> filter) {
			return List.Any(filter);
		}
	}
}
