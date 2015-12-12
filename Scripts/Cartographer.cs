using UnityEngine;
using System.Collections;

// TO DO: Get this onto my laptop and/or Github.
// TO DO: Add starting location/goal tiles (do we need different types?  How are we handling plants on a tile?  Definitely need a goal tile)
// TO DO: Actual Tile code calls.

// This class stores level data and includes level generation algorithms.
// Goal = 8.

public class Cartographer : MonoBehaviour {

	private int[,] mapData;
	private GameObject[,] mapObjects; // Might want to store Tile components here instead, but I'm doing GameObjects for initial testing.

	public float spacing;

	public bool debugBuild; // For testing absent supporting scripts.
	public int debugSize;
	public GameObject debugFab;

	public void Awake () {
		if (debugBuild)
			GenerateRandom (debugSize, debugSize);
	}

	// Basic functionality: generate a completely random level of X width and Y height.
	public void GenerateRandom (int width, int height) {

		mapData = new int[width, height];
		mapObjects = new GameObject[width, height];

		for (int why = 0; why < height; why++) {
			for (int ecks = 0; ecks < width; ecks++) {
				mapData[ecks, why] = Random.Range (0, 8);
				GameObject temp;

				// The following is purely for visualizing in absence of supporting scripts and assets.
				if (debugBuild) {
					temp = (GameObject) GameObject.Instantiate (debugFab, new Vector3(ecks*spacing, 0, why*spacing), Quaternion.identity);
					float brightness = mapData[ecks, why]/7f;
					temp.renderer.material.color = new Color (brightness, brightness, brightness);
				} else {
					// This is more how we'd actually be doing it.
					temp = new GameObject();
					temp.transform.position = new Vector3(ecks*spacing, 0, why*spacing);
				}

				// Insert calls to the tile scripting here for tile initialization.

				temp.name = ""+mapData[ecks, why];
				mapObjects[ecks, why] = temp;
			}
		}

		// Designate start location and goal.
		// What's a reasonable percentage of the overall size?
		int placementRange = debugSize/5;
		int minPlace = 1;
		if (placementRange <= 1)
			minPlace = 0;

		int startX = Random.Range(minPlace, placementRange);
		int startY = Random.Range(minPlace, placementRange);

		mapObjects[startX, startY].renderer.material.color = Color.green;

		int endX = Random.Range(width-placementRange, width-minPlace);
		int endY = Random.Range(height-placementRange, height-minPlace);

		mapData[endX, endY] =  8;
		mapObjects[endX, endY].renderer.material.color = Color.yellow;

		// TileManager.CreateMap(mapData);
	}

	// Second layer of functionality: generate different clumps of terrain based on moisture and elevation maps.
	public void GenerateBiomes (int width, int height) {
		// TO BE CONTINUED
		
		// The double perlin?  Probably.  At least double.  Maybe for each element?  I need combinations, so math time.
		
	}
}