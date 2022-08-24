using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GrandTheftRice {
	public class DamageOnCollide : MonoBehaviour {
		[Title("설정")]
		[SerializeField] private float _damage = 1;
		[SerializeField] private bool _destroyOnCollide = true;
		[SerializeField] private bool _useTrigger;

		[SerializeField] private string _tagToDamage = "Player";

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
			if (!damageable.CompareTag(_tagToDamage)) return;

			damageable.Damage(_damage);
			if (_destroyOnCollide)
				Destroy(gameObject);
		}
	}
}
