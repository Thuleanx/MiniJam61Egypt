
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thuleanx {
	public class AnubisInAirState : AnubisState {
		protected bool isGrounded;

		bool isJumping;

		public AnubisInAirState(Anubis anubis, AnubisStateMachine stateMachine, AnubisData anubisData, string animName) : base(anubis, stateMachine, anubisData, animName)
		{
		}

		public override void CheckPhysics() {
			base.CheckPhysics();

			isGrounded = anubis.CheckIfGrounded();
		}	

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (stateMachine.CurrentState == this) {
				if (isGrounded && anubis.Body.velocity.y <= 0.0001f)
					stateMachine.ChangeState(anubis.LandState);
			}
		}
	}
}