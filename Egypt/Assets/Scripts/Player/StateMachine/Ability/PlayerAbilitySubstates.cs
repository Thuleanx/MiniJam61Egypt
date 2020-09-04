
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Thuleanx.Math;

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

			AudioManager.Instance.Play("Jump");

			abilityDone = true;
		}
	}

	public class PlayerAttackState : PlayerAbilityState
	{
		Timers timers;

		public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) : base(player, stateMachine, playerData, animName)
		{
			timers = new Timers();
			timers.RegisterTimer("attackCD");
		}

		public override void Enter() {
			base.Enter();
			timers.StartTimer("attackCD", player.playerData.attackCooldownSeconds);
		}

		public override void AnimationTrigger() {
			GameObject projectile = Thuleanx.Preset.ObjectPool.Instance.Instantiate("PlayerAttack", player.Hand.position, Quaternion.identity);		
			projectile.GetComponent<Rigidbody2D>().velocity = Vector2.right * player.FacingDirection * player.playerData.attackProjectileSpeed;
			// placeholder
			projectile.GetComponentInChildren<Hitbox>().AttachStatus(player.Status);
			AudioManager.Instance.Play("Attack");
		}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (stateMachine.CurrentState == this) {
				if (isAnimationFinished)
					stateMachine.ChangeState(player.CheckIfGrounded() ? (PlayerState) player.IdleState : (PlayerState) player.InAirState);
				else {
					float inputX = player.InputHandler.MovementInput.x;
					if (inputX != 0)
						player.SetVelocityX(playerData.baseSpeed * Mathf.Sign(inputX));
					if (player.CheckIfGrounded() && player.InputHandler.JumpInput) {
						player.InputHandler.UseJumpInput();
						stateMachine.ChangeState(player.JumpState);
					}
				}
			}
		}

		// Exit on finish
		public override void AnimationFinishTrigger() {
			stateMachine.ChangeState(player.CheckIfGrounded() ? (PlayerState) player.IdleState : (PlayerState) player.InAirState);
		}

		public bool CanAttack() {
			return !timers.ActiveAndNotExpired("attackCD");
		}
	}
}

