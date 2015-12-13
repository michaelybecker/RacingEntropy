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
		foreach(intVector2 plant in plantLocation)
		{
			foreach(intVector2 dir in directions)
			{
				if(plant.x+dir.x >= 0 && plant.x+dir.x < manager.getTile.GetLength(0) &&
				   plant.y+dir.y >= 0 && plant.y+dir.y < manager.getTile.GetLength(1))
				{
					Tile tempTile = manager.tileFromObject[manager.objectFromTile[manager.getTile[plant.x+dir.x,plant.y+dir.y]]];
					if(tempTile.plant == false)
					{
						if(tempTile.growthFactor > 0.5f)
						{
							AddPlant(plant.x+dir.x,plant.y+dir.y);
						}
					}
				}
			}
		}
	}

	public void AddPlant(int x, int y)
	{
		GameObject tile = manager.objectFromTile [manager.getTile[x,y]];
		Tile newTile = manager.tileFromObject [tile];
		newTile.plant = true;
		getPlantTile.Add (new intVector2(x,y),newTile);
		//TODO add plant mesh thingy
		tile.GetComponent<MeshRenderer>().material = Resource.plantMaterial;
		foreach(Transform child in tile.transform)
		{
			child.GetComponent<MeshRenderer>().material = Resource.plantMaterial;
		}
	}
}

