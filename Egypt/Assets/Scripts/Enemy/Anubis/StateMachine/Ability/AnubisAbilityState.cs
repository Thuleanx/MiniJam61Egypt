using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thuleanx {
	public class AnubisAbilityState : AnubisState
	{
		protected bool abilityDone;
		protected bool isGrounded;

		public AnubisAbilityState(Anubis anubis, AnubisStateMachine stateMachine, AnubisData playerData, string animName) : base(anubis, stateMachine, playerData, animName)
		{
		}

		public override void CheckPhysics()
		{
			base.CheckPhysics();

			isGrounded = anubis.CheckIfGrounded();	
		}

		public override void Enter()
		{
			base.Enter();

			abilityDone = false;
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (abilityDone) {
				if (isGrounded && anubis.GetVelocity().y <= 0.0001)
					stateMachine.ChangeState(anubis.IdleState);
				else
					stateMachine.ChangeState(anubis.InAirState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}

