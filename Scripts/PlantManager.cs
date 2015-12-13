using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlantManager : MonoBehaviour
{
	//Data holder for map data
	public TileManager manager;
	//list of tiles with plants
	public Dictionary<intVector2,Tile> getPlantTile = new Dictionary<intVector2,Tile>();

	private intVector2[] directions = new intVector2[]{
		new intVector2(1,0),
		new intVector2(0,1),
		new intVector2(-1,0),
		new intVector2(0,-1)
	};

	public void Grow()
	{
		List<intVector2> plantLocation = new List<intVector2> (getPlantTile.Keys);
		intVector2 idealSpace = null;
		float mostFertile = 0;

		foreach(intVector2 plant in plantLocation)
		{
			foreach(intVector2 dir in directions)
			{
				if(plant.x+dir.x >= 0 && plant.x+dir.x < manager.getTile.GetLength(0) &&
				   plant.y+dir.y >= 0 && plant.y+dir.y < manager.getTile.GetLength(1))
				{
					Tile tempTile = manager.getTile[plant.x+dir.x,plant.y+dir.y];
					if(tempTile.plant == false)
					{
						if(tempTile.growthFactor > mostFertile)
						{
							idealSpace = new intVector2(plant.x+dir.x,plant.y+dir.y);
							mostFertile = tempTile.growthFactor;
						}
					}
				}
			}
		}
		if(idealSpace != null) 
		{
			Tile growTile = manager.getTile[idealSpace.x,idealSpace.y];
			growTile.growthFactor += mostFertile;
			if(growTile.growthFactor > 5)
			{
				AddPlant(idealSpace);
				growTile.growthFactor = 0;
			}
		}
	}

	public void KillPlant(Tile tile)
	{
		tile.plant = false;
		Transform plant = manager.objectFromTile [tile].transform.Find ("Plant");
		if(plant != null)
		{
			Destroy(plant.gameObject);
		}
	}

	public void KillPlant(int x, int y)
	{
		manager.getTile [x, y].plant = false;
		Transform plant = manager.objectFromTile [manager.getTile [x, y]].transform.Find ("Plant");
		if(plant != null)
		{
			Destroy(plant.gameObject);
		}
	}

	public void AddPlant(intVector2 pos)
	{
		AddPlant (pos.x, pos.y);
	}

	public void AddPlant(int x, int y)
	{
		GameObject tile = manager.objectFromTile [manager.getTile[x,y]];
		Tile newTile = manager.tileFromObject [tile];
		GameObject newPlant = new GameObject ("Plant");
		newTile.plant = true;
		getPlantTile.Add (new intVector2(x,y),newTile);
		//TODO add plant mesh thingy
		MeshFilter filter = newPlant.AddComponent<MeshFilter> ();
		MeshRenderer renderer = newPlant.AddComponent<MeshRenderer>();

		filter.mesh = Resource.plantMesh;
		renderer.material = Resource.plantMaterial;

		newPlant.transform.parent = tile.transform;
		newPlant.transform.position = new Vector3 (tile.transform.position.x, tile.transform.position.y+manager.worldScale.y, tile.transform.position.z);
	}
}

