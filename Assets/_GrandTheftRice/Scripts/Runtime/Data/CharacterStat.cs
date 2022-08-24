using UnityEngine;

namespace GrandTheftRice {
	public class CharacterStat : ScriptableObject {
		[field: SerializeField] public float Health { get; private set; }
		[field: SerializeField] public float MoveSpeed { get; private set; }
	}
}
