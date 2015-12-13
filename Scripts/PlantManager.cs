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

	public void Update()
	{
		foreach(KeyValuePair<intVector2,Tile> plant in getPlantTile)
		{
			foreach(intVector2 dir in directions)
			{
				if(plant.Key.x+dir.x > 0 && plant.Key.x+dir.x < manager.getTile.GetLength(0) &&
				   plant.Key.y+dir.y > 0 && plant.Key.y+dir.y < manager.getTile.GetLength(1))
				{
					Tile tempTile = manager.tileFromObject [manager.objectFromCoordinate [new intVector2 (plant.Key.x+dir.x,plant.Key.y+dir.y)]];
					if(tempTile.plant == false)
					{
						if(tempTile.growthFactor > 0.5f)
						{
							AddPlant(plant.Key.x+dir.x,plant.Key.y+dir.y);
						}
					}
				}
			}
		}
	}

	public void AddPlant(int x, int y)
	{
		Tile newTile = manager.tileFromObject [manager.objectFromCoordinate [new intVector2 (x, y)]];
		newTile.plant = true;
		getPlantTile.Add (new intVector2(x,y),newTile);
		//TODO add plant mesh thingy
		GameObject tile = manager.objectFromCoordinate [new intVector2 (x, y)];
		tile.GetComponent<MeshRenderer>().material = Resource.plantMaterial;
		foreach(Transform child in tile.transform)
		{
			child.GetComponent<MeshRenderer>().material = Resource.plantMaterial;
		}
	}
}

