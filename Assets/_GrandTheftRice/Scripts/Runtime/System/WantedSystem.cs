using System;
using System.Linq;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace GrandTheftRice.System {
	public class WantedSystem : MMSingleton<WantedSystem> {
		[field: SerializeField] public int CurrentStars { get; private set; }
		[field: SerializeField] public int KillPoints { get; private set; }

		private void Start() {
			CurrentStars = 0;
			KillPoints = 0;
		}

		private void Update() {
			var aiList = FindObjectsOfType<Character>()
				.Where(c => c.CharacterType == Character.CharacterTypes.AI);

			aiList.Where(c => c.PlayerID == "경찰")
				.ForEach(c => {
					var movement = c.FindAbility<CharacterMovement>();
					movement.MovementSpeedMultiplier = CurrentStars switch {
						0 => 1,
						1 => 1.1f,
						2 => 1.2f,
						3 => 1.3f,
						4 => 1.4f,
						5 => 1.5f,
						_ => 1f
					};

					var weapon = c.FindAbility<CharacterHandleWeapon>().CurrentWeapon;
					weapon.TimeBetweenUses = CurrentStars switch {
						0 => 1f,
						1 => 1f,
						2 => 1f,
						3 => 0.8f,
						4 => 0.8f,
						5 => 0.8f,
						_ => 1f
					};
				});
		}

		[Button]
		public void AddStar() {
			CurrentStars++;
			EndingSystem.Instance.GameTime = CurrentStars switch {
				1 => 360,
				2 => 420,
				3 => 480,
				4 => 540,
				5 => 600,
				_ => EndingSystem.DEFAULT_GAME_TIME
			};

			var refill = LevelManager.Instance.Players.First().GetComponent<HealthAutoRefill>();
			refill.DurationBetweenBursts =
				CurrentStars switch {
					1 => 5f,
					2 => 10f,
					3 => 15f,
					4 => 20f,
					5 => 25f,
					_ => 0f
				};
		}

		[Button]
		public void AddKillPoint(int amount) {
			KillPoints += amount;
			switch (CurrentStars) {
				case 0 when KillPoints >= 2:
				case 1 when KillPoints >= 10:
				case 2 when KillPoints >= 20:
				case 3 when KillPoints >= 40:
				case 4 when KillPoints >= 100:
					AddStar();
					break;
			}
		}
	}
}
