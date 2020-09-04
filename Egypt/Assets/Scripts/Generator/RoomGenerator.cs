
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : Generator
{
	public static Vector2Int roomSize = new Vector2Int(24, 16);
	public static Vector2Int presetSize = new Vector2Int(8, 8);

	[Header("Room Presets")]
	public int[] numberOfVariants = new int[6];

	public override void Generate(Vector2Int pos) {
		int p = 0;
		for (int y = 0; y < roomSize.y; y += presetSize.y) {
			for (int x = 0; x < roomSize.x; x += presetSize.x) {
				Vector2Int location = pos + x * Vector2Int.right + y * Vector2Int.up;
				string preset = "Room" + p.ToString() + "-" + Thuleanx.Math.Random.Range(0, numberOfVariants[p] - 1).ToString();
				GameObject quadrant = Thuleanx.Preset.ObjectPool.Instance.Instantiate(preset, (Vector2) location, Quaternion.identity);
				p++;
			}
		}
	}
}

