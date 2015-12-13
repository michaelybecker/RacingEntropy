using UnityEngine;
using System.Collections;

// This class stores level data and includes level generation algorithms.
// Goal = 8.

public class Cartographer : MonoBehaviour {

	private int[,] mapData;

	public float spacing;

	//public bool debugBuild; // For testing absent supporting scripts.
	public int debugSize;

	public TileManager manager;

	public float noise;

	/*public void Awake () 
	{
		if (debugBuild)
			GenerateBiomes (debugSize, debugSize);
		if (manager == null)
			manager = gameObject.GetComponent<TileManager>();
	}*/

	public void GenerateDifficulty(int level) 
	{
		noise = 0.1f;
		GenerateRandom (10*level,10*level);
	}

	// Basic functionality: generate a completely random level of X width and Y height.
	public void GenerateRandom (int width, int height) {

		mapData = new int[width, height];

		for (int why = 0; why < height; why++) {
			for (int ecks = 0; ecks < width; ecks++) {
				// Set each tile to a completely random type.
				mapData[ecks, why] = Random.Range (0, 7);
				
			}
		}

		// Designate start location and goal.
		int placementRange = debugSize/5;
		int minPlace = 1;
		if (placementRange <= 1)
			minPlace = 0;

		int endX = Random.Range(width-placementRange, width-minPlace);
		int endY = Random.Range(height-placementRange, height-minPlace);

		mapData[endX, endY] =  7;

		manager.CreateMap(mapData);

		int startX = Random.Range(minPlace, placementRange);
		int startY = Random.Range(minPlace, placementRange);

		manager.AddPlant(startX, startY); // Single tile start for now.
	}

	// Second layer of functionality: generate different clumps of terrain based on moisture and elevation maps.
	// PRO: using the Unity PerlinNoise function is easy.
	// CON: I can't modify how the noise is generated in order to generate good and/or reasonable puzzles.
	public void GenerateBiomes (int width, int height) {
		
		mapData = new int[width, height];

		int[,] elevData = new int[width, height];
		int[,] precipData = new int[width, height];
		
		// The double perlin?  Probably.  At least double.  Maybe for each element?  I need combinations, so math time.
		float elevEcks = Random.Range (0f, 500f);
		float elevWhy = Random.Range (0f, 500f);

		float precipEcks = Random.Range (0f, 500f);
		float precipWhy = Random.Range (0f, 500f);

		for (int why = 0; why < height; why++) {
			for (int ecks = 0; ecks < width; ecks++) {
				// Need to generate more extreme data here.
				float noiseEcks = ((float)ecks)*noise;
				float noiseWhy = ((float)why)*noise;


				elevData[ecks, why] = Mathf.RoundToInt(7f*Mathf.PerlinNoise(elevEcks+noiseEcks, elevWhy+noiseWhy));
				precipData[ecks, why] = Mathf.RoundToInt(3f*Mathf.PerlinNoise(precipEcks+noiseEcks, precipWhy+noiseWhy));

				// This feels like a horrible, horrible way to do this.
				if (elevData[ecks, why] <= 2) {
					if (precipData[ecks, why] == 0) {
						mapData[ecks, why] = 1;
					} else if (precipData[ecks, why] == 1) {
						mapData[ecks, why] = 3;
					} else if (precipData[ecks, why] == 2) {
						mapData[ecks, why] = 3;
					} else { // Assume 3.
						mapData[ecks, why] = 3;
					}
				} else if (elevData[ecks, why] == 3) {
					if (precipData[ecks, why] == 0) {
						mapData[ecks, why] = 0;
					} else if (precipData[ecks, why] == 1) {
						mapData[ecks, why] = 5;
					} else if (precipData[ecks, why] == 2) {
						mapData[ecks, why] = 2;
					} else { // Assume 3.
						mapData[ecks, why] = 1;
					}
				} else if (elevData[ecks, why] == 4) {
					if (precipData[ecks, why] == 0) {
						mapData[ecks, why] = 4;
					} else if (precipData[ecks, why] == 1) {
						mapData[ecks, why] = 6;
					} else if (precipData[ecks, why] == 2) {
						mapData[ecks, why] = 5;
					} else { // Assume 3.
						mapData[ecks, why] = 2;
					}
				} else { // Assume 3.
					if (precipData[ecks, why] == 0) {
						mapData[ecks, why] = 4;
					} else if (precipData[ecks, why] == 1) {
						mapData[ecks, why] = 4;
					} else if (precipData[ecks, why] == 2) {
						mapData[ecks, why] = 4;
					} else { // Assume 3.
						mapData[ecks, why] = 6;
					}
				}
				// End states

			}
		}
		// End loops

		int placementRange = debugSize/5;
		int minPlace = 1;
		if (placementRange <= 1)
			minPlace = 0;

		int endX = Random.Range(width-placementRange, width-minPlace);
		int endY = Random.Range(height-placementRange, height-minPlace);

		mapData[endX, endY] =  7;

		manager.CreateMap(mapData);

		int startX = Random.Range(minPlace, placementRange);
		int startY = Random.Range(minPlace, placementRange);

		manager.AddPlant(startX, startY); // Single tile start for now.
		
	}
}
