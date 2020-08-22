
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputHandler : MonoBehaviour {

	public float InputBufferTimeSeconds = .2f;

	float jumpInputTime;

	public Vector2 MovementInput {get; private set; }
	public bool JumpInput { get; private set; }
	public bool JumpReleased {get; private set; }

	public void OnMoveInput(InputAction.CallbackContext context) {
		MovementInput = context.ReadValue<Vector2>();
	}

	public void OnJumpInput(InputAction.CallbackContext context) {
		if (context.started) {
			JumpInput = true;
			jumpInputTime = Time.time;
			JumpReleased = false;
		} else if (context.canceled) {
			JumpReleased = true;
		}
	}

	public void UseJumpInput() => JumpInput = false;

	void Update() {
		if ( JumpInput && InputBufferTimeSeconds + jumpInputTime < Time.time) {
			JumpInput = false;	
		}
	}
}
