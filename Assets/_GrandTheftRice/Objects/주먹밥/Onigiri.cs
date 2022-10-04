using MoreMountains.TopDownEngine;
using Sirenix.Utilities;
using UnityEngine;

namespace GrandTheftRice.Objects.Onigiri {
	public class Onigiri : Coin {
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
		}
	}
}
