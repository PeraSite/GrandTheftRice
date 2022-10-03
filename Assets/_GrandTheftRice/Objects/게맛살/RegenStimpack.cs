using System.Collections;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace Objects {
	public class RegenStimpack : Stimpack {
		[Header("Regenerate")]
		[Tooltip("How much time it takes to regenerate after the pickup")]
		[SerializeField] private float _regenTime;

		private WaitForSecondsRealtime _awaiter;

		private void Awake() {
			_awaiter = new WaitForSecondsRealtime(_regenTime);
		}

		protected override void Pick(GameObject picker) {
			base.Pick(picker);
			StartCoroutine(Regenerate());
		}

		private IEnumerator Regenerate() {
			yield return _awaiter;
			Model.SetActive(true);
			_collider2D.enabled = true;
		}
	}
}
