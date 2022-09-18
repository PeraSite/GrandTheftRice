using System;
using UnityEngine;

namespace GrandTheftRice {
	public class Bullet : MonoBehaviour {
		private Rigidbody2D _rb;
		private Camera _camera;

		private void Awake() {
			_rb = GetComponent<Rigidbody2D>();
			_camera = Camera.main;
		}

		public void Init(Vector2 velocity) {
			_rb.velocity = velocity;
		}

		private void Update() {
			CheckOutOfScreen();
		}

		private void CheckOutOfScreen() {
			Vector2 viewportPos = _camera.WorldToViewportPoint(transform.position);

			//If object is outside of screen
			if (viewportPos.x is > 1 or < 0 || viewportPos.y is > 1 or < 0) {
				Destroy(gameObject);
			}
		}
	}
}
