
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputHandler : MonoBehaviour {

	public float InputBufferTimeSeconds = .2f;

	float jumpInputTime, attackInputTime;

	public Vector2 MovementInput {get; private set; }
	public bool JumpInput { get; private set; }
	public bool JumpReleased {get; private set; }
	public bool AttackInput {get; private set; }

	bool attackHold = false;

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

	public void OnAttackInput(InputAction.CallbackContext context) {
		if (context.started) {
			attackHold = true;
		} 
		if (context.canceled) {
			attackHold = false;
		}
		if (context.started) {
			AttackInput = true;
			attackInputTime = Time.time;
		}
	}

	public void UseJumpInput() => JumpInput = false;
	public void UseAttackInput() => JumpInput = false;

	void Update() {
		if (attackHold) {
			attackInputTime = Time.time;	
			AttackInput = true;
		}
		if ( JumpInput && InputBufferTimeSeconds + jumpInputTime < Time.time)
			JumpInput = false;	
		if ( AttackInput && InputBufferTimeSeconds + attackInputTime < Time.time)
			AttackInput = false;
	}
}
