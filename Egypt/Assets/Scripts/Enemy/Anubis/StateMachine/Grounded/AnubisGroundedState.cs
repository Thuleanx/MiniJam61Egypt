using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx {
	public class AnubisGroundState : AnubisState
	{
		protected Vector2 input;
		bool isGrounded;

		public AnubisGroundState(Anubis anubis, AnubisStateMachine stateMachine, AnubisData playerData, string animName) : base(anubis, stateMachine, playerData, animName)
		{
		}

		public override void CheckPhysics() {
			base.CheckPhysics();

			isGrounded = anubis.CheckIfGrounded();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			// Jump logic
			if (playerSighted && playerDirection.y > 0 && anubis.JumpState.CanJump())
				stateMachine.ChangeState(anubis.JumpState);

			// Follow Player Logic
			if (playerSighted)
				anubis.SetVelocityX(Mathf.Sign(playerDirection.x) * anubisData.baseSpeed);

			if (!isGrounded)	
				stateMachine.ChangeState(anubis.InAirState);

		}
	}
}


