
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

namespace Thuleanx
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Status))]
	[RequireComponent(typeof(Collider2D))]
	public class Agent : MonoBehaviour {

		#region Check variables 
		[Header("Check variables")]
		[SerializeField] protected Transform groundCheck;
		[SerializeField] protected float groundCheckRadius = .3f; 
		[SerializeField] protected LayerMask whatIsGround;
		#endregion

		#region Components
		public Rigidbody2D Body { get; private set; }
		public SpriteRenderer Sprite {get; private set; }
		public Animator Anim { get; private set; }
		public Status Status { get; private set; }
		public Collider2D Collider { get; private set; }
		#endregion

		#region Misc Variables 
		[HideInInspector]
		public float FacingDirection = 1;
		[Header("Animations")]
		public bool FlipX;

		[Header("Raycast")]
		[Range(2, 16)]
		public int RaycastNumberOnKnockback = 8;
		#endregion

		#region Unity Callbacks
		public virtual void Awake() {
			Body = GetComponent<Rigidbody2D>();
			Anim = GetComponentInChildren<Animator>();
			Sprite = GetComponentInChildren<SpriteRenderer>();
			Status = GetComponent<Status>();
			Collider = GetComponent<Collider2D>();
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

		#region Getters
		public Vector2 GetVelocity() => Body.velocity;
		#endregion

		#region Setters
		public void SetVelocityX(float value) => Body.velocity = new Vector2(value, Body.velocity.y);
		public void SetVelocityY(float value) => Body.velocity = new Vector2(Body.velocity.x, value);
		public void SetVelocity(Vector2 value) => Body.velocity = value;

		public void ApplyKnockBackX(float dist) {
			Bounds bounds = Collider.bounds;

			Vector2 origin = new Vector2(dist < 0 ? bounds.min.x : bounds.max.x, bounds.min.y);
			float spacing = (bounds.max.y - bounds.min.y) / (RaycastNumberOnKnockback - 1);
			for (int i = 0; i < RaycastNumberOnKnockback; i++)
			{
				RaycastHit2D hit = Physics2D.Raycast(origin + Vector2.up * spacing * i, Vector2.right * Mathf.Sign(dist), Mathf.Abs(dist), whatIsGround);

				if (hit) {
					dist = hit.distance * Mathf.Sign(dist);
				}
			}
			if (!Mathf.Approximately(0, dist))
				Body.MovePosition(Body.position + Vector2.right * dist);
		}
		#endregion
	}
}