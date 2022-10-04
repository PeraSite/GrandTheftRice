using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using PixelCrushers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GrandTheftRice.System {
	public class EndingSystem : MMSingleton<EndingSystem> {
		[Title("설정")]
		[Tooltip("게임 전체 플레이 타임(초)")]
		[field: SerializeField] public int GameTime { get; private set; } = 600;

		[Title("런타임 값")]
		[Tooltip("남은 플레이 타임(초)")]
		public int CurrentTime;

		[SerializeField] private UIPanel _endingPanel;
		[SerializeField] private TextMeshProUGUI _score;

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

		[Button]
		private void EndGame() {
			CurrentTime = GameTime;
			GameManager.Instance.Pause(PauseMethods.NoPauseMenu);
			_endingPanel.Open();
			_score.text = GameManager.Instance.Points.ToString();
		}

		public void GoMain() {
			GameManager.Instance.UnPause(PauseMethods.NoPauseMenu);
			GameModeManager.Instance.HandleStartRequested(GameModeManager.Instance.mainMenuMode);
		}
	}
}
