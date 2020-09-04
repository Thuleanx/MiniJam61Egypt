using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx {

	public class Anubis : Agent {
		public AnubisData anubisData;		

		#region State Variables
		public AnubisStateMachine StateMachine {get; private set;}

		public AnubisIdleState IdleState { get; private set;}
		public AnubisMoveState MoveState { get; private set;}
		public AnubisLandState LandState { get; private set;}
		public AnubisJumpState JumpState { get; private set;}
		public AnubisInAirState InAirState { get; private set;}
		#endregion

		#region Components
		public Transform Player { get; private set; }
		#endregion


		public override void Awake() {
			base.Awake();
			anubisData = Thuleanx.IO.JSONReader.Parse<AnubisData>(
				"Data/Anubis"
			);

			StateMachine = new AnubisStateMachine();

			IdleState = new AnubisIdleState(this, StateMachine, anubisData, "Idle");
			MoveState = new AnubisMoveState(this, StateMachine, anubisData, "Run");
			LandState = new AnubisLandState(this, StateMachine, anubisData, "Idle");
			JumpState = new AnubisJumpState(this, StateMachine, anubisData, "InAir");
			InAirState = new AnubisInAirState(this, StateMachine, anubisData, "InAir");

			Player = GameObject.FindGameObjectWithTag("Player").transform;
		}

		void Start() {
			StateMachine.Init(IdleState);
			Status.Init(anubisData.damage, anubisData.health, anubisData.knockback);
		}

		void Update() {
			StateMachine.CurrentState.LogicUpdate();
		}

		void FixedUpdate() {
			StateMachine.CurrentState.PhysicsUpdate();
		}

		#region Animations
		public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
		public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
		#endregion
	}
}