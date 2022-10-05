using System.Collections.Generic;
using GrandTheftRice.System;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace GrandTheftRice.UI {
	public class StarUI : MonoBehaviour {
		[SerializeField] private List<Image> _stars;
		[SerializeField] private Sprite _starFull;
		[SerializeField] private Sprite _starEmpty;

		protected virtual void Update() {
			UpdateUI(WantedSystem.Instance.CurrentStars);
		}

		protected virtual void UpdateUI(int current) {
			_stars.ForEach((image, index) => { image.sprite = index < current ? _starFull : _starEmpty; });
		}
	}
}
