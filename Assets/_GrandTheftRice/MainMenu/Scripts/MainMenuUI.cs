using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GrandTheftRice.MainMenu {
	public class MainMenuUI : MonoBehaviour {
		[SerializeField] private Vector3 _defaultCameraPosition;
		[SerializeField] private Vector3 _settingCameraPosition;
		[SerializeField] private Vector3 _rankingCameraPosition;


		private Camera _cam;
		[SerializeField] private float _animationDuration = 0.5f;

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
