
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx {
	public class AnubisIdleState : AnubisGroundState
	{
		public AnubisIdleState(Anubis anubis, AnubisStateMachine stateMachine, AnubisData playerData, string animName) : base(anubis, stateMachine, playerData, animName)
		{
		}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (stateMachine.CurrentState == this) {
				if (anubis.GetVelocity() != Vector2.zero)
					stateMachine.ChangeState(anubis.MoveState);
			}
		}
	}

	public class AnubisMoveState : AnubisGroundState
	{
		public AnubisMoveState(Anubis anubis, AnubisStateMachine stateMachine, AnubisData playerData, string animName) : base(anubis, stateMachine, playerData, animName)
		{
		}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (stateMachine.CurrentState == this) {
				if (anubis.GetVelocity() == Vector2.zero)
					stateMachine.ChangeState(anubis.IdleState);
				else {
					anubis.CheckIfShouldFlip(anubis.GetVelocity().x);
				}
			}
		}
	}

	public class AnubisLandState : AnubisGroundState
	{
		public AnubisLandState(Anubis anubis, AnubisStateMachine stateMachine, AnubisData playerData, string animName) : base(anubis, stateMachine, playerData, animName)
		{
		}

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (stateMachine.CurrentState == this) {
				if (anubis.GetVelocity().x != 0)
					stateMachine.ChangeState(anubis.MoveState);
				else if (isAnimationFinished) {
					stateMachine.ChangeState(anubis.IdleState);
				}
			}
		}
	}
}
