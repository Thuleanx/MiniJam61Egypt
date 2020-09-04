using System.Collections;
using UnityEngine;

public class MaterialUpdatePosition : MonoBehaviour {
	void Update() {
		Shader.SetGlobalVector("_Player_Position", transform.position);
	}
}