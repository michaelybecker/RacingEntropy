using UnityEngine;
using System.Collections;

// This class stores level data and includes level generation algorithms.
// Goal = 8.

public class Cartographer : MonoBehaviour {

	private int[,] mapData;

	public float spacing;

	public bool debugBuild; // For testing absent supporting scripts.
	public int debugSize;
	public int debugDifficulty;

	private int currentSize;

	public TileManager manager;

	public float noise;

	public void Awake () {
		if (debugBuild)
			BuildDifficulty (debugDifficulty);
		if (manager == null)
			manager = gameObject.GetComponent<TileManager>();
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

		mapData[endX, endY] = 7;

		int startX = Random.Range(minPlace, placementRange);
		int startY = Random.Range(minPlace, placementRange);
		mapData[startX,startY] = (int)TileType.tile.PLAIN;

		manager.CreateMap(mapData);

		manager.AddPlant(startX, startY); // Single tile start for now.
		
	}

	// Build a level based on the provided difficulty value.
	// Higher difficulty means more disasters and a larger size.
	// Might also randomize noise slightly.
	public void BuildDifficulty(int difficulty) {
		// Let's just handle this bad input case here:
		//while (difficulty <= 0)
			//difficulty++;

		noise = 0.1f + Random.Range(-0.02f, 0.02f);

		int size = debugSize + (difficulty*Mathf.CeilToInt(((float)debugSize)/10f));
		currentSize = size;
		GenerateBiomes(size, size);

		// Then scatter disasters between the player and goal at random, with quantity and perhaps position depending on difficulty.
		// Need a math function to spawn disasters evenly (roughly) across the opposite axis as player/goal.

		// So I need to divide the world into thirds.
		int sectionSize = Mathf.FloorToInt (((float)size)/3f);

		// Let's talk sections.  They're organized like a snake (or a keypad with the middle line reversed).
		/*
		*	7 8 9
		*	6 5 4
		*	1 2 3
		*/
		// The goal is in the section 9 area while the first plant is in the section 1 area.

		int midDiff = (difficulty/2) + (difficulty%2);
		int[] disasterSections = new int[difficulty];
		for (int i = 0; i < midDiff; i++)
			disasterSections[i] = 5; // Stick half the disasters, rounding up, in the middle section.

		// Now I need to halve the remaining ones, and put each half in oppsite side regions.
		int sideDiff = difficulty-midDiff;

		int botDiff = sideDiff/2;
		int topDiff = botDiff + (sideDiff%2);

		for (int i = midDiff; i < midDiff+topDiff; i++)
			disasterSections[i] = Random.Range(6, 9);

		for (int i = midDiff+topDiff; i < difficulty; i++)
			disasterSections[i] = Random.Range(2, 5);

		// Now we have cases for each section.  I wish that I had smarter math for this, but I don't.
		// Or do I?

		// Making readable variables.
		int close = sectionSize;
		int mid = sectionSize*2;
		int far = sectionSize*3;
		close++;
		mid++;
		far++;
		Debug.Log(close + ", " + mid + ", " + far);
		for (int i = 0; i < disasterSections.Length ; i++) {
			switch (disasterSections[i]) {
				case 5:
					SpawnDisaster(Random.Range(close, mid), Random.Range(close, mid));
					break;
				case 2:
					SpawnDisaster(Random.Range(close, mid), Random.Range(0, close));
					break;
				case 3:
					SpawnDisaster(Random.Range(mid, far), Random.Range(0, close));
					break;
				case 4:
					SpawnDisaster(Random.Range(mid, far), Random.Range(close, mid));
					break;
				case 6:
					SpawnDisaster(Random.Range(0, close), Random.Range(close, mid));
					break;
				case 7:
					SpawnDisaster(Random.Range(0, close), Random.Range(mid, far));
					break;
				case 8:
					SpawnDisaster(Random.Range(close, mid), Random.Range(mid, far));
					break;

			}
		}

	}

	private void SpawnDisaster (int ecks, int why) {
		if (ecks < 0 || ecks >= currentSize || why < 0 || why >= currentSize)
			Debug.Log("Attempting to spawn disaster outside of map.");
		// This is where I'll put the actual call when the manager or whatever is able to execute it.  For now, this stays empty.
		manager.AddDisaster(ecks, why);

	}
}
