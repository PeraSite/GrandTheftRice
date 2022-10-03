using Aarthificial.Reanimation;
using MoreMountains.TopDownEngine;
using Sirenix.Utilities;
using UnityEngine;

namespace GrandTheftRice.Characters.Scripts {
	public class ReanimatorCharacter : Character {
		[SerializeField] private Reanimator _reanimator;

		public override void AssignAnimator(bool forceAssignation = false) {
			if (_animatorInitialized && !forceAssignation) {
				return;
			}
			if (_reanimator.SafeIsUnityNull()) {
				_reanimator = GetComponent<Reanimator>();
			}

			_animatorInitialized = true;
		}

		protected override void UpdateAnimators() {
			base.UpdateAnimators();
			_reanimator.Set("IsMoving", _controller.CurrentMovement.magnitude > 0);
		}
	}
}
