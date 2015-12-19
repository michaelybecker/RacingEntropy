using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Storm : MonoBehaviour 
{
	public TileManager manager;
	public Tile currentTile;
	public Tile previousTile;

	//Creates the object
	public void StartStorm(Tile newTile)
	{
		currentTile = newTile;
		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
		
		filter.mesh = Resource.stormMesh;
		renderer.material = Resource.stormMaterial;

		transform.position = new Vector3 (
				manager.objectFromTile[currentTile].transform.position.x,
				manager.objectFromTile[currentTile].transform.position.y+manager.worldScale.y, 
				manager.objectFromTile[currentTile].transform.position.z);

		previousTile = currentTile;
	}

	//Action on every turn
	public void Turn () 
	{
		if (Random.Range(0, 10) == 0)
		{
			Kill();
			return;
		}
		manager.cascade.UpdateSize();
		// Check the four adjacent tiles to make sure that they exist.

		// Disqualify mountains and crags
		// Also disqualify if last tile occupied

		int coordX = currentTile.x;
		int coordY = currentTile.y;

		List<Tile> candidates = new List<Tile>();

		// Four checks to see if it walled in
		Tile currentCheck;

		if (coordY != 0) {
			currentCheck = manager.getTile[coordX, coordY-1];
			if (currentCheck.type != (int)TileType.tile.MOUNTAIN && (previousTile == null || currentCheck != previousTile))
				candidates.Add(currentCheck);
		}

		if (coordX != 0) {
			currentCheck = manager.getTile[coordX-1, coordY];
			if (currentCheck.type != (int)TileType.tile.MOUNTAIN && (previousTile == null || currentCheck != previousTile))
				candidates.Add(currentCheck);
		}

		if (coordX != manager.cascade.width-1) { 
			currentCheck = manager.getTile[coordX+1, coordY];
			if (currentCheck.type != (int)TileType.tile.MOUNTAIN && (previousTile == null || currentCheck != previousTile))
				candidates.Add(currentCheck);
		}

		if (coordY != manager.cascade.height-1) { 
			currentCheck = manager.getTile[coordX, coordY+1];
			if (currentCheck.type != (int)TileType.tile.MOUNTAIN && (previousTile == null || currentCheck != previousTile))
				candidates.Add(currentCheck);
		}

		previousTile = currentTile;
		if (candidates.Count == 0) 
		{
			Kill();
			return;
		}

		currentTile = candidates[Random.Range(0, candidates.Count)];

		// Apply air effects here.
		currentTile.Change((int)TileType.element.AIR);
		manager.Change(manager.objectFromTile[currentTile],currentTile);

		transform.position = new Vector3 (
			manager.objectFromTile[currentTile].transform.position.x,
			manager.objectFromTile[currentTile].transform.position.y+manager.worldScale.y, 
			manager.objectFromTile[currentTile].transform.position.z);
	}

	//Destroy itself and remove all traces of it's existence
	public void Kill()
	{
		manager.storms.Remove (this);
		DestroyImmediate (gameObject);
	}
}
