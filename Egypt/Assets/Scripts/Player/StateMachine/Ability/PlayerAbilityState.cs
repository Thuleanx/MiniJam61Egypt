using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thuleanx {
	public class PlayerAbilityState : PlayerState
	{
		protected bool abilityDone;
		protected bool isGrounded;

		public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
		{
		}

		public override void CheckPhysics()
		{
			base.CheckPhysics();

			isGrounded = player.CheckIfGrounded();	
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
				if (isGrounded && player.Body.velocity.y <= 0.0001)
					stateMachine.ChangeState(player.IdleState);
				else
					stateMachine.ChangeState(player.InAirState);
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}
	}
}

