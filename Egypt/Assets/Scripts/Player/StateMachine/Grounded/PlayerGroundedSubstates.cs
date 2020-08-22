

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx {
	public class PlayerIdleState : PlayerGroundState
	{
		public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
		{

		}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (stateMachine.CurrentState == this) {
				if (input.x != 0)
					stateMachine.ChangeState(player.MoveState);
				else 
					player.SetVelocityX(0);
			}
		}
	}

	public class PlayerMoveState : PlayerGroundState
	{
		public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
		{
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			if (stateMachine.CurrentState == this) {
				if (input.x == 0)
					stateMachine.ChangeState(player.IdleState);
				else
				{
					player.CheckIfShouldFlip(player.InputHandler.MovementInput.x);
					player.SetVelocityX(playerData.baseSpeed * Mathf.Sign(input.x));
				}
			}
		}
	}

	public class PlayerLandState : PlayerGroundState
	{
		public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
		{
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (stateMachine.CurrentState == this) {
				if (input.x != 0) {
					stateMachine.ChangeState(player.MoveState);
				} else if (isAnimationFinished) {
					stateMachine.ChangeState(player.IdleState);
				}
			}
		}
	}
}
