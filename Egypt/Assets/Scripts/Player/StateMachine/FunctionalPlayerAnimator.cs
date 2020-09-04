using UnityEngine;

namespace Thuleanx {
	public class FunctionalPlayerAnimator : MonoBehaviour {
		Player player;

		void Awake() {
			player = GetComponentInParent<Player>();
		}

		public void AnimationTrigger() => player.AnimationTrigger();
		public void AnimationFinishTrigger() => player.AnimationFinishTrigger();
	}
}