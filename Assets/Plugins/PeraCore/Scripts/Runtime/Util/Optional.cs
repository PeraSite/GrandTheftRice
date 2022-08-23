using System;
using UnityEngine;

[Serializable]
public struct Optional<T> {
	[SerializeField]
	private bool enabled;

	[SerializeField]
	private T value;

	public bool Enabled => enabled;

	public T Value => value;

	public Optional(T initialValue) {
		enabled = true;
		value = initialValue;
	}

	public void Run(Action<T> action) {
		if (!enabled) return;
		action(value);
	}

	public static implicit operator Optional<T>(T value) {
		return new Optional<T>(value);
	}
}
