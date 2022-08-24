using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GrandTheftRice {
	public class CrabController : MonoBehaviour {
		[InlineEditor(InlineEditorObjectFieldModes.Foldout)]
		[SerializeField] private CharacterStat _stat;


		private Rigidbody2D _rb;
		private Camera _cam;

		private void Awake() {
			_rb = GetComponent<Rigidbody2D>();
			_cam = Camera.main;
		}

		public void OnMove(InputAction.CallbackContext ctx) {
			_rb.velocity = ctx.ReadValue<Vector2>() * _stat.MoveSpeed;
		}

	}
}
