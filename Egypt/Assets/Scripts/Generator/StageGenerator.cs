
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : Generator
{
	#region Components
	GroundGenerator ground;
	RoomGenerator room;
	BridgeGenerator bridge;
	Transform player;
	#endregion


	[SerializeField] float generateAhead = 50;
	[SerializeField] Vector2Int generatedUpTo = new Vector2Int(0, 0);

	public override void Generate(Vector2Int pos) {
		ground.Generate(pos);
		bridge.Generate(pos + Vector2Int.right * RoomGenerator.roomSize.x);
		room.Generate(pos);
	}

	void Awake() {
		ground = GetComponent<GroundGenerator>();
		room = GetComponent<RoomGenerator>();
		bridge = GetComponent<BridgeGenerator>();

		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update() {
		while (generatedUpTo.x < player.position.x + generateAhead) {
			Generate(generatedUpTo);
			generatedUpTo.x += RoomGenerator.roomSize.x + BridgeGenerator.size;
		}
	}
}

