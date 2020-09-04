

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnCollision : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision) {
		gameObject.SetActive(false);
	}
}
