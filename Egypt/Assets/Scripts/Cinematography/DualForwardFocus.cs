using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;
using Thuleanx.Math;

public class DualForwardFocus : CameraController
{
	[SerializeField, Range(0f, .5f)] float leftOffset;
	[SerializeField, Range(0f, .5f)] float rightOffset;

	[SerializeField] float deadZoneTime = 1f;
	[SerializeField] float focusRate = 1f;
	float currentXOffset;
	float desiredOffset;

	Timers timers;
	Player player;

	public override void Awake() {
		base.Awake();
		timers = new Timers();
		timers.RegisterTimer("offsetChange");
		timers.StartTimer("offsetChange", deadZoneTime);
		currentXOffset = 0;
		player = followPosition.GetComponent<Player>();
	}

	public override void Update() {
		base.Update();
		SeekFollowPosition();

		if (Active) {

			if (desiredOffset == Mathf.Sign(player.FacingDirection))
				timers.StartTimer("offsetChange", deadZoneTime);
			else if (timers.Expired("offsetChange")) {
				desiredOffset = Mathf.Sign(player.FacingDirection);
				timers.StartTimer("offsetChange", deadZoneTime);
			}

			if (player.GetVelocity().x != 0) {
				// adjust camera	
				currentXOffset = Calculate.AsympEase(currentXOffset, desiredOffset > 0 ? rightOffset : -leftOffset, focusRate);
			}

			transform.position = new Vector3(
				Calculate.AsympEase(transform.position.x, followPosition.position.x + currentXOffset * camWidth, 0.1f),
				transform.position.y,
				transform.position.z
			);
		}

	}

	void OnDrawGizmos() {
		UpdateSize();

		Gizmos.color = Color.red;
		Gizmos.DrawLine(
			transform.position - Vector3.down * camHeight * .5f + Vector3.left * leftOffset * camWidth, 
			transform.position + Vector3.up * camHeight * .5f + Vector3.left * leftOffset * camWidth
		);

		Gizmos.color = Color.blue;
		Gizmos.DrawLine(
			transform.position - Vector3.down * camHeight * .5f + Vector3.right * leftOffset * camWidth, 
			transform.position + Vector3.up * camHeight * .5f + Vector3.right * leftOffset * camWidth
		);
	}
}
