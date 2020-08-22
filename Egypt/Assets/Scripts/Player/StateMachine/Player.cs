using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx
{
	[RequireComponent(typeof(PlayerInputHandler))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class Player : MonoBehaviour
	{
		public PlayerData playerData;

		#region State Variables
		public PlayerStateMachine StateMachine { get; private set; }

		public PlayerIdleState IdleState { get; private set; }
		public PlayerMoveState MoveState { get; private set; }
		public PlayerJumpState JumpState { get; private set; }
		public PlayerInAirState InAirState { get; private set; }
		public PlayerLandState LandState {get; private set; }
		#endregion

		#region Components
		public PlayerInputHandler InputHandler { get; private set; }
		public Rigidbody2D Body { get; private set; }
		public SpriteRenderer Sprite {get; private set; }
		#endregion

		#region Check Variables

		[Header("Check variables")]
		[SerializeField] Transform groundCheck;
		[SerializeField] float groundCheckRadius = .3f; 
		[SerializeField] LayerMask whatIsGround;

		#endregion	

		#region Misc Variables 
		[HideInInspector]
		public float FacingDirection = 1;
		[Header("Animations")]
		public bool FlipX;
		#endregion

		#region Unity Callback Functions
		void Awake() {
			playerData = Thuleanx.IO.JSONReader.Parse<PlayerData>("/Data/Player.json");

			StateMachine = new PlayerStateMachine();

			IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
			MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
			JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
			InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
			LandState = new PlayerLandState(this, StateMachine, playerData, "land");


			InputHandler = GetComponent<PlayerInputHandler>();
			Body = GetComponent<Rigidbody2D>();
			Sprite = GetComponentInChildren<SpriteRenderer>();
		}

		void Start() {
			StateMachine.Init(IdleState);
		}

		void Update() {
			StateMachine.CurrentState.LogicUpdate();
		}

		void FixedUpdate() {
			StateMachine.CurrentState.PhysicsUpdate();
		}

		#endregion

		#region Setters
		public void SetVelocityX(float value) {
			Body.velocity = new Vector2(value, Body.velocity.y);
		}
		public void SetVelocityY(float value) {
			Body.velocity = new Vector2(Body.velocity.x, value);
		}
		public void SetVelocity(Vector2 value) {
			Body.velocity = value;
		}
		#endregion

		#region Check Functions

		public bool CheckIfGrounded() {
			return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		}

		public void CheckIfShouldFlip(float xInput) {
			if (xInput != 0) {
				FacingDirection = Mathf.Sign(xInput);
				Sprite.flipX = FacingDirection > 0 ^ FlipX;
			}
		}
		#endregion

		#region Animations
		public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
		public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
		#endregion
	}
}