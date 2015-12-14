using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TO DO: Need to kill fires and plants on the tile.  Don't currently know the commands for that, TileManager should probably have that option.

public class Storm : MonoBehaviour {

	public TileManager manager;
	public Tile currentTile;
	public Tile previousTile;

	public void Turn () {
		manager.cascade.UpdateSize();
		// Check the four adjacent tiles to make sure that they exist.

		// Disqualify mountains and crags
		// Also disqualify if last tile occupied

		int coordX = currentTile.x;
		int coordY = currentTile.y;

		List<Tile> candidates = new List<Tile>();

		// Four checks
		Tile currentCheck;

		if (coordY != 0) {
			currentCheck = manager.getTile[coordX, coordY-1];
			if (previousTile == null || currentCheck != previousTile)
				candidates.Add(currentCheck);
		}

		if (coordX != 0) {
			currentCheck = manager.getTile[coordX-1, coordY];
			if (previousTile == null || currentCheck != previousTile)
				candidates.Add(currentCheck);
		}

		if (coordX != manager.cascade.width-1) { 
			currentCheck = manager.getTile[coordX+1, coordY];
			if (previousTile == null || currentCheck != previousTile)
				candidates.Add(currentCheck);
		}

		if (coordY != manager.cascade.height-1) { 
			currentCheck = manager.getTile[coordX, coordY+1];
			if (previousTile == null || currentCheck != previousTile)
				candidates.Add(currentCheck);
		}

		previousTile = currentTile;
		currentTile = candidates[Random.Range(0, candidates.Count)];

		// Apply air effects here.
		currentTile.Change((int)TileType.element.AIR);
		manager.Change(manager.objectFromTile[currentTile],currentTile);

	}

}
