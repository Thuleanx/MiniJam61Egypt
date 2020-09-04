

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx.Math;

public class Spawner : MonoBehaviour {

	public static Spawner Instance {get; private set; }

	Timers timers;	

	List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

	[HideInInspector]
	public int credits;

	[HideInInspector]
	public int numberOfMonsters;

	void Awake() {
		Instance = this;
		timers = new Timers();
		timers.RegisterTimer("SpawnCD");
		Terminate();
	}

	public void AddSpawnPoint(SpawnPoint point) {
		spawnPoints.Add(point);
	}

	public void Terminate() {
		foreach (var point in spawnPoints)	
			point.gameObject.SetActive(false);
		spawnPoints = new List<SpawnPoint>();
		Active = false;
	}

	public void Activate() => Active = true;

	public bool Active { get; private set; }

	bool CanSpawn() => !timers.ActiveAndNotExpired("SpawnCD") && credits > 0;

	void Update() {
		if (Active && CanSpawn()) {
			if (Spawn()) {
				credits--;
				timers.StartTimer("SpawnCD", 1f / SpawnInfo.Info.spawnFrequency);
			}
		}
	}

	bool Spawn() {
		if (spawnPoints.Count > 0) {
			GameObject mob = Thuleanx.Preset.ObjectPool.Instance.Instantiate(
				SpawnInfo.Info.monsterTags[0], 
				spawnPoints[Thuleanx.Math.Random.Range(0, spawnPoints.Count - 1)].transform.position,
				Quaternion.identity
			);
			numberOfMonsters++;
			return true;
		}
		return false;
	}
}