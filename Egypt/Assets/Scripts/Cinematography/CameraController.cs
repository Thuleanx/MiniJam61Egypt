using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	protected 
	Transform followPosition; // player

	protected 
	float camHeight, camWidth;

	protected
	bool Active {get; private set;}

	public virtual void Awake() {
		SeekFollowPosition();
		UpdateSize();
	}

	void OnEnable() {
		Activate();
	}

	protected void SeekFollowPosition() {
		followPosition = GameObject.FindGameObjectWithTag("Player").transform;
	}

	protected void UpdateSize() {
		camHeight = 2 * Camera.main.orthographicSize;
		camWidth = camHeight * Camera.main.aspect;
	}

	public virtual void Update() {
		UpdateSize();
	}

	public void Activate() => Active = true;
	public void Disable() => Active = false;
}
