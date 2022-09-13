using PixelCrushers;
using Sirenix.OdinInspector;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace GrandTheftRice.UI {
	public class NameSettingUI : MonoBehaviour {
		[Title("오브젝트")]
		[SerializeField] private TMP_InputField _inputField;

		[Title("변수")]
		[SerializeField] private StringVariable _nickname;

		private void Awake() {
			_inputField.onSubmit.RemoveAllListeners();
			_inputField.onSubmit.AddListener(OnSubmit);
		}

		public void OnSubmit() {
			OnSubmit(_inputField.text);
		}

		public void OnSubmit(string newNickname) {
			_nickname.SetValue(newNickname);
			SaveSystem.LoadScene("Play");
		}
	}
}
