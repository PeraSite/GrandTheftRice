using DG.Tweening.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GrandTheftRice.UI {
	public class MainMenuUI : MonoBehaviour {
		[Title("카메라 위치")]
		[SerializeField] private Transform _mainPanel;
		[SerializeField] private Transform _settingPanel;
		[SerializeField] private Transform _rankingPanel;

		[SerializeField] private float _zoomInZ;
		[SerializeField] private float _jumpPower;


		[Title("애니메이션 설정")]
		[SerializeField] private float _animationDuration = 0.5f;

		private Camera _cam;

		private void Awake() {
			_cam = Camera.main;
		}

		[Button]
		public void ShowMainScreen() => MoveCameraTo(_mainPanel.position);

		[Button]
		public void ShowSetting() => MoveCameraTo(_settingPanel.position);

		[Button]
		public void ShowRanking() => MoveCameraTo(_rankingPanel.position);

		private void MoveCameraTo(Vector2 screenPos) {
			var zoomIn = new Vector3(screenPos.x, screenPos.y, _zoomInZ);
			_cam.transform.DOJumpZ(zoomIn, _jumpPower, 1, _animationDuration);
		}

		public void StartGame() {
			GameModeManager.Instance.HandleStartRequested(GameModeManager.Instance.playMode);
		}

		public void QuitGame() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}
