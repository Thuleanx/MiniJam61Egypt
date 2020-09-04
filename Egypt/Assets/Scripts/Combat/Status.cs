
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx.FX;
using Thuleanx.Math;
using Thuleanx;

using UnityEngine.SceneManagement;

public class Status : MonoBehaviour
{
	[HideInInspector]
	public float damage, health, maxHealth, knockback;

	TurnWhiteShader shader;
	IncrementalTimers itimers;

	public virtual void Awake() {
		itimers = new IncrementalTimers();
		shader = GetComponentInChildren<TurnWhiteShader>();
	}

	public void Init(float baseDamage, float baseHealth, float knockback) {
		// damage health maxhealth
		damage = baseDamage;
		maxHealth = baseHealth;
		health = maxHealth;
		this.knockback = knockback;
	}

	void OnEnable() {
		health = maxHealth;
	}

	public float Health {
		get { return health; }
		set { 
			health = Mathf.Clamp(value, 0, maxHealth); 
		}
	}

	public void CommitDie() {
		if (!CompareTag("Player"))
			Spawner.Instance.numberOfMonsters--;
		else {
			// transition to next scene
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			AudioManager.Instance.Play("Fail");
		}
		gameObject.SetActive(false);
	}

	public virtual void DealDamage(float damage) {
		// health
		Health -= damage;
		if (Health == 0)
			CommitDie();

		if (CompareTag("Player")) 
			AudioManager.Instance.Play("HurtSelf");
		else
			AudioManager.Instance.Play("HurtEnemy");
	}

	public virtual void OnHit(Hurtbox hurtbox, float damage, Hitbox hitbox) {

	}

	public virtual void OnGettingHit(Hitbox hitbox) {
		shader?.TurnWhite(.05f);
		float knockbackDist = Mathf.Sign(transform.position.x - hitbox.transform.position.x) * hitbox.status.knockback;
		GetComponent<Agent>().ApplyKnockBackX(knockbackDist);	
	}
}
