using UnityEngine;
using System.Collections;

// TO DO: Get this onto my laptop and/or Github.
// TO DO: Add starting location/goal tiles (do we need different types?  How are we handling plants on a tile?  Definitely need a goal tile)
// TO DO: Actual Tile code calls.

// This class stores level data and includes level generation algorithms.
// Goal = 8.

public class Cartographer : MonoBehaviour {

	private int[,] mapData;

	public float spacing;

	public bool debugBuild; // For testing absent supporting scripts.
	public int debugSize;

	public TileManager manager;

	public void Awake () {
		if (debugBuild)
			GenerateRandom (debugSize, debugSize);
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

		//int startX = Random.Range(minPlace, placementRange);
		//int startY = Random.Range(minPlace, placementRange);

		//mapObjects[startX, startY].renderer.material.color = Color.green; // I don't have plant control yet.

		int endX = Random.Range(width-placementRange, width-minPlace);
		int endY = Random.Range(height-placementRange, height-minPlace);

		mapData[endX, endY] =  7;

		manager.CreateMap(mapData);
	}

	// Second layer of functionality: generate different clumps of terrain based on moisture and elevation maps.
	public void GenerateBiomes (int width, int height) {
		// TO BE CONTINUED
		
		// The double perlin?  Probably.  At least double.  Maybe for each element?  I need combinations, so math time.
		
	}
}