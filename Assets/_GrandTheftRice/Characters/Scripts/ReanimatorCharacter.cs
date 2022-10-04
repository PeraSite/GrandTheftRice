using Aarthificial.Reanimation;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace GrandTheftRice.Characters.Scripts {
	public class ReanimatorCharacter : Character {
		[SerializeField] private Reanimator _reanimator;

		private CharacterHandleWeapon _handleWeapon;

		protected override void OnEnable() {
			base.OnEnable();
			_handleWeapon = FindAbility<CharacterHandleWeapon>();
			//TODO: Attack animation
		}

		protected override void OnHit() {
			base.OnHit();
			_reanimator.Set("state", Drivers.Hit);
		}

		protected override void UpdateAnimators() {
			base.UpdateAnimators();
			_reanimator.Set("isMoving", _controller.CurrentMovement.magnitude > 0);
		}

		private static class Drivers {
			public const int Idle = 0;
			public const int Hit = 1;
			public const int Attack = 2;
		}
	}
}
