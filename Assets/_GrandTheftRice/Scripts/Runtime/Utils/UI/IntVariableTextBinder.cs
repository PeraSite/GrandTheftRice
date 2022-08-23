using Sirenix.Utilities;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace ShakaCat {
	public class IntVariableTextBinder : MonoBehaviour {
		public TextMeshProUGUI Text;
		public IntVariable Variable;

		public bool IsFormatted;

		public string Prefix;
		public string Suffix;

		private void OnValidate() {
			if (Text.SafeIsUnityNull()) Text = GetComponent<TextMeshProUGUI>();
		}

		private void Awake() {
			Variable.Changed.Register(OnChanged);
		}

		private void OnDisable() {
			Variable.Changed.Unregister(OnChanged);
		}

		private void OnChanged(int value) {
			var str = IsFormatted ? string.Format("{0:#,0}", value) : value.ToString();
			Text.text = Prefix + str + Suffix;
		}
	}
}
