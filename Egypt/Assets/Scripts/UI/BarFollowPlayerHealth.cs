

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFollowPlayerHealth : BarFill
{
	Status playerStatus;
	float lastHealth;

	void Start() {
		playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<Status>();
	}

	void Update() {
		if (playerStatus.Health != lastHealth)
			SetFill((float) playerStatus.Health / playerStatus.maxHealth);
		lastHealth = playerStatus.Health;
	}
}
