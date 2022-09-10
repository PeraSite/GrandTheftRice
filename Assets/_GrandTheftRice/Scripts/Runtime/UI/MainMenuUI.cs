using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GrandTheftRice.UI {
	public class MainMenuUI : MonoBehaviour {
		[Title("카메라 위치")]
		[SerializeField] private Vector3 _defaultCameraPosition;
		[SerializeField] private Vector3 _settingCameraPosition;
		[SerializeField] private Vector3 _rankingCameraPosition;

		[Title("애니메이션 설정")]
		[SerializeField] private float _animationDuration = 0.5f;
		
		private Camera _cam;

		private void Awake() {
			_cam = Camera.main;
		}

		[Button]
		public void ShowSetting() => MoveCameraTo(_settingCameraPosition);

		[Button]
		public void ShowRanking() => MoveCameraTo(_rankingCameraPosition);

		[Button]
		public void ShowMainScreen() => MoveCameraTo(_defaultCameraPosition);

		private void MoveCameraTo(Vector3 position) {
			_cam.transform.DOMove(position, _animationDuration);
		}
	}
}
