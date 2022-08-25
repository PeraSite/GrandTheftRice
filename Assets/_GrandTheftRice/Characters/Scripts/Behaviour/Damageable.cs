using System;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GrandTheftRice {
	public class Damageable : MonoBehaviour {
		[Title("설정")]
		[InlineEditor(InlineEditorObjectFieldModes.Foldout)]
		[SerializeField] private CharacterStat _stat;

		[field: Title("런타임 정보"),
		        OnValueChanged(nameof(InvokeHealthChanged)),
		        InlineButton("@Damage(1)", "Damage"),
		        InlineButton("@Heal(1)", "Heal")]
		[field: SerializeField] public float Health { get; private set; }

		public float MaxHealth => _stat.MaxHealth;

		public event Action<float> OnHealthChanged;

		private void Awake() {
			Health = _stat.MaxHealth;
			InvokeHealthChanged();
		}

		public void Damage(float amount) {
			Health = (Health - amount).Clamp(0, _stat.MaxHealth);
			InvokeHealthChanged();
		}

		public void Heal(float amount) => Damage(-amount);

		private void InvokeHealthChanged() {
			//if health is equals or less than 0, then destroy this object
			OnHealthChanged?.Invoke(Health);
			if (Health <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
