using UnityEngine;

namespace PeraCore.Runtime {
	public static class MathUtil {
		public static int Clamp(this int value, int min, int max) => Mathf.Clamp(value, min, max);

		public static float Clamp(this float value, float min, float max) => Mathf.Clamp(value, min, max);

		public static float Clamp01(this float value) => Mathf.Clamp01(value);
	}
}
