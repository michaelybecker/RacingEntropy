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
	public Dictionary<GameObject,GameObject> getFlair = new Dictionary<GameObject,GameObject> ();
	//Scale of the world corresponding to the X,Y coordinates
	public Vector3 worldScale;
	public Vector2 boardSize;
	//Plant manager
	public PlantManager plant;
	public List<Fire> fires = new List<Fire>();
	public List<Storm> storms = new List<Storm>();
	public List<Disaster> disasters = new List<Disaster>();

	//Hover functions
	private GameObject lastHover;

	public void Awake()
	{
		NewLevel (10);
		cascade = new CascadeManager(this);
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
		getFlair.Add (newTile,tileFlair);

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
		tileFromObject.Clear();
		objectFromTile.Clear ();
		getFlair.Clear ();
		lastHover = null;
		while (fires.Count > 0)
			fires [fires.Count - 1].Kill ();
		fires.Clear ();
		while (storms.Count > 0)
			storms [storms.Count - 1].Kill ();
		storms.Clear ();
		while (disasters.Count > 0)
			disasters [disasters.Count - 1].Kill ();
		disasters.Clear ();
		plant.plantTiles.Clear ();

		Global.win = false;
		Global.lose = false;
		Global.playingTheme = false;

		mapControl.BuildDifficulty ((difficulty-1)+(difficulty*Global.levelNumber));
	}

	//Create map using a multidimensional array of ints corresponding to the TileType.type ENUM
	public void CreateMap(int[,] map)
	{
		getTile = new Tile[map.GetLength(0),map.GetLength(1)];

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
				Global.numOfTiles++;
				Global.tileTypes[map[x,y]]++;
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

	public void Turn()
	{
		plant.Grow ();
		if(fires.Count > 0)
		{
			for(int i = 0; i < fires.Count; i++)
			{
				fires[i].Grow();
			}
		}
		if(storms.Count > 0)
		{
			for(int i = 0; i < storms.Count; i++)
			{
				storms[i].Turn();
			}
		}
		if(disasters.Count > 0)
		{
			for(int i = 0; i < disasters.Count; i++)
			{
				disasters[i].Turn();
			}
		}
		Global.turns++;
		Debug.Log ("Turns made " + Global.turns);
	}

	//Change the tile based on the gameObject
	public void ChangeType(GameObject tile, int element)
	{
		if(Resource.elementSound[element] != null)audioControl.Play (Resource.elementSound [element],0.5f);

		Tile changedTile = tileFromObject [tile];
		cascade.OnElement (changedTile,element);
		Change (tile, changedTile);
		Turn ();
	}

	public void Change(GameObject tile, Tile changedTile)
	{
		tile.GetComponent<MeshRenderer>().material = changedTile.material;
		Transform flair = getFlair [tile].transform;
		flair.GetComponent<MeshRenderer>().material = changedTile.material;
		flair.GetComponent<MeshFilter>().mesh = changedTile.mesh;		
		plant.KillPlant (changedTile);
	}

	public void OnHover(GameObject tile)
	{
		if (lastHover != null)
		{
			lastHover.GetComponent<MeshRenderer>().material = tileFromObject[lastHover].material;
			getFlair[lastHover].GetComponent<MeshRenderer> ().material = tileFromObject[lastHover].material;
		}
		tile.GetComponent<MeshRenderer> ().material.color += new Color(0.5f,0.5f,0.5f);
		getFlair[tile].GetComponent<MeshRenderer> ().material.color += new Color(0.5f,0.5f,0.5f);
		lastHover = tile;
	}

	//Add a plant to a certain tile
	public void AddPlant(int x, int y)
	{
		plant.AddPlant (x, y);
	}

	public void AddFire(int x, int y)
	{
		GameObject newFireObject = new GameObject ("Fire" + x + "," + y);
		Fire newFire = newFireObject.AddComponent<Fire>();
		newFire.manager = this;
		newFire.StartFire(getTile[x,y]);
		fires.Add (newFire);
	}

	public void AddStorm(int x, int y)
	{
		GameObject newStormObject = new GameObject ("Storm" + x + "," + y);
		Storm newStorm = newStormObject.AddComponent<Storm>();
		newStorm.manager = this;
		newStorm.StartStorm(getTile[x,y]);
		storms.Add (newStorm);
	}

	public void AddDisaster(int x, int y) 
	{
		GameObject newDisasterObject = new GameObject ("Disaster" + x + "," + y);
		Disaster newDisaster = newDisasterObject.AddComponent<Disaster>();
		newDisaster.manager = this;
		newDisaster.StartDisaster(getTile[x,y]);
		disasters.Add (newDisaster);
		//Remove existing flair on that tile
		getFlair [objectFromTile [getTile [x, y]]].transform.GetComponent<MeshFilter> ().mesh = null;
	}
}
