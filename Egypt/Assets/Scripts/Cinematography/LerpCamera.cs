
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCamera : CameraController
{
	[SerializeField] bool lerpX, lerpY;
	[SerializeField, Range(0f, 1f)] float lerpXConstant, lerpYConstant;

	public override void Update() {
		base.Update();
		SeekFollowPosition();
		if (Active) {
			transform.position = new Vector3(
				lerpX ? Thuleanx.Math.Calculate.AsympEase(transform.position.x, followPosition.transform.position.x, lerpXConstant) : transform.position.x,
				lerpY ? Thuleanx.Math.Calculate.AsympEase(transform.position.y, followPosition.transform.position.y, lerpYConstant) : transform.position.y,
				transform.position.z
			);
		}
	}
}
