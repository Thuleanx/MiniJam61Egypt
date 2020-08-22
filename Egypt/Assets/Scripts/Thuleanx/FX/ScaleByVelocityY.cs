using UnityEngine;

namespace Thuleanx.FX {
	public class ScaleByVelocityY : MonoBehaviour
	{
		[HideInInspector]
		public new Rigidbody2D rigidbody;

		[Header("Spring Mass Damper")]
		public float springConstant = 1;
		public float dampingConstant = 0;
		public float distanceCapMin = 0.5f, distanceCapMax = 1.5f;
		public float capVelocityWhenOutBounds = 2f;

		[Header("Squash And Stretch")]
		public float strength = .1f;
		public float convergenceTime = .3f;

		RigidBody1D stretch = new RigidBody1D(1f);

		private Vector2 startScale;

		private void Start()
		{
			rigidbody = GetComponentInParent<Rigidbody2D>();
			startScale = transform.localScale;
		}

		private void Update()
		{
			var velocity = Mathf.Abs(rigidbody.velocity.y);
			if (velocity < .001f) 
				velocity = 0;

			// spring damper
			var force = -springConstant * (stretch.Position - 1) - dampingConstant * stretch.Velocity;

			if (!Mathf.Approximately(velocity, 0)) {
				var amount = velocity * strength + 1;
				stretch.Position = amount;
				force = 0;
			}

			stretch.SetForce(force);
			stretch.Update();

			// cap distance
			float clampedValue = Mathf.Clamp(stretch.Position, distanceCapMin, distanceCapMax);
			if (Mathf.Approximately(clampedValue, distanceCapMin) || Mathf.Approximately(clampedValue, distanceCapMax))
				stretch.SetVelocity(Mathf.Clamp(stretch.Velocity, -capVelocityWhenOutBounds, capVelocityWhenOutBounds));
			stretch.Position = clampedValue;

			var inverseAmount = (1f / stretch.Position) * startScale.magnitude;

			transform.localScale = new Vector3(inverseAmount, stretch.Position, 1f);
		}
	}
}