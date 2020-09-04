

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thuleanx;

public class ReflectByFacingDirection : MonoBehaviour
{
	[SerializeField] bool flipX;

	Player player;
	Vector2 pos;

	void Start() {
		player = GetComponentInParent<Player>();
		pos = transform.localPosition;
	}

	void Update() {
		transform.localPosition = new Vector3(
			pos.x * player.FacingDirection * (flipX ? -1 : 1),
			pos.y,
			transform.position.z
		);
	}
}
