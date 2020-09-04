using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thuleanx.FX {
	public class TweenIn : MonoBehaviour {

		[SerializeField] Vector2 posAway = new Vector2(0, -5f);
		[SerializeField] float duration = 2f;

		float startTime;
		Vector2 dest, pos; 

		void OnEnable() {
			dest = transform.position;
			pos = (Vector2) transform.position + posAway;
			transform.position = pos;
			startTime = Time.time;	
		}

		public virtual float Ease(float t) {
			return t;
		}

		void Update() {
			float t = Mathf.Clamp01((Time.time - startTime) / duration);
			transform.position = Vector3.Lerp(pos, dest, t) + Vector3.forward * transform.position.z;
		}
	}

	public class TweenInCubicBezier : TweenIn {

	}
}
