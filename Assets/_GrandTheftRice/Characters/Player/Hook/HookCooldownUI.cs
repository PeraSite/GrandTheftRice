using PeraCore.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace GrandTheftRice.Characters.Player.Hook {
	public class HookCooldownUI : MonoSingleton<HookCooldownUI> {
		protected override bool KeepAlive => false;

		[SerializeField] private Image _image;
		[SerializeField] private float _time;
		[ReadOnly]
		public float CurrentCooldown;

		private void Update() {
			_image.fillAmount = 1f - (CurrentCooldown / _time);
		}
	}
}
