using MoreMountains.Tools;
using UnityEngine;

namespace GrandTheftRice.System {
	public class WantedSystem : MMSingleton<WantedSystem> {
		[field: SerializeField] public int CurrentStars { get; private set; }
		[field: SerializeField] public int KillPoints { get; private set; }

		private void Start() {
			CurrentStars = 0;
			KillPoints = 0;
		}

		public void AddStar() {
			CurrentStars++;
		}

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
