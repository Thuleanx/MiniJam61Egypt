


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventOnContact : MonoBehaviour
{
	public UnityEvent onTrigger;
	bool interacted;

	void OnEnable() {
		interacted = false;
	}
	
	void OnTriggerEnter2D(Collider2D collision) {
		if (!interacted && collision.gameObject.CompareTag("Player"))
			onTrigger.Invoke();
		interacted = true;
	}
}
