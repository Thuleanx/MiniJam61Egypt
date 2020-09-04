
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Thuleanx;

public class KeyPickup : MonoBehaviour
{
	public int Heal = 2;

	void Awake() {
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Agent agent = collision.gameObject.GetComponent<Agent>();
		if (agent != null) {
			Status status = agent.Status;
			if (status.Health != status.maxHealth) {
				status.Health += Heal;
				gameObject.SetActive(false);
				AudioManager.Instance.Play("HeartPickup");
			}
		}
	}
}
