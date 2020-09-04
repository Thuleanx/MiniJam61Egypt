using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx
{
	[RequireComponent(typeof(PlayerInputHandler))]
	public class Player : Agent
	{
		public PlayerData playerData;

		#region State Variables
		public PlayerStateMachine StateMachine { get; private set; }

		public PlayerIdleState IdleState { get; private set; }
		public PlayerMoveState MoveState { get; private set; }
		public PlayerJumpState JumpState { get; private set; }
		public PlayerInAirState InAirState { get; private set; }
		public PlayerLandState LandState {get; private set; }
		public PlayerAttackState AttackState { get; private set; }
		#endregion

		#region Components
		public PlayerInputHandler InputHandler { get; private set; }
		public Transform Hand { get; private set; }
		#endregion

		#region Unity Callback Functions
		public override void Awake() {
			base.Awake();
			playerData = Thuleanx.IO.JSONReader.Parse<PlayerData>("Data/Player");

			StateMachine = new PlayerStateMachine();

			IdleState = new PlayerIdleState(this, StateMachine, playerData, "Idle");
			MoveState = new PlayerMoveState(this, StateMachine, playerData, "Run");
			JumpState = new PlayerJumpState(this, StateMachine, playerData, "InAir");
			InAirState = new PlayerInAirState(this, StateMachine, playerData, "InAir");
			LandState = new PlayerLandState(this, StateMachine, playerData, "Idle");
			AttackState = new PlayerAttackState(this, StateMachine, playerData, "Attack");

			InputHandler = GetComponent<PlayerInputHandler>();
			Hand = GameObject.FindGameObjectWithTag("PlayerHand").transform;
		}

		void Start() {
			StateMachine.Init(IdleState);
			Status.Init(playerData.damage, playerData.health, playerData.knockback);
		}

		void Update() {
			StateMachine.CurrentState.LogicUpdate();
		}

		void FixedUpdate() {
			StateMachine.CurrentState.PhysicsUpdate();
		}

		#endregion


		#region Animations
		public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
		public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
		#endregion
	}
}