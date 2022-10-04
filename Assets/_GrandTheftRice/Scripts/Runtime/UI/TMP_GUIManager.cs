using MoreMountains.TopDownEngine;
using TMPro;
using UnityEngine;

public class TMP_GUIManager : GUIManager {
	[Header("TMP")]
	[SerializeField] private TextMeshProUGUI _pointTMP;

	public override void RefreshPoints() {
		base.RefreshPoints();
		if (_pointTMP != null) {
			_pointTMP.text = GameManager.Instance.Points.ToString(PointsTextPattern);
		}
	}
}
