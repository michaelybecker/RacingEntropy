using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
	public Tile[,] getTile;
	public Dictionary<Tile,GameObject> getGameObject = new Dictionary<Tile,GameObject>();
	public Vector3 worldScale;

	public void Add(int tileType, int X, int Y)
	{
		//scale to world coordinates
		Vector3 newLocation = new Vector3(worldScale.x*X,worldScale.y,worldScale.z*Y);
		Tile newTile = new Tile (tileType,newLocation);

		//add to array
		getTile [X, Y] = newTile;

		//create the gameobject
		Place (newTile);
	}

	public void Place(Tile tile)
	{
		//Creates a new tile
		GameObject newTile = new GameObject ("");
		//adds it to the dictionary
		getGameObject.Add (tile, newTile);

		//Creates a collider for Jade to hit with a raycast (can't remember if a rigidbody is needed....
		BoxCollider collider = newTile.AddComponent<BoxCollider> ();
		//Rigidbody body = newTile.AddComponent<Rigidbody> ();
		//Creates the mesh holder and adds a mesh
		MeshFilter filter = newTile.AddComponent<MeshFilter> ();
		MeshRenderer renderer = newTile.AddComponent<MeshRenderer>();

		filter.mesh = tile.mesh;
		//TODO add a texture and shader

		//position the gameobject
		newTile.transform.position = tile.position;
		newTile.transform.parent = transform;
	}
}

