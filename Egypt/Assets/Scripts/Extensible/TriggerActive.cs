

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActive: MonoBehaviour
{
	public GameObject obj;
	public bool defaultActive;

	void OnEnable() {
		if (defaultActive)	
			Activate();
		else Disable();
	}

	public void Activate() => obj.SetActive(true);
	public void Disable() => obj.SetActive(false);
}
