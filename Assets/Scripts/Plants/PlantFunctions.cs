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

	public void flowerTemplate(Tile tile, int type)
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
		if(tileOptions.Count > 0)
		{
			if(Random.Range(0,4) == 0)AddPlant(tileOptions[Random.Range(0,tileOptions.Count)],type);
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
