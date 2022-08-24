using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GrandTheftRice {
	public class HealthBar : MonoBehaviour {
		[SerializeField] private Image _bar;
		[SerializeField] private TextMeshProUGUI _text;

		private Damageable _damageable;

		private void OnEnable() {
			_damageable = GetComponentInParent<Damageable>();
			_damageable.OnHealthChanged += OnHealthChanged;
		}

		private void OnDisable() {
			_damageable.OnHealthChanged -= OnHealthChanged;
		}

		private void OnHealthChanged(float health) {
			_bar.fillAmount = health / _damageable.MaxHealth;
			_text.text = Mathf.RoundToInt(health) + " / " + Mathf.RoundToInt(_damageable.MaxHealth);
		}
	}
}
