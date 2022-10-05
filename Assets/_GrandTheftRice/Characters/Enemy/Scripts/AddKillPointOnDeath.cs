using System;
using GrandTheftRice.System;
using MoreMountains.TopDownEngine;
using UnityEngine;

namespace GrandTheftRice.Characters.Enemy.Scripts {
	[RequireComponent(typeof(Health))]
	public class AddKillPointOnDeath : MonoBehaviour {
		[SerializeField] private int _killPoint;

		private Health _health;

		private void Awake() {
			_health = GetComponent<Health>();
		}

		private void OnEnable() {
			_health.OnDeath += OnDeath;
		}

		private void OnDisable() {
			_health.OnDeath -= OnDeath;
		}

		private void OnDeath() {
			WantedSystem.Instance.AddKillPoint(_killPoint);
		}
	}
}
