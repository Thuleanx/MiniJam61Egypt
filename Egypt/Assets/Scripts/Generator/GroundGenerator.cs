
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : Generator
{
	[SerializeField] int numberOfVariants = 1;

	public override void Generate(Vector2Int pos) {
		GameObject ground = Thuleanx.Preset.ObjectPool.Instance.Instantiate(
			"Ground" + Thuleanx.Math.Random.Range(0, numberOfVariants - 1).ToString(), 
			(Vector2) pos, 
			Quaternion.identity
		);
	}
}

