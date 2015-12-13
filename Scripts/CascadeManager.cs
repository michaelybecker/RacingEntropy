using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TO DO: Implement Air.

public class CascadeManager {

	// So for each cascading element, I'm going to need a whole array of affected tiles because I don't want to hit the same tile twice.
	// So I'll need to check if the tile in question is already on the affected list.
	// If !Contains(currentTile) Add(currentTile)

	// Then at the end, apply the element to every affected tile.
	// Foreach (Tile current in xxxx)

	// So the only differece is the way that the cascade spreads, which is based on the element.

	public TileManager manager;

	private int width;
	private int height;

	private int[] elevations = new int[]{1,0,1,0,3,1,2,1};
	//private int[] telements = new int[]{3,2,2,2,0,1,0,1}; // For future air implementation.

	public CascadeManager (TileManager m) {
		manager = m;
	}

	public void OnElement (Tile currentTile, TileType.element currentElement) {

		// I can't guarentee that the map size will remain the same from call to call on this function.
		width = manager.getTile.GetLength(0);
		height = manager.getTile.GetLength(1);
		
		// So, first things first, I need to know the coordinate of the current tile.  And that's not currently stored.
		// I'd rather that each tile store this on creation, but this will work for now.
		int coordX = Mathf.RoundToInt (currentTile.position.x / manager.worldScale.x);
		int coordY = Mathf.RoundToInt (currentTile.position.y / manager.worldScale.y);

		// So now I need a bunch of cases for each element, and then I need a loop function for each of them
		List<Tile> affectedTiles = new List<Tile>();
		Queue<Tile> toDo = new Queue<Tile>();

		affectedTiles.Add(currentTile);
		toDo.Enqueue(currentTile);
		// Now we can assume in each iteration that we've already added the tile on toDo.
		// (reasonable, since it'll have been done by the prior iteration.)

		// loops until toDo is done.  This isn't dangerous at all...
		while (toDo.Count > 0) {
			switch (currentElement) {
				case TileType.element.WATER:
					OnWater(coordX, coordY, toDo.Dequeue(), affectedTiles, toDo);
					break;
				case TileType.element.FIRE:
					OnFire(coordX, coordY, toDo.Dequeue(), affectedTiles, toDo);
					break;
				case TileType.element.EARTH:
					OnEarth(coordX, coordY, toDo.Dequeue(), affectedTiles, toDo);
					break;
				case TileType.element.AIR:
					OnAir(coordX, coordY, toDo.Dequeue(), affectedTiles, toDo);
					break;
			}
		}

		// Then apply the element effect to every tile in affectedTiles.  I don't currently know what to call for that.
		foreach (Tile t in affectedTiles) {
			t.Change((int)currentElement);
		}
	}

	private void OnWater (int coordX, int coordY, Tile currentTile, List<Tile> affectedTiles, Queue<Tile> toDo) {
		// Four checks, comparing elevation of each adjacent tile with elevation of this tile.
		// Can short-circut if this tile is already elevation 1 or less.
		if (elevations[currentTile.type] == 0)
			return;

		Tile currentCheck;

		if (coordY != 0) {
			currentCheck = manager.getTile[coordX, coordY-1];
			if (elevations[currentCheck.type] < elevations[currentTile.type]) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}

		if (coordX != 0) {
			currentCheck = manager.getTile[coordX-1, coordY];
			if (elevations[currentCheck.type] < elevations[currentTile.type]) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}

		if (coordX != width-1) { 
			currentCheck = manager.getTile[coordX+1, coordY];
			if (elevations[currentCheck.type] < elevations[currentTile.type]) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}

		if (coordY != height-1) { 
			currentCheck = manager.getTile[coordX, coordY+1];
			if (elevations[currentCheck.type] < elevations[currentTile.type]) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}
	}

	private void OnFire (int coordX, int coordY, Tile currentTile, List<Tile> affectedTiles, Queue<Tile> toDo) {
		// Four checks to see if the adjacent tiles are flammable.

		Tile currentCheck;

		if (coordY != 0) {
			currentCheck = manager.getTile[coordX, coordY-1];
			if (!affectedTiles.Contains(currentCheck) && (currentCheck.type == 5 || currentCheck.type == 2)) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}

		if (coordX != 0) {
			currentCheck = manager.getTile[coordX-1, coordY];
			if (!affectedTiles.Contains(currentCheck) && (currentCheck.type == 5 || currentCheck.type == 2)) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}

		if (coordX != width-1) { 
			currentCheck = manager.getTile[coordX+1, coordY];
			if (!affectedTiles.Contains(currentCheck) && (currentCheck.type == 5 || currentCheck.type == 2)) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}

		if (coordY != height-1) { 
			currentCheck = manager.getTile[coordX, coordY+1];
			if (!affectedTiles.Contains(currentCheck) && (currentCheck.type == 5 || currentCheck.type == 2)) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}
	}

	private void OnEarth (int coordX, int coordY, Tile currentTile, List<Tile> affectedTiles, Queue<Tile> toDo) {
		// Make sure we're a crag or mountain
		if (currentTile.type != 6 && currentTile.type != 4)
			return;

		// Four checks
		Tile currentCheck;

		if (coordY != 0) {
			currentCheck = manager.getTile[coordX, coordY-1];
			if (elevations[currentCheck.type] < elevations[currentTile.type]) {
				affectedTiles.Add(currentCheck);
			}
		}

		if (coordX != 0) {
			currentCheck = manager.getTile[coordX-1, coordY];
			if (elevations[currentCheck.type] < elevations[currentTile.type]) {
				affectedTiles.Add(currentCheck);
			}
		}

		if (coordX != width-1) { 
			currentCheck = manager.getTile[coordX+1, coordY];
			if (elevations[currentCheck.type] < elevations[currentTile.type]) {
				affectedTiles.Add(currentCheck);
			}
		}

		if (coordY != height-1) { 
			currentCheck = manager.getTile[coordX, coordY+1];
			if (elevations[currentCheck.type] < elevations[currentTile.type]) {
				affectedTiles.Add(currentCheck);
			}
		}

	}

	private void OnAir (int coordX, int coordY, Tile currentTile, List<Tile> affectedTiles, Queue<Tile> toDo) {
		// Four checks

		// Currently air doesn't do anything anyway, so... take a moment.
	}
}
