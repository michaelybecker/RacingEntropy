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
	public Tile[,] getTile;

	//Camera control
	public CameraControl cameraControl;

	//Cascade manager
	public CascadeManager cascade;
	public Cartographer mapControl;
	public AudioManager audioControl;

	//Tile to game object
	public Dictionary<GameObject,Tile> tileFromObject = new Dictionary<GameObject,Tile>();
	public Dictionary<Tile,GameObject> objectFromTile = new Dictionary<Tile,GameObject>();
	//Scale of the world corresponding to the X,Y coordinates
	public Vector3 worldScale;
	public Vector2 boardSize;
	//Plant manager
	public PlantManager plant;
	public List<FireManager> fires = new List<FireManager>();

	public void Awake()
	{
		cascade = new CascadeManager(this);
		NewLevel (1);
	}

	//Adds a new tile
	public void Add(int tileType, int X, int Y)
	{
		//scale to world coordinates
		Vector3 newLocation = new Vector3(worldScale.x*X,worldScale.y,worldScale.z*Y);
		Tile newTile = new Tile (tileType,newLocation,X,Y);

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
		GameObject newTile = new GameObject ("Tile" + x + "," + y);
		GameObject tileFlair = new GameObject ("Flair");
		//adds it to the dictionary
		tileFromObject.Add (newTile, tile);
		objectFromTile.Add (tile, newTile);

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

	public void NewLevel(int difficulty)
	{
		Global.win = false;
		Global.lose = false;
		Global.playingTheme = false;
		mapControl.BuildDifficulty (difficulty);
	}

	//Create map using a multidimensional array of ints corresponding to the TileType.type ENUM
	public void CreateMap(int[,] map)
	{
		getTile = new Tile[map.GetLength(0),map.GetLength(1)];
		tileFromObject.Clear();
		objectFromTile.Clear ();
		fires.Clear ();
		for ( int i=transform.childCount-1; i>=0; --i )
		{
			var child = transform.GetChild(i).gameObject;
			Destroy( child );
		}

		for(int x = 0; x < map.GetLength(0); x++)
		{
			for(int y = 0; y < map.GetLength(1); y++)
			{
				Add(map[x,y],x,y);
			}
		}
		boardSize = new Vector2 (map.GetLength(0)*worldScale.x,map.GetLength(1)*worldScale.z);
		cameraControl.Init();
	}

	//Change the tile object
	public void ChangeType(int x, int y, int element)
	{
		//get the game object
		GameObject tile = objectFromTile[getTile[x,y]];
		ChangeType (tile, element);
	}

	//Change the tile based on the gameObject
	public void ChangeType(GameObject tile, int element)
	{
		if(Resource.elementSound[element] != null)audioControl.Play (Resource.elementSound [element]);

		Tile changedTile = tileFromObject [tile];
		cascade.OnElement (changedTile,element);
		Change (tile, changedTile);
		plant.Grow ();
		if(fires.Count > 0)
		{
			for(int i = 0; i < fires.Count; i++)
			{
				fires[i].Grow();
				fires[i].KillFire(changedTile);
			}
		}
		//changedTile.Change (element);
	}

	public void Change(GameObject tile, Tile changedTile)
	{
		tile.GetComponent<MeshRenderer>().material = changedTile.material;
		Transform flair = objectFromTile [changedTile].transform.Find ("Flair");
		flair.GetComponent<MeshRenderer>().material = changedTile.material;
		flair.GetComponent<MeshFilter>().mesh = changedTile.mesh;

		plant.KillPlant (changedTile);
	}

	//Add a plant to a certain tile
	public void AddPlant(int x, int y)
	{
		plant.AddPlant (x, y);
	}

	public void AddFire(int x, int y)
	{
		GameObject newFireObject = new GameObject ("Fire" + x + "," + y);
		FireManager newFire = newFireObject.AddComponent<FireManager>();
		newFire.manager = this;
		newFire.AddFire (x,y);
		fires.Add (newFire);
	}
}

