
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx {
	public class PlayerGroundState : PlayerState
	{
		protected Vector2 input;

		bool isGrounded;

		public PlayerGroundState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
		{
		}

		public override void CheckPhysics() {
			base.CheckPhysics();

			isGrounded = player.CheckIfGrounded();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			input = player.InputHandler.MovementInput;
			if (player.InputHandler.JumpInput) {
				player.InputHandler.UseJumpInput();
				stateMachine.ChangeState(player.JumpState);
			} else if (!isGrounded) {
				player.InAirState.StartCoyoteTime();
				stateMachine.ChangeState(player.InAirState);
			} else if (player.InputHandler.AttackInput && player.AttackState.CanAttack())
				stateMachine.ChangeState(player.AttackState);
		}
	}
}
