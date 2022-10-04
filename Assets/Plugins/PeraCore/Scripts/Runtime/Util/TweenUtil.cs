using DG.Tweening;
using UnityEngine;

namespace GrandTheftRice.UI {
	public static class TweenUtil {
		public static Sequence DOJumpZ(
			this Transform target,
			Vector3 endValue,
			float jumpPower,
			int numJumps,
			float duration,
			bool snapping = false) {
			if (numJumps < 1)
				numJumps = 1;
			var startPosZ = target.position.z;
			var offsetZ = -1f;
			var offsetZSet = false;
			var s = DOTween.Sequence();
			var zTween = (Tween) DOTween
				.To(() => target.position, x => target.position = x,
					new Vector3(0.0f, 0f, jumpPower), duration / (numJumps * 2))
				.SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.OutQuad).SetRelative()
				.SetLoops(numJumps * 2, LoopType.Yoyo)
				.OnStart(() => startPosZ = target.position.z);
			s.Append(DOTween
					.To(() => target.position, x => target.position = x,
						new Vector3(endValue.x, 0.0f, 0.0f), duration).SetOptions(AxisConstraint.X, snapping)
					.SetEase(Ease.Linear))
				.Join(DOTween
					.To(() => target.position, x => target.position = x,
						new Vector3(0.0f, endValue.y, 0), duration).SetOptions(AxisConstraint.Y, snapping)
					.SetEase(Ease.Linear))
				.Join(zTween)
				.SetTarget(target)
				.SetEase(DOTween.defaultEaseType);
			zTween.OnUpdate(() => {
				if (!offsetZSet) {
					offsetZSet = true;
					offsetZ = s.isRelative ? endValue.z : endValue.z - startPosZ;
				}
				var position = target.position;
				position.z += DOVirtual.EasedValue(0.0f, offsetZ, zTween.ElapsedPercentage(), Ease.OutQuad);
				target.position = position;
			});
			return s;
		}
	}
}