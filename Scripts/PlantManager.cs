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
				Tile tempTile = 
					manager.tileFromObject [manager.tileFromCoordinate [new intVector2 (plant.Key.x+dir.x,plant.Key.y+dir.y)]].plant;
				if(tempTile.plant = false)
				{
					if(tempTile.growthFactor > 0.5f)
					{
						AddPlant(
					}
				}
			}
		}
	}

	public void AddPlant(int x, int y)
	{
		manager.tileFromObject [manager.tileFromCoordinate [new intVector2 (x, y)]].plant = true;
		//TODO add plant mesh thingy
	}
}

