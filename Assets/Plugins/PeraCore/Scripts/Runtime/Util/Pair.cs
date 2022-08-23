using System.ComponentModel;
using Sirenix.OdinInspector;

[InlineProperty]
public struct Pair<TKey, TValue> {
	[HorizontalGroup, HideLabel]
	public TKey Key;
	[HorizontalGroup, HideLabel]
	public TValue Value;

	public Pair(TKey key, TValue value) {
		Key = key;
		Value = value;
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public void Deconstruct(out TKey key, out TValue value) {
		key = Key;
		value = Value;
	}

	public override string ToString() {
		return $"{nameof(Key)}: {Key}, {nameof(Value)}: {Value}";
	}
}
