using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class PlantManager : MonoBehaviour
{
	//Pointer to main manager class
	public TileManager manager;

	//List of functions depending on the type of plant
	public List<TileType.growFunction> functions = new List<TileType.growFunction>(); 

	//The plants
	public List<Plant> plantTiles = new List<Plant>();
	
	public void Awake()
	{
		//DESERT,MARSH,FOREST,LAKE,MOUNTAIN,PLAIN,CRAGS
		functions = new List<TileType.growFunction>()
		{
			DesertFlower,MarshFlower,ForestFlower,LakeFlower,MountainFlower,PlainFlower,CragFlower,
		};
	}

	public void AddPlant(int x, int y, int type)
	{
		AddPlant (manager.getTile [x, y], type);
	}

	public void AddPlant(Tile newTile, int type)
	{
		if(type != -1 && newTile.plant == null)
		{
			GameObject tile = manager.objectFromTile [newTile];
			GameObject newPlant = new GameObject ("Plant");
			//if(plantObject.ContainsKey(newTile)) KillPlant(newTile);
			//plantObject.Add(newTile,newPlant);
			newPlant.transform.Rotate (new Vector3 (0,135,0));

			Plant plantObj = new Plant (functions [type],newPlant , newTile, type);
			plantTiles.Add (plantObj);

			newTile.plant = plantObj;

			//TODO add plant mesh thingy
			MeshFilter filter = newPlant.AddComponent<MeshFilter> ();
			MeshRenderer renderer = newPlant.AddComponent<MeshRenderer>();
			
			filter.mesh = Resource.plantMesh;
			renderer.material = Resource.plantMaterial;
			
			newPlant.transform.parent = tile.transform;
			newPlant.transform.position = new Vector3 (tile.transform.position.x, tile.transform.position.y+manager.worldScale.y, tile.transform.position.z);

			Global.plantTypes[type]++;
		}
	}
	
	public void Grow()
	{
		for(int i = 0; i < plantTiles.Count; i++)
		{
			Tile newTile = plantTiles[i].tile;
			plantTiles[i].function(newTile);
		}
	}

	public void Clear()
	{
		for(int i = 0; i < plantTiles.Count; i++) 
		{
			Global.plantTypes[plantTiles[i].type]--;
			plantTiles[i].tile.plant = null;
			plantTiles[i].Kill();
			plantTiles.Remove (plantTiles[i]);
		}
	}
	
	public void KillPlant(Tile plantTile)
	{
		if(plantTiles.Contains(plantTile.plant))
		{
			Global.plantTypes[plantTile.plant.type]--;
			plantTile.plant.Kill();
			plantTiles.Remove (plantTile.plant);
			plantTile.plant = null;
		}
	}

	private bool inRange(int newX, int newY)
	{
		if (newX >= 0 && newX < manager.getTile.GetLength (0) 
			&& newY >= 0 && newY < manager.getTile.GetLength (1))
			return true;
		else
			return false;
	}
	
	private void SetFlair(GameObject flair, Mesh mesh, Material material)
	{
		//Set the flair material and mesh
		flair.GetComponent<MeshRenderer>().material = material;
		flair.GetComponent<MeshFilter>().mesh = mesh;	
	}
}