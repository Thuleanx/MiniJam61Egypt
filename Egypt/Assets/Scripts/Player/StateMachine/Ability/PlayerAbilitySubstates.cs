
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Thuleanx {
	public class PlayerJumpState : PlayerAbilityState
	{
		float jumpVelocity;

		public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
		{
			float gravity = Mathf.Abs(Physics2D.gravity.y);
			float timeToJumpApex = Mathf.Sqrt(2 * playerData.jumpHeight / gravity);
			jumpVelocity = gravity * timeToJumpApex;
		}

		public override void Enter()
		{
			base.Enter();

			player.SetVelocityY(jumpVelocity);
			player.InAirState.StartJump();

			abilityDone = true;
		}
	}
}

