using GrandTheftRice.System;
using UnityEngine;
using UnityEngine.UI;

namespace GrandTheftRice.UI {
	public class TimerUI : MonoBehaviour {
		//TODO: 실제 시간을 받아와서 표시해야함
		[SerializeField] private Image _bar;
		[SerializeField] private Image _handle;

		[SerializeField] private float _handleMinX;
		[SerializeField] private float _handleMaxX;

		public float Percent => (float) EndingSystem.Instance.CurrentTime / EndingSystem.Instance.GameTime;

		private void Update() {
			_bar.fillAmount = Percent;
			_handle.rectTransform.anchoredPosition =
				new Vector2(_handleMinX + (_handleMaxX - _handleMinX) * Percent, 0);
		}
	}
}
