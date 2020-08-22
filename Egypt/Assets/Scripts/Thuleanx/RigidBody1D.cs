using UnityEngine; 

public class RigidBody1D
{
	public float Position = 0;
	public float Velocity = 0;
	public float Acceleration = 0;

	public RigidBody1D(float pos) {
		Position = pos;
	}

	public void SetForce(float force)
	{
		Acceleration = force;
	}

	public void SetVelocity(float velocity) {
		this.Velocity = velocity;
	}

	public void Update() {
		Velocity += Acceleration * Time.deltaTime;
		Position += Velocity * Time.deltaTime;
	}
}