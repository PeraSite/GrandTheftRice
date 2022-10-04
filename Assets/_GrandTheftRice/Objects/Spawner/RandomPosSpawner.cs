using System;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GrandTheftRice.Objects.Spawner {
	public class RandomPosSpawner : TimedSpawner {
		[MinMaxSlider(-3, 3, true)]
		[SerializeField] private Vector2 _xOffset;

		[MinMaxSlider(-3f, 3f, true)]
		[SerializeField] private Vector2 _yOffset;

		protected override void Spawn() {
			//TODO: Check max spawn count from IntVariable that determined by star count
			GameObject nextGameObject = ObjectPooler.GetPooledGameObject();

			// mandatory checks
			if (nextGameObject == null) {
				return;
			}
			if (nextGameObject.GetComponent<MMPoolableObject>() == null) {
				throw new Exception(gameObject.name +
				                    " is trying to spawn objects that don't have a PoolableObject component.");
			}

			// we activate the object
			nextGameObject.gameObject.SetActive(true);
			nextGameObject.gameObject.MMGetComponentNoAlloc<MMPoolableObject>().TriggerOnSpawnComplete();

			// we check if our object has an Health component, and if yes, we revive our character
			Health objectHealth = nextGameObject.gameObject.MMGetComponentNoAlloc<Health>();
			if (objectHealth != null) {
				objectHealth.Revive();
			}

			// we position the object
			nextGameObject.transform.position = this.transform.position + new Vector3(
				Random.Range(_xOffset.x, _xOffset.y),
				Random.Range(_yOffset.x, _yOffset.y)
			);

			// we reset our timer and determine the next frequency
			_lastSpawnTimestamp = Time.time;
			DetermineNextFrequency();
		}
	}
}
