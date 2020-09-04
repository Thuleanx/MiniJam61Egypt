using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx {
	public class AnubisState
	{
		protected Anubis anubis;
		protected AnubisStateMachine stateMachine;
		protected AnubisData anubisData;

		protected float startTime;
		protected string animName;

		protected bool isAnimationFinished;
		protected bool playerSighted;
		protected Vector2 playerDirection;

		public AnubisState(Anubis anubis, AnubisStateMachine stateMachine, AnubisData anubisData, string animName) {
			this.stateMachine = stateMachine;
			this.anubisData = anubisData;
			this.animName = animName;
			this.anubis = anubis;
		}

		public virtual void Enter() {
			CheckPhysics();
			startTime = Time.time;
			anubis.Anim.SetBool(animName, true);
			isAnimationFinished = false;
		}
		public virtual void Exit() {
			anubis.Anim.SetBool(animName, false);
		}
		public virtual void LogicUpdate() {
			CheckPhysics();	
		}
		public virtual void PhysicsUpdate() {
			CheckPhysics();
		}
		public virtual void CheckPhysics() {
			playerSighted = anubis.anubisData.visionRange > ((Vector2) (anubis.Player.position - anubis.transform.position)).magnitude;
			playerDirection = anubis.Player.position - anubis.transform.position;
			anubis.CheckIfShouldFlip(anubis.GetVelocity().x);
		}

		public virtual void AnimationTrigger() {}
		public virtual void AnimationFinishTrigger() {
			isAnimationFinished = true;
		}
	}
}
