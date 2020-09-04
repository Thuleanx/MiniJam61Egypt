
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Thuleanx.Math;

namespace Thuleanx {
	public class AnubisJumpState : AnubisAbilityState {
		float jumpVelocity;

		Timers timers;

		public AnubisJumpState(Anubis anubis, AnubisStateMachine stateMachine, AnubisData anubisData, string animName) : base(anubis, stateMachine, anubisData, animName)
		{
			float gravity = Mathf.Abs(Physics2D.gravity.y);
			float timeToJumpApex = Mathf.Sqrt(2 * anubisData.jumpHeight / gravity);
			jumpVelocity = gravity * timeToJumpApex;

			timers = new Timers();
			timers.RegisterTimer("JumpCD");
		}

		public override void Enter() {
			base.Enter();
			anubis.SetVelocityY(jumpVelocity);

			timers.StartTimer("JumpCD", anubisData.jumpCooldownSeconds);

			abilityDone = true;
		}

		public bool CanJump() => !timers.ActiveAndNotExpired("JumpCD");
	}
}

