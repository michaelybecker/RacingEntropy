using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
	//holders for what the gameObject is doing
	public Mesh mesh;
	public Material material;
	public Vector3 position;

	//Variables for growth and stuff
	public int type;
	public float growthFactor;
	public bool plant;

	//Create a new Tile
	public Tile(int newType, Vector3 newPosition)
	{
		//Set the tile parameters
		setTile (newType);
		position = newPosition;
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
				growthFactor = 0;
				break;
			case (int)TileType.tile.MARSH:
				growthFactor = 0.75f;
				break;
			case (int)TileType.tile.FOREST:
				growthFactor = 1;
				break;
			case (int)TileType.tile.LAKE:
				growthFactor = 0.5f;
				break;
			case (int)TileType.tile.MOUNTAIN:
				growthFactor = 0.2f;
				break;
			case (int)TileType.tile.PLAIN:
				growthFactor = 0.75f;
				break;
			case (int)TileType.tile.CRAGS:
				growthFactor = 0.2f;
				break;
			case (int)TileType.tile.GOAL:
				growthFactor = 10;
				break;
			}
		}
		else
		{
			Debug.Log("That has yet to be implemented");
		}
	}
}
