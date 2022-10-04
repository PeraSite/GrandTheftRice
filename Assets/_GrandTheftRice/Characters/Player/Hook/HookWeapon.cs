using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Sirenix.Utilities;
using UnityEngine;

namespace GrandTheftRice.Characters.Player.Hook {
	public class HookWeapon : Weapon {
		[MMInspectorGroup("Hook", true)]
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private float _maxLength = 5f;
		[SerializeField] private float _stunTime = 1f;
		[SerializeField] private Ease _ease;
		[SerializeField] private float _force = 500f;

		private float _lineCompletePercent;
		private Vector3 _targetPosition;
		private CharacterHandleWeapon _handleWeapon;
		private bool _isHit;
		private bool _isHanging;

		public override void Initialization() {
			base.Initialization();
			_handleWeapon = Owner.FindAbility<CharacterHandleWeapon>();
			_lineRenderer.enabled = false;
		}

		protected override void Update() {
			base.Update();
			_lineRenderer.SetPosition(0, transform.position);
			HookCooldownUI.Instance.CurrentCooldown = _delayBetweenUsesCounter;
		}

		private void OnDisable() {
			_lineRenderer.DOKill();
		}


		public override void WeaponUse() {
			if (_isHanging) return;

			base.WeaponUse();
			_lineRenderer.DOKill();
			_handleWeapon.PermitAbility(false);
			_lineRenderer.SetPosition(0, transform.position);
			_lineRenderer.SetPosition(1, transform.position);
			_targetPosition = (GetMousePosition() - transform.position).normalized * _maxLength + transform.position;
			_lineCompletePercent = 0f;
			_isHit = false;
			_lineRenderer.enabled = true;
			DOTween.Sequence(_lineRenderer)
				.Append(TweenLine(1f, 0.5f).OnUpdate(CheckHookCollide))
				.Append(EndLineTween());
		}

		private Tween TweenLine(float endValue, float duration) {
			return DOTween.To(GetLineCompletion, SetLineCompletion, endValue, duration).SetEase(_ease);
		}

		private Tween EndLineTween() {
			return DOTween.Sequence(_lineRenderer)
				.Append(TweenLine(0f, 0.3f))
				.AppendCallback(() => {
					_handleWeapon.PermitAbility(true);
					_lineRenderer.enabled = false;
				});
		}

		private float GetLineCompletion() => _lineCompletePercent;

		private void SetLineCompletion(float percent) {
			_lineCompletePercent = percent;
			var endPosition = transform.position + (_targetPosition - transform.position) * percent;
			endPosition.z = -1f; //Fixing same Z position issue
			_lineRenderer.SetPosition(1, endPosition);
		}

		private void CheckHookCollide() {
			if (_isHit) return;

			var worldPosition = GetLineWorldPosition();
			var hit = Physics2D.Linecast(transform.position, worldPosition, LayerMask.GetMask("Enemies", "Obstacles"));
			var col = hit.collider;
			if (col.SafeIsUnityNull()) return;

			_isHit = true;
			_lineRenderer.DOKill();

			if (col.CompareTag("Enemy")) {
				var end = EndLineTween();

				if (!col.TryGetComponent<Character>(out var character)) return;
				var stun = character.FindAbility<CharacterStun>();
				if (stun.SafeIsUnityNull()) return;
				stun.StunFor(_stunTime);
				character.CharacterHealth.Damage(2f, Owner.gameObject, 0.2f,0.2f, (character.transform.position - Owner.transform.position).normalized);
				end.SetDelay(0.3f);
			} else if (col.CompareTag("World")) {
				StartCoroutine(MoveTowardWall(hit.point));
			} else {
				EndLineTween();
			}
		}

		private IEnumerator MoveTowardWall(Vector3 targetPosition) {
			_isHanging = true;
			yield return new WaitForSecondsRealtime(0.5f);

			var dir = (targetPosition - transform.position).normalized;
			_controller.CurrentMovement = Vector3.zero;
			_controller.Impact(dir, _force);

			_isHanging = false;
			EndLineTween();
		}

		private Vector3 GetLineWorldPosition() {
			return _lineRenderer.GetPosition(1);
		}

		private Vector3 GetMousePosition() {
			var mousePosition = _weaponAim.GetMousePosition();
			mousePosition.z = 0f;
			return mousePosition;
		}
	}
}
