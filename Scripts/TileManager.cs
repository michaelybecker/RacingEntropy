using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class intVector2
{
	public int x;
	public int y;

	public intVector2(int newX, int newY)
	{
		x = newX;
		y = newY;
	}
}

public class TileManager : MonoBehaviour
{
	//Game map
	public Tile[,] getTile = new Tile[100,100];
	//Tile to game object
	public Dictionary<GameObject,Tile> tileFromObject = new Dictionary<GameObject,Tile>();
	public Dictionary<intVector2,GameObject> objectFromCoordinate = new Dictionary<intVector2,GameObject>();
	//Scale of the world corresponding to the X,Y coordinates
	public Vector3 worldScale;
	public Vector2 boardSize;
	//Plant manager
	public PlantManager plant;

	//Adds a new tile
	public void Add(int tileType, int X, int Y)
	{
		//scale to world coordinates
		Vector3 newLocation = new Vector3(worldScale.x*X,worldScale.y,worldScale.z*Y);
		Tile newTile = new Tile (tileType,newLocation);

		//add to array
		getTile [X, Y] = newTile;

		//create the gameobject
		Place (newTile,X,Y);
	}

	//Place a new tile in the scene
	public void Place(Tile tile, int x, int y)
	{
		//Creates a new tile
		//A purely cosmetic object to be placed above the base tile
		GameObject newTile = new GameObject ("Tile");
		GameObject tileFlair = new GameObject ("Flair");
		//adds it to the dictionary
		tileFromObject.Add (newTile, tile);
		objectFromCoordinate.Add (new intVector2 (x,y), newTile);

		//Creates a collider for Jade to hit with a raycast (can't remember if a rigidbody is needed....
		BoxCollider collider = newTile.AddComponent<BoxCollider> ();
		collider.size = worldScale;
		//TODO get the size correct
		collider.center = new Vector3 (0, worldScale.y / 2, 0);

		//Creates the mesh holder and adds a mesh
		MeshFilter filter = newTile.AddComponent<MeshFilter> ();
		MeshRenderer renderer = newTile.AddComponent<MeshRenderer>();
		MeshFilter flairFilter = tileFlair.AddComponent<MeshFilter> ();
		MeshRenderer flairRenderer = tileFlair.AddComponent<MeshRenderer>();

		filter.mesh = Resource.baseMesh;
		flairFilter.mesh = tile.mesh;

		renderer.material = tile.material;
		flairRenderer.material = tile.material;
		//TODO add a texture and shader

		//position the gameobject
		newTile.transform.position = tile.position;
		tileFlair.transform.position = new Vector3 (tile.position.x, tile.position.y + 0.5f, tile.position.z);
		newTile.transform.parent = transform;
		tileFlair.transform.parent = newTile.transform;
	}

	//Create map using a multidimensional array of ints corresponding to the TileType.type ENUM
	public void CreateMap(int[,] map)
	{
		for(int x = 0; x < map.GetLength(0); x++)
		{
			for(int y = 0; y < map.GetLength(1); y++)
			{
				Add(map[x,y],x,y);
			}
		}
		boardSize = new Vector2 (map.GetLength(0)*worldScale.x,map.GetLength(1)*worldScale.z);
	}

	//Change the tile object
	public void ChangeType(int x, int y, int element)
	{
		GameObject tile = objectFromCoordinate[new intVector2(x,y)];
		Tile changedTile = tileFromObject [tile];
		changedTile.Change (element);
		tile.GetComponent<MeshRenderer>().material = changedTile.material;
		foreach(Transform child in tile.transform)
		{
			child.GetComponent<MeshRenderer>().material = changedTile.material;
			child.GetComponent<MeshFilter>().mesh = changedTile.mesh;
		}
		plant.Update ();
	}

	//Chang the tile based on the gameObject
	public void ChangeType(GameObject tile, int element)
	{
		Tile changedTile = tileFromObject [tile];
		changedTile.Change (element);
		tile.GetComponent<MeshRenderer>().material = changedTile.material;
		foreach(Transform child in tile.transform)
		{
			child.GetComponent<MeshRenderer>().material = changedTile.material;
			child.GetComponent<MeshFilter>().mesh = changedTile.mesh;
		}
		plant.Update ();
	}
	
	//Add a plant to a certain tile
	public void AddPlant(int x, int y)
	{
		Debug.Log ("adding plant at" + x + " " + y);
		plant.AddPlant (x, y);
	}
}

