using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Sirenix.Utilities;
using UnityEngine;

namespace GrandTheftRice.Characters.Enemy.Scripts {
	public class AIDecisionDetectTargetByHit : AIDecisionHit, MMEventListener<MMDamageTakenEvent> {
		protected override void OnEnable() {
			_health = _brain.gameObject.GetComponentInParent<Health>();
			if (_health.SafeIsUnityNull()) {
				Debug.LogError($"Can't find Health component in parent:{name}");
				return;
			}

			this.MMEventStartListening();
		}

		protected override void OnDisable() {
			this.MMEventStopListening();
		}

		public void OnMMEvent(MMDamageTakenEvent e) {
			var instigator = e.Instigator;
			var victim = e.AffectedCharacter;

			//맞은 캐릭터가 없다면 무시
			if (victim.SafeIsUnityNull()) return;

			//다른 캐릭터가 맞은거라면 무시
			if (victim.gameObject != _brain.Owner) return;

			//공격한 사람이 없다면 무시(자연적인 데미지라면)
			if (instigator.SafeIsUnityNull()) return;

			//만약 투사체가 아니라면 instigator가 공격한 것으로 간주
			if (!instigator.TryGetComponent<Projectile>(out var projectile)) {
				OnHit();
				_brain.Target = instigator.transform;
				return;
			}

			//투사체라면 투사체의 Owner가 공격한 것으로 간주
			var owner = projectile.GetOwner();
			if (owner.SafeIsUnityNull()) return;
			_brain.Target = owner.transform;
			OnHit();
		}
	}
}
