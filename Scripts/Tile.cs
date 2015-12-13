using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
	//holders for what the gameObject is doing
	public Mesh mesh;
	public Material material;
	public Vector3 position;
	public int x;
	public int y;

	//Terrain types
	public int type;

	//Plant factor
	public bool plant;//plant that is growing
	public float growthFactor;
	public float plantGrowth;

	//Fire factor
	public bool fire;
	public float flammability;
	public int burnout; //turns until burnt out

	//Create a new Tile
	public Tile(int newType, Vector3 newPosition, int X, int Y)
	{
		//Set the tile parameters
		setTile (newType);
		position = newPosition;

		x = X;
		y = Y;
	}

	public void Change(int element)
	{
		setTile (TileHelper.CombinationLookup(type,element));
	}

	//Set the tile parameters
	private void setTile(int newType)
	{
		if(newType != -1)
		{
			type = newType;
			mesh = Resource.tileMesh[type];
			material = Resource.tileMaterial[type];
			switch(type)
			{
			case (int)TileType.tile.DESERT:
				growthFactor = 0.0f;
				flammability = 0.0f;
				break;
			case (int)TileType.tile.MARSH:
				growthFactor = 0.25f;
				flammability = 0.01f;
				break;
			case (int)TileType.tile.FOREST:
				growthFactor = 0.5f;
				flammability = 0.5f;
				break;
			case (int)TileType.tile.LAKE:
				growthFactor = 0.0f;
				flammability = 0.0f;
				break;
			case (int)TileType.tile.MOUNTAIN:
				growthFactor = 0.05f;
				flammability = 0.05f;
				break;
			case (int)TileType.tile.PLAIN:
				growthFactor = 1f;
				flammability = 1f;
				break;
			case (int)TileType.tile.CRAGS:
				growthFactor = 0.075f;
				flammability = 0.025f;
				break;
			case (int)TileType.tile.GOAL:
				growthFactor = 10;
				flammability = 0.0f;
				break;
			}
		}
		else
		{
			//Debug.Log("That has yet to be implemented");
		}
	}
}
