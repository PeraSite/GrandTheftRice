using MoreMountains.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GrandTheftRice.System {
	public class EndingSystem : MMSingleton<EndingSystem> {
		[Title("설정")]
		[Tooltip("게임 전체 플레이 타임(초)")]
		[field: SerializeField] public int GameTime { get; private set; } = 600;

		[ReadOnly]
		[Tooltip("남은 플레이 타임(초)")]
		public int CurrentTime;

		private float _timer;

		protected override void Awake() {
			base.Awake();
			CurrentTime = 0;
		}

		private void Update() {
			_timer += Time.deltaTime;
			if (_timer >= 1) {
				_timer = 0;
				CurrentTime++;
				if (CurrentTime >= GameTime) {
					EndGame();
				}
			}
		}

		private void EndGame() {
			Debug.Log("game end!");
		}
	}
}
