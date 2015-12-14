using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TO DO: Implement disaster cascades.

public class CascadeManager {

	// So for each cascading element, I'm going to need a whole array of affected tiles because I don't want to hit the same tile twice.
	// So I'll need to check if the tile in question is already on the affected list.
	// If !Contains(currentTile) Add(currentTile)

	// Then at the end, apply the element to every affected tile.
	// Foreach (Tile current in xxxx)

	// So the only differece is the way that the cascade spreads, which is based on the element.

	public TileManager manager;

	public int width;
	public int height;

	private int[] elevations = new int[]{1,0,1,-1,3,1,2,1};
	//private int[] telements = new int[]{3,2,2,2,0,1,0,1};

	private List<Tile> affectedTiles;
	private Queue<Tile> toDo;

	public CascadeManager (TileManager m) {
		manager = m;
	}

	public void UpdateSize () {
		width = manager.getTile.GetLength(0);
		height = manager.getTile.GetLength(1);
	}

	public void OnElement (Tile currentTile, int currentElement) {

		// I can't guarentee that the map size will remain the same from call to call on this function.
		UpdateSize();
		
		// So, first things first, I need to know the coordinate of the current tile.  And that's not currently stored.
		// I'd rather that each tile store this on creation, but this will work for now.

		// So now I need a bunch of cases for each element, and then I need a loop function for each of them
		affectedTiles = new List<Tile>();
		toDo = new Queue<Tile>();

		// Don't want to hit ourselves with air.
		affectedTiles.Add(currentTile);
		toDo.Enqueue(currentTile);
		// Now we can assume in each iteration that we've already added the tile on toDo.
		// (reasonable, since it'll have been done by the prior iteration.)

		// loops until toDo is done.  This isn't dangerous at all...
		while (toDo.Count > 0) 
		{
			switch (currentElement) 
			{
				case (int)TileType.element.WATER:
					OnWater(toDo.Dequeue());
					break;
				case (int)TileType.element.FIRE:
					if(currentTile.flammability > 0)manager.AddFire(currentTile.x,currentTile.y);
					OnFire(toDo.Dequeue());
					break;
				case (int)TileType.element.EARTH:
					OnEarth(toDo.Dequeue());
					break;
				case (int)TileType.element.AIR:
					OnAir(toDo.Dequeue());
					break;
			}
		}

		// Then apply the element effect to every tile in affectedTiles.  I don't currently know what to call for that.
		foreach (Tile t in affectedTiles) {
			t.Change(currentElement);
			manager.Change(manager.objectFromTile[t],t);
		}
	}

	private void OnWater (Tile currentTile) {
		// Four checks, comparing elevation of each adjacent tile with elevation of this tile.
		// Can short-circut if this tile is already elevation 1 or less.
		if (elevations[(int)currentTile.type] == 0)
			return;

		int coordX = currentTile.x;
		int coordY = currentTile.y;

		Tile currentCheck;

		if (coordY != 0) {
			currentCheck = manager.getTile[coordX, coordY-1];
			if (!affectedTiles.Contains(currentCheck) && (elevations[currentCheck.type] < elevations[currentTile.type])) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}

		if (coordX != 0) {
			currentCheck = manager.getTile[coordX-1, coordY];
			if (!affectedTiles.Contains(currentCheck) && (elevations[currentCheck.type] < elevations[currentTile.type])) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}

		if (coordX != width-1) { 
			currentCheck = manager.getTile[coordX+1, coordY];
			if (!affectedTiles.Contains(currentCheck) && (elevations[currentCheck.type] < elevations[currentTile.type])) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}

		if (coordY != height-1) { 
			currentCheck = manager.getTile[coordX, coordY+1];
			if (!affectedTiles.Contains(currentCheck) && (elevations[currentCheck.type] < elevations[currentTile.type])) {
				affectedTiles.Add(currentCheck);
				toDo.Enqueue(currentCheck);
			}
		}
	}

	private void OnFire (Tile currentTile) {
		// Four checks to see if the selected tile is flammable.
		if (currentTile.type != (int)TileType.tile.FOREST && currentTile.type != (int)TileType.tile.PLAIN)
			return;
		
		
		int coordX = currentTile.x;
		int coordY = currentTile.y;
		
		// Four checks
		Tile currentCheck = manager.getTile[coordX,coordY];
		manager.AddFire(currentCheck.x,currentCheck.y);

		/*int coordX = currentTile.x;
		int coordY = currentTile.y;
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
		}*/
	}

	private void OnEarth (Tile currentTile) {
		// Make sure we're a crag or mountain
		if (currentTile.type != 6 && currentTile.type != 4)
			return;

		int coordX = currentTile.x;
		int coordY = currentTile.y;

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

	private void OnAir (Tile currentTile) {
		int coordX = currentTile.x;
		int coordY = currentTile.y;

		Tile currentCheck = manager.getTile[coordX,coordY];
		//manager.AddStorm(currentCheck.x,currentCheck.y);

		
		// Currently air doesn't do anything anyway, so... take a moment.
	}

	// Make a storm on a random adjacent tile (inclusive of this tile).
	public void OnThunder (Tile currentTile) {
		UpdateSize();
		int coordX = currentTile.x;
		int coordY = currentTile.y;

		List<Tile> candidates = new List<Tile>();
		candidates.Add(currentTile);

		// Four checks
		Tile currentCheck;

		if (coordY != 0) {
			currentCheck = manager.getTile[coordX, coordY-1];
			candidates.Add(currentCheck);
		}

		if (coordX != 0) {
			currentCheck = manager.getTile[coordX-1, coordY];
			candidates.Add(currentCheck);
		}

		if (coordX != width-1) { 
			currentCheck = manager.getTile[coordX+1, coordY];
			candidates.Add(currentCheck);
		}

		if (coordY != height-1) { 
			currentCheck = manager.getTile[coordX, coordY+1];
			candidates.Add(currentCheck);
		}

		currentCheck = candidates[Random.Range(0, candidates.Count)];
		//manager.AddStorm(currentCheck.x,currentCheck.y);

	}

	// Spread fire like a volcano.
	public void OnEruption (Tile currentTile) {
		UpdateSize();
		int coordX = currentTile.x;
		int coordY = currentTile.y;

		// Also light current tile on fire if possible.
		if (currentTile.type == (int)TileType.tile.FOREST || currentTile.type == (int)TileType.tile.PLAIN) {
			manager.AddFire(currentTile.x,currentTile.y);
		}


		// Four checks
		Tile currentCheck;

		if (coordY != 0) {
			currentCheck = manager.getTile[coordX, coordY-1];
			if (currentCheck.type == (int)TileType.tile.FOREST || currentCheck.type == (int)TileType.tile.PLAIN) {
				manager.AddFire(currentCheck.x,currentCheck.y);
			}
		}

		if (coordX != 0) {
			currentCheck = manager.getTile[coordX-1, coordY];
			if (currentCheck.type == (int)TileType.tile.FOREST || currentCheck.type == (int)TileType.tile.PLAIN) {
				manager.AddFire(currentCheck.x,currentCheck.y);
			}		}

		if (coordX != width-1) { 
			currentCheck = manager.getTile[coordX+1, coordY];
			if (currentCheck.type == (int)TileType.tile.FOREST || currentCheck.type == (int)TileType.tile.PLAIN) {
				manager.AddFire(currentCheck.x,currentCheck.y);
			}
		}

		if (coordY != height-1) { 
			currentCheck = manager.getTile[coordX, coordY+1];
			if (currentCheck.type == (int)TileType.tile.FOREST || currentCheck.type == (int)TileType.tile.PLAIN) {
				manager.AddFire(currentCheck.x,currentCheck.y);
			}
		}

		// Now also throw fire a distance.

		int reach = 3;

		// So, select values that are within reach but not off the board.

		int minX = currentTile.x - reach;
		while (minX < 0)
			minX++;

		int maxX = currentTile.x + reach;
		while (maxX >= width)
			maxX--;

		int epiX = Random.Range(minX, maxX+1);

		int minY = currentTile.y - reach;
		while (minY < 0)
			minY++;

		int maxY = currentTile.y + reach;
		while (maxY >= height)
			maxY--;

		int epiY = Random.Range(minY, maxY+1);

		// Then we can do a regular four-check to hit the surrounding tiles with earth.  Actually, we're just hitting that tile with earth...
		OnElement(manager.getTile[epiX, epiY], (int)TileType.element.FIRE);
	}

	// This puppy should cascade, complete with toDo, until it finds an appropriate tile to spread water to.
	// And by "appripriate" I mean the closest non-crag and non-mountain tile.
	public void OnFlood (Tile currentTile) {
		UpdateSize();

		// If this tile is a lake, then we'll end up affecting this tile instead of searching for a tile to flood.
		Tile t = currentTile;

		if (currentTile.type == (int)TileType.tile.LAKE) {
			int bestValue = 99; // Just a value higher than any elevation.
			// Do all the usual prep for a long search.
			width = manager.getTile.GetLength(0);
			height = manager.getTile.GetLength(1);

			toDo = new Queue<Tile>();
			List<Tile> candidates = new List<Tile>();
			affectedTiles = new List<Tile>(); // We're not using this for actual effects, but we need to track what's been searched.

			// Check for flooding candidates until we've looked at every tile adjacent to this body of water.
			toDo.Enqueue(currentTile);
			while (toDo.Count > 0) {
				bestValue = FloodSearch(toDo.Dequeue(), candidates, bestValue);
			}

			// Exclude all but the lowest-elevation candidates
			List<Tile> secondCut = new List<Tile>();
			foreach (Tile currentCandidate in candidates) {
				if (elevations[(int)currentCandidate.type] == bestValue)
					secondCut.Add(currentCandidate);
			}

			// Pick a random candidate to apply water.
			if (secondCut.Count > 0) {
				t = secondCut[Random.Range(0, secondCut.Count)];
			}
		}

		// Apply water to the best candidate
		t.Change((int)TileType.element.WATER);
		manager.Change(manager.objectFromTile[t],t);

	}

	// Called by OnFlood.
	private int FloodSearch (Tile currentTile, List<Tile> candidates, int bestValue) {

		// Here we need to check all adjacent tiles and, if height >= 0, add them to the candidates list.
		// Else, add them to toDo if they're not already included.

		int coordX = currentTile.x;
		int coordY = currentTile.y;

		// Four checks
		Tile currentCheck;

		if (coordY != 0) {
			currentCheck = manager.getTile[coordX, coordY-1];
			if (!affectedTiles.Contains(currentCheck)) {
				affectedTiles.Add(currentCheck);
				if (currentCheck.type == (int)TileType.tile.LAKE) {
					toDo.Enqueue(currentCheck);
				} else {
					if (elevations[currentCheck.type] <= bestValue) {
						bestValue = elevations[currentCheck.type];
						candidates.Add(currentCheck);
					}
				}
			}
		}

		if (coordX != 0) {
			currentCheck = manager.getTile[coordX-1, coordY];
			if (!affectedTiles.Contains(currentCheck)) {
				affectedTiles.Add(currentCheck);
				if (currentCheck.type == (int)TileType.tile.LAKE) {
					toDo.Enqueue(currentCheck);
				} else {
					if (elevations[currentCheck.type] <= bestValue) {
						bestValue = elevations[currentCheck.type];
						candidates.Add(currentCheck);
					}
				}
			}		
		}

		if (coordX != width-1) { 
			currentCheck = manager.getTile[coordX+1, coordY];
			if (!affectedTiles.Contains(currentCheck)) {
				affectedTiles.Add(currentCheck);
				if (currentCheck.type == (int)TileType.tile.LAKE) {
					toDo.Enqueue(currentCheck);
				} else {
					if (elevations[currentCheck.type] <= bestValue) {
						bestValue = elevations[currentCheck.type];
						candidates.Add(currentCheck);
					}
				}
			}
		}

		if (coordY != height-1) { 
			currentCheck = manager.getTile[coordX, coordY+1];
			if (!affectedTiles.Contains(currentCheck)) {
				affectedTiles.Add(currentCheck);
				if (currentCheck.type == (int)TileType.tile.LAKE) {
					toDo.Enqueue(currentCheck);
				} else {
					if (elevations[currentCheck.type] <= bestValue) {
						bestValue = elevations[currentCheck.type];
						candidates.Add(currentCheck);
					}
				}
			}
		}

		return bestValue;

	}

	// I don't know what this does.  Turn stuff into crags, mostly.  Like a reverse flood.
	// So yeah, we need non-element effects then.
	// I feel that an earthquake should feel more random, but be powerful when it strikes.
	// Randomly pick a tile within X spaces, then hit all tiles around the epicenter with earth.
	public void OnQuake (Tile currentTile) {
		UpdateSize();

		int reach = 5;

		// So, select values that are within reach but not off the board.

		int minX = currentTile.x - reach;
		while (minX < 0)
			minX++;

		int maxX = currentTile.x + reach;
		while (maxX >= width)
			maxX--;

		int epiX = Random.Range(minX, maxX+1);

		int minY = currentTile.y - reach;
		while (minY < 0)
			minY++;

		int maxY = currentTile.y + reach;
		while (maxY >= height)
			maxY--;

		int epiY = Random.Range(minY, maxY+1);

		// Then we can do a regular four-check to hit the surrounding tiles with earth.  Actually, we're just hitting that tile with earth...
		OnElement(manager.getTile[epiX, epiY], (int)TileType.element.EARTH);

	}

}
