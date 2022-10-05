using System.Collections;
using GrandTheftRice.Characters.Player.Hook;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace GrandTheftRice.Objects.Buff {
	public class Gochujang : PickableItem {
		[Header("Regenerate")]
		[Tooltip("How much time it takes to regenerate after the pickup")]
		[SerializeField] private float _regenTime;

		[SerializeField] private float _buffDuration;

		protected override void Pick(GameObject picker) {
			if (!picker.TryGetComponent<Character>(out var character)) return;
			var weapon = character.FindAbility<CharacterHandleSecondaryWeapon>().CurrentWeapon as HookWeapon;
			if (weapon == null) return;
			StartCoroutine(Buff(weapon));
			StartCoroutine(Regenerate());
		}

		private IEnumerator Buff(HookWeapon weapon) {
			var beforeRange = weapon.AttackRange;
			weapon.AttackRange = beforeRange * 2;
			yield return new WaitForSecondsRealtime(_buffDuration);
			weapon.AttackRange = beforeRange;
		}

		private IEnumerator Regenerate() {
			yield return new WaitForSecondsRealtime(_regenTime);
			Model.SetActive(true);
			_collider2D.enabled = true;
		}
	}
}
