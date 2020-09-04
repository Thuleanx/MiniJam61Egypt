
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx.Math;

public class StageManager : MonoBehaviour {

	public static StageManager Instance {get; private set; }

	public int Stage {get; private set; }

	void Awake() {
		Instance = this;
		Stage = -1;
	}

	public void StartStage() {
		Stage++;
		Spawner.Instance.Activate();	
		Spawner.Instance.credits = SpawnInfo.Info.numberOfMonsters[Mathf.Clamp(Stage, 0, SpawnInfo.Info.numberOfMonsters.Length-1)];
	}

	public void EndStage() {
		Spawner.Instance.Terminate();
		// open door to next stage
		GameObject.FindGameObjectWithTag("FogGateNext").GetComponent<TriggerActive>().Disable();
		GameObject.FindGameObjectWithTag("FogGateNext").gameObject.SetActive(false);
	}

	void Update() {
		if (Spawner.Instance.Active && Spawner.Instance.credits == 0 && Spawner.Instance.numberOfMonsters == 0) {
			EndStage();
			AudioManager.Instance.Play("Finish");
		}
	}
}