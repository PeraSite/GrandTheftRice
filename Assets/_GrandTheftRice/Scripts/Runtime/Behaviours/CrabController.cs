using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GrandTheftRice {
	public class CrabController : MonoBehaviour {
		[InlineEditor(InlineEditorObjectFieldModes.Foldout)]
		[SerializeField] private CharacterStat _stat;
		
		private Rigidbody2D _rb;

		private void Awake() {
			_rb = GetComponent<Rigidbody2D>();
		}

		public void OnMove(InputValue value) {
			_rb.velocity = value.Get<Vector2>() * _stat.MoveSpeed;
		}
	}
}
