

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Thuleanx;

public class LaunchPad : MonoBehaviour
{
	public int Height = 7;

	float launchVelocityY;

	void Awake() {
		float gravity = Mathf.Abs(Physics2D.gravity.y);
		float timeToJumpApex = Mathf.Sqrt(2 * Height / gravity);
		launchVelocityY = timeToJumpApex * gravity;
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Agent agent = collision.gameObject.GetComponent<Agent>();
		if (agent != null)
			agent.SetVelocityY(launchVelocityY);
	}
}
