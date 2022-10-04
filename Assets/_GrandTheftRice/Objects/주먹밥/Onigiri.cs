using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using Sirenix.Utilities;
using UnityEngine;

namespace GrandTheftRice.Objects.Onigiri {
	public class Onigiri : Coin {
		[Header("Regenerate")]
		[Tooltip("How much time it takes to regenerate after the pickup")]
		[SerializeField] private float _regenTime;

		protected override bool CheckIfPickable() {
			_character = _collidingObject.GetComponent<Character>();
			return !_character.SafeIsUnityNull();
		}

		protected override void Pick(GameObject picker) {
			if (!picker.TryGetComponent<Character>(out var character)) return;
			var movement = character.FindAbility<CharacterMovement>();
			if (movement.SafeIsUnityNull()) return;
			movement.ApplyMovementMultiplier(1.5f, 3f);

			if (character.CharacterType == Character.CharacterTypes.Player) {
				TopDownEnginePointEvent.Trigger(PointsMethods.Add, PointsToAdd);
			}
			StartCoroutine(Regenerate());
		}

		private IEnumerator Regenerate() {
			yield return new WaitForSecondsRealtime(_regenTime);
			Model.SetActive(true);
			_collider2D.enabled = true;
		}
	}
}
