using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx {
	public class PlayerState
	{
		protected Player player;
		protected PlayerStateMachine stateMachine;
		protected PlayerData playerData;

		protected float startTime;
		protected string animName;

		protected bool isAnimationFinished;

		public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animName) {
			this.stateMachine = stateMachine;
			this.playerData = playerData;
			this.animName = animName;
			this.player = player;
		}

		public virtual void Enter() {
			CheckPhysics();
			startTime = Time.time;
			player.Anim.SetBool(animName, true);
			isAnimationFinished = false;
		}
		public virtual void Exit() {
			player.Anim.SetBool(animName, false);
		}
		public virtual void LogicUpdate() {}
		public virtual void PhysicsUpdate() {
			CheckPhysics();
		}
		public virtual void CheckPhysics() {}

		public virtual void AnimationTrigger() {}
		public virtual void AnimationFinishTrigger() {
			isAnimationFinished = true;
		}
	}
}
