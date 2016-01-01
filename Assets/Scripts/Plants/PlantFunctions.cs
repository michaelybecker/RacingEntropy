using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class PlantManager : MonoBehaviour
{
	public intVector2[] dir4 = new  intVector2[]{
		new intVector2(0,1),
		new intVector2(1,0),
		new intVector2(0,-1),
		new intVector2(-1,0),
	};

	//Template used for growing flowers
	//Finds the best tile option out of the options in the 4 cardinal directions
	public void flowerTemplate(Tile tile, int type)
	{
		//Check if the plant is grown, if so spread it
		if(tile.plant.grown)
		{
			List<Tile> tileOptions = new List<Tile> ();
			
			for(int i = 0; i < dir4.Length; i++)
			{
				int newX = tile.x+dir4[i].x;
				int newY = tile.y+dir4[i].y;
				if(inRange(newX,newY))
				{
					if(manager.getTile[newX,newY].type == type && manager.getTile[newX,newY].plant == null)
					{
						tileOptions.Add(manager.getTile[newX,newY]);
					}
					if(manager.getTile[newX,newY].type == (int)TileType.tile.GOAL)
					{
						//Goal tile trumps all, cleat all the other options and just grow there
						tileOptions.Clear();
						AddPlant(manager.getTile[newX,newY],type);
						break;
					}
				}
			}
			//Add a plant at that point
			if(tileOptions.Count > 0)
			{
				AddPlant(tileOptions[Random.Range(0,tileOptions.Count)],type);
			}
		}
		//if it isn't then grow it.
		else
		{
			//set the growth factor to a little bit higher
			tile.plant.growthValue += tile.growthFactor;
			//if it gets over 10 it's a fully grown plant
			if(tile.plant.growthValue > 1) tile.plant.grown = true;

			//Do some graphic fun to make it look pretty and seem like it's growing by scaling the size of the plant
			float s = tile.plant.growthValue/1;
			if(s > 1) s = 1;
			else if(s <= 0) s = 0.1f;
			tile.plant.instance.transform.localScale = new Vector3(s,s,s);
		}
	}

	//"Desert","Marsh","Forest","Lake","Mountain","Plain","Crags",
	public void DesertFlower(Tile tile)
	{
		flowerTemplate (tile, (int)TileType.tile.DESERT);
	}
	public void MarshFlower(Tile tile)
	{
		flowerTemplate (tile, (int)TileType.tile.MARSH);
	}
	public void ForestFlower(Tile tile)
	{
		flowerTemplate (tile, (int)TileType.tile.FOREST);
	}
	public void LakeFlower(Tile tile)
	{
		flowerTemplate (tile, (int)TileType.tile.LAKE);
	}
	public void MountainFlower(Tile tile)
	{
		flowerTemplate (tile, (int)TileType.tile.MOUNTAIN);
	}
	public void PlainFlower(Tile tile)
	{
		flowerTemplate (tile, (int)TileType.tile.PLAIN);
	}
	public void CragFlower(Tile tile)
	{
		flowerTemplate (tile, (int)TileType.tile.CRAGS);
	}
}
