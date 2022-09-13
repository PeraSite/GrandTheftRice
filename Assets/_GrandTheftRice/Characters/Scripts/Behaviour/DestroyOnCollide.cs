using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GrandTheftRice {
	public class DestroyOnCollide : MonoBehaviour {
		[Title("설정")]
		[SerializeField] private bool _useTrigger;
		[SerializeField] private List<string> _targetTags;

		private void OnTriggerEnter2D(Collider2D col) {
			if (!_useTrigger) return;
			OnHit(col.gameObject);
		}

		private void OnCollisionEnter2D(Collision2D col) {
			if (_useTrigger) return;
			OnHit(col.gameObject);
		}

		private void OnHit(GameObject go) {
			if (!_targetTags.Any(go.CompareTag)) return;

			Destroy(gameObject);
		}
	}
}
