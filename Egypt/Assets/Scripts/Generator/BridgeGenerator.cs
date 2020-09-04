
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeGenerator : Generator
{
	[SerializeField] int numberOfVariants = 1;

	public static int size = 44;

	public override void Generate(Vector2Int pos) {
		GameObject ground = Thuleanx.Preset.ObjectPool.Instance.Instantiate(
			"Bridge" + Thuleanx.Math.Random.Range(0, numberOfVariants - 1).ToString(), 
			(Vector2) pos, 
			Quaternion.identity
		);
	}
}

