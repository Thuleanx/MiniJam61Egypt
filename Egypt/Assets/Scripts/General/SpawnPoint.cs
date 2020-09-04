

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
	static float SpawnPointRange = 10f;
	Transform player;

	bool detected = false;

	void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void OnEnable() {
		detected = false;
	}

	void Update() {
		if (!detected && ((Vector2)( transform.position - player.position)).magnitude < SpawnPointRange) {
			Spawner.Instance.AddSpawnPoint(this);
			detected = true;
		}
	}
}