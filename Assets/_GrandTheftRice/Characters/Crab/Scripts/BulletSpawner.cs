using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GrandTheftRice {
	public class BulletSpawner : MonoBehaviour {
		[SerializeField] private Bullet _bulletPrefab;
		[SerializeField] private float _bulletSpeed = 3f;
		[SerializeField] private float _shootDelay = 1f;

		private Camera _cam;

		private void Awake() {
			_cam = Camera.main;
		}

		public void OnFire(InputAction.CallbackContext ctx) {
			if (ctx.started) {
				StartCoroutine(nameof(Shoot));
			}

			if (ctx.canceled) {
				StopCoroutine(nameof(Shoot));
			}
		}

		//coroutine that spawn bullet every _shootDelay seconds
		private IEnumerator Shoot() {
			while (true) {
				SpawnBullet(0);
				SpawnBullet(30);
				SpawnBullet(-30);
				yield return new WaitForSeconds(_shootDelay);
			}
		}

		private void SpawnBullet(float angle) {
			var playerPos = (Vector2) transform.position;
			var aimPos = (Vector2) _cam.ScreenToWorldPoint(Input.mousePosition);
			var direction = (Vector2) (Quaternion.Euler(0, 0, angle) * (playerPos - aimPos)).normalized;
			var bullet = Instantiate(_bulletPrefab, playerPos, Quaternion.identity);
			var velocity = direction * _bulletSpeed * -1;
			bullet.Init(velocity);
		}
	}
}
