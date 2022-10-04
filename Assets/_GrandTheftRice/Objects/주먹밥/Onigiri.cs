using MoreMountains.TopDownEngine;
using Sirenix.Utilities;
using UnityEngine;

namespace GrandTheftRice.Objects.Onigiri {
	public class Onigiri : Coin {
		protected override void Pick(GameObject picker) {
			base.Pick(picker);
			if (!picker.TryGetComponent<Character>(out var character)) return;
			var movement = character.FindAbility<CharacterMovement>();
			if (movement.SafeIsUnityNull()) return;
			movement.ApplyMovementMultiplier(1.5f, 3f);
		}
	}
}
