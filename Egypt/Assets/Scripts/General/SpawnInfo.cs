
public class SpawnInfo {
	public string[] monsterTags;			
	public int[] numberOfMonsters;
	public float spawnFrequency;

	public static SpawnInfo Info = 
		Thuleanx.IO.JSONReader.Parse<SpawnInfo>("Data/SpawnInfo");
}