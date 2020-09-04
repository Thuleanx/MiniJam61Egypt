

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToPOI : CameraController
{
	LerpCamera lerp;
	DualForwardFocus focus;
	float defaultOrthoSize;

	POI poi;
	[SerializeField, Range(0f, 1f)] float lerpConstant = 0.01f;

	public override void Awake() {
		base.Awake();
		lerp = GetComponent<LerpCamera>();
		focus = GetComponent<DualForwardFocus>();
		defaultOrthoSize = Camera.main.orthographicSize;
	}
	
	public void AttachPOI(POI poi) {
		this.poi = poi;
		lerp?.Disable();
		focus?.Disable();
	}

	public void Detach() {
		poi = null;
		lerp?.Activate();
		focus?.Activate();
	}

	public override void Update() {
		base.Update();
		SeekFollowPosition();

		// if has poi
		if (poi != null) {				
			// orthographic lerp
			Camera.main.orthographicSize = Thuleanx.Math.Calculate.AsympEase(
				Camera.main.orthographicSize,
				poi.OrthoSize,
				lerpConstant	
			);

			// position lerp
			transform.position = new Vector3(
				Thuleanx.Math.Calculate.AsympEase(transform.position.x, poi.transform.position.x, lerpConstant),
				Thuleanx.Math.Calculate.AsympEase(transform.position.y, poi.transform.position.y, lerpConstant),
				transform.position.z
			);
		} 
	}

	void LateUpdate() {
		transform.position = new Vector3(
			transform.position.x,
			transform.position.y,
			-10
		);
		print(transform.position);
	}
}
