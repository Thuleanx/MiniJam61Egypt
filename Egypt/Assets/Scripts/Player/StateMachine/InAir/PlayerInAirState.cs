using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thuleanx {
	public class PlayerInAirState : PlayerState
	{
		protected bool isGrounded;
		protected bool coyote;
		float jumpVelocityMin;
		bool isJumping;

		public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
		{
			float gravity = Mathf.Abs(Physics2D.gravity.y);
			float timeToJumpApex = Mathf.Sqrt(2 * playerData.jumpHeightMinimum / gravity);
			jumpVelocityMin = gravity * timeToJumpApex;
		}

		public override void CheckPhysics()
		{
			base.CheckPhysics();

			isGrounded = player.CheckIfGrounded();
		}

		public override void Enter()
		{
			base.Enter();
		}

		public override void Exit()
		{
			base.Exit();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			CheckCoyoteTime();

			if (stateMachine.CurrentState == this) {
				CheckVariableJump();

				if (isGrounded && player.Body.velocity.y <= 0.0001f)
					stateMachine.ChangeState(player.LandState);
				else if (coyote && player.InputHandler.JumpInput)
				{
					player.InputHandler.UseJumpInput();
					stateMachine.ChangeState(player.JumpState);
				} else
				{
					player.CheckIfShouldFlip(player.InputHandler.MovementInput.x);
					player.SetVelocityX(playerData.baseSpeed * player.InputHandler.MovementInput.x);
				}
			}
		}

		public override void PhysicsUpdate()
		{
			base.PhysicsUpdate();
		}

		void CheckVariableJump() {
			if (isJumping)
			{
				if (player.InputHandler.JumpReleased) {
					player.SetVelocityY(Mathf.Min(player.Body.velocity.y, jumpVelocityMin));
					isJumping = false;
				}
				else if (player.Body.velocity.y <= 0f)
					isJumping = false;
			}
		}

		void CheckCoyoteTime() {
			if (coyote && startTime + playerData.coyoteTime < Time.time)
				coyote = false;
		}

		public void StartCoyoteTime() {
			coyote = true;
		}

		public void StartJump() {
			isJumping = true;
		}
	}
}