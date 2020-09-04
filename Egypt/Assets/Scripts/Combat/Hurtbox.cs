using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Thuleanx.Math;

[RequireComponent(typeof(BoxCollider2D))]
public class Hurtbox : MonoBehaviour
{
	// Attached to a status
	
	public Status Status {get; private set; }
	BoxCollider2D box;
	Timers timers;

	void Awake() {
		timers = new Timers();
		timers.RegisterTimer("iframe");
		box = GetComponent<BoxCollider2D>();
		Status = GetComponentInParent<Status>();
	}

	public float RegisterHit(float damage, Hitbox hitbox)
	{
		if (canBeHit()) {
			Status.DealDamage(damage);
			Status.OnGettingHit(hitbox);
			return damage;
		}
		return 0;
	}

	public void GiveIframe(float duration) {
		timers.StartTimer("iframe", duration);
	}

	public bool canBeHit()
	{
		return !timers.ActiveAndNotExpired("iframe");
	}
}
