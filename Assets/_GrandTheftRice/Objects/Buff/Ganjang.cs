using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace GrandTheftRice.Objects.Buff {
	public class Ganjang : PickableItem {
		[Header("Regenerate")]
		[Tooltip("How much time it takes to regenerate after the pickup")]
		[SerializeField] private float _regenTime;

		[SerializeField] private float _buffDuration;

		protected override void Pick(GameObject picker) {
			if (!picker.TryGetComponent<Character>(out var character)) return;
			var weapon = character.FindAbility<CharacterHandleWeapon>().CurrentWeapon;
			if (weapon == null) return;
			StartCoroutine(Buff(weapon));
			StartCoroutine(Regenerate());
		}

		private IEnumerator Buff(Weapon weapon) {
			var timeBetweenUses = weapon.TimeBetweenUses;
			weapon.TimeBetweenUses = timeBetweenUses / 2;
			yield return new WaitForSecondsRealtime(_buffDuration);
			weapon.TimeBetweenUses = timeBetweenUses;
		}

		private IEnumerator Regenerate() {
			yield return new WaitForSecondsRealtime(_regenTime);
			Model.SetActive(true);
			_collider2D.enabled = true;
		}
	}
}
