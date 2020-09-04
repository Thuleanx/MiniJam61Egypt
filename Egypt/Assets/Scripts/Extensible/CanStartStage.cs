using UnityEngine;

public class CanStartStage : MonoBehaviour {
	public void StartStage() {
		StageManager.Instance.StartStage();
	}
}