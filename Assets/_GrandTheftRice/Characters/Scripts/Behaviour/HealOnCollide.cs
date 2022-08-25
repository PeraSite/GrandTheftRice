using Sirenix.OdinInspector;
using UnityEngine;

namespace GrandTheftRice {
	public class HealOnCollide : MonoBehaviour {
		[Title("설정")]
		[SerializeField] private float _healAmount = 1;
		[SerializeField] private bool _destroyOnCollide = true;
		[SerializeField] private bool _useTrigger;

		[SerializeField] private string _tagToHeal = "Player";

		private void OnTriggerEnter2D(Collider2D col) {
			if (!_useTrigger) return;
			OnHit(col.gameObject);
		}

		private void OnCollisionEnter2D(Collision2D col) {
			if (_useTrigger) return;
			OnHit(col.gameObject);
		}

		private void OnHit(GameObject go) {
			if (!go.TryGetComponent<Damageable>(out var damageable)) return;
			if (!damageable.CompareTag(_tagToHeal)) return;

			damageable.Heal(_healAmount);
			if (_destroyOnCollide)
				Destroy(gameObject);
		}
	}
}
