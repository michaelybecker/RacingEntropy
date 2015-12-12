using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
	//Game map
	public Tile[,] getTile = new Tile[100,100];
	//Tile to game object
	public Dictionary<Tile,GameObject> getGameObject = new Dictionary<Tile,GameObject>();
	//Scale of the world corresponding to the X,Y coordinates
	public Vector3 worldScale;

	public void Start()
	{
		int[,] map = new int[,]{{0,1,2,3,4},{5,6,7,6,5},{6,5,4,3,2},{1,0,0,0,0},{0,0,0,0,0}};
		CreateMap (map);
	}

	//Adds a new tile
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

	//Place a new tile in the scene
	public void Place(Tile tile)
	{
		//Creates a new tile
		GameObject newTile = new GameObject ("");
		//adds it to the dictionary
		getGameObject.Add (tile, newTile);

		//Creates a collider for Jade to hit with a raycast (can't remember if a rigidbody is needed....
		BoxCollider collider = newTile.AddComponent<BoxCollider> ();
		collider.size = worldScale;
		//TODO get the size correct
		collider.center = new Vector3 (0, worldScale.y / 2, 0);

		//Creates the mesh holder and adds a mesh
		MeshFilter filter = newTile.AddComponent<MeshFilter> ();
		MeshRenderer renderer = newTile.AddComponent<MeshRenderer>();

		filter.mesh = tile.mesh;
		renderer.material = tile.material;
		//TODO add a texture and shader

		//position the gameobject
		newTile.transform.position = tile.position;
		newTile.transform.parent = transform;
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
	}

	//Change the tile object
	public void ChangeType(Tile tile, int newType)
	{

	}

	//Chang the tile based on the gameObject
	public void ChangeType(GameObject tile, int newType)
	{
		
	}
	
	//Add a plant to a certain tile
	public void AddPlant(int x, int y)
	{

	}
}

