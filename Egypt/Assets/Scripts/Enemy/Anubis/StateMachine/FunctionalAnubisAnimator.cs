using UnityEngine;

namespace Thuleanx {
	public class FunctionalAnubisAnimator : MonoBehaviour {
		Anubis anubis;

		void Awake() {
			anubis = GetComponentInParent<Anubis>();
		}

		public void AnimationTrigger() => anubis.AnimationTrigger();
		public void AnimationFinishTrigger() => anubis.AnimationFinishTrigger();
	}
}