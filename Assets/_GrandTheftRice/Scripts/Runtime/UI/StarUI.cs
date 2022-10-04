using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GrandTheftRice.UI {
	public class StarUI : MonoBehaviour {
		[SerializeField] private List<Image> _stars;
		[SerializeField] private Sprite _starFull;
		[SerializeField] private Sprite _starEmpty;

		//TODO: 실제 별 수치 가져오기
		[SerializeField] private int _currentStar;

		protected virtual void Update() {
			UpdateUI(_currentStar);
		}

		protected virtual void UpdateUI(int current) {
			_stars.ForEach((image, index) => { image.sprite = index < current ? _starFull : _starEmpty; });
		}
	}
}
