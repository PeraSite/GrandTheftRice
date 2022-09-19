using MoreMountains.TopDownEngine;
using UnityEngine;

namespace Characters.Player.Scripts {
	public class WeaponAim2DWithCursor : WeaponAim2D {
		private Camera _cam;

		private void Awake() {
			_cam = Camera.main;
		}

		protected override void OnEnable() {
			base.OnEnable();
			// Cursor.visible = false;
		}

		protected override void HideMousePointer() {
			if (AimControl != AimControls.Mouse) {
				return;
			}
			if (GameManager.Instance.Paused) {
				Cursor.visible = true;
				return;
			}
			if (ReplaceMousePointer) {
				var view = _cam.ScreenToViewportPoint(Input.mousePosition);
				var isOutside = view.x is < 0 or > 1 || view.y is < 0 or > 1;
				Cursor.visible = isOutside;
			} else {
				Cursor.visible = true;
			}
		}
	}
}
