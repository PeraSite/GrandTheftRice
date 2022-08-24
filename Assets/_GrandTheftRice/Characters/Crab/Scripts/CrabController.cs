using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GrandTheftRice {
	public class CrabController : MonoBehaviour {
		[InlineEditor(InlineEditorObjectFieldModes.Foldout)]
		[SerializeField] private CharacterStat _stat;

		[SerializeField] private Bullet _bullet;

		private Rigidbody2D _rb;
		private Camera _cam;

		private void Awake() {
			_rb = GetComponent<Rigidbody2D>();
			_cam = Camera.main;
		}

		public void OnMove(InputValue value) {
			_rb.velocity = value.Get<Vector2>() * _stat.MoveSpeed;
		}

		public void OnFire() {
			SpawnBullet(0);
			SpawnBullet(30);
			SpawnBullet(-30);
		}

		private void SpawnBullet(float angle) {
			var playerPos = (Vector2) transform.position;
			var aimPos = (Vector2) _cam.ScreenToWorldPoint(Input.mousePosition);
			var direction = (Vector2) (Quaternion.Euler(0, 0, angle) * (playerPos - aimPos)).normalized;
			const float BULLET_SPEED_MODIFIER = 3f;
			var power = _stat.MoveSpeed * BULLET_SPEED_MODIFIER;

			var bullet = Instantiate(_bullet, playerPos, Quaternion.identity);
			var velocity = direction * power * -1;
			Debug.Log($"dir:{direction}, vel:{velocity}");
			bullet.Init(velocity);
		}
	}
}
