


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
	public float Range = 12f;
	public float OrthoSize = 9f;

	ZoomToPOI cameraZoom;

	void Awake() {
		cameraZoom = Camera.main.GetComponent<ZoomToPOI>();
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player"))
			cameraZoom?.AttachPOI(this);
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player"))
			cameraZoom?.Detach();
	}
}
