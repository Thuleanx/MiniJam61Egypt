using UnityEngine;

namespace Thuleanx.Math {
	public class Random {
		static Unity.Mathematics.Random random;	
		static bool initiated = false;

		static void Init() {
			if (!initiated)
			{
				random = new Unity.Mathematics.Random(
					(uint) (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc)).TotalSeconds);

				initiated = true;
			}
		}

		public static float Range(float low, float hi) {
			Init();
			return random.NextFloat(low, hi);
		}

		public static int Range(int lo, int hi) {
			Init();
			return random.NextInt(lo, hi + 1);
		}
	}
}
	