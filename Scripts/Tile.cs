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

	//Create a new Tile
	public Tile(int newType, Vector3 newPosition)
	{
		//Set the tile parameters
		setTile (newType);
		position = newPosition;
	}

	//Set the tile parameters
	private void setTile(int newType)
	{
		type = newType;
		mesh = Resource.tileMesh[type];
		material = Resource.tileMaterial[type];
		switch(type)
		{
		case (int)TileType.tile.DESERT:
			growthFactor = 1;
			break;
		case (int)TileType.tile.MARSH:
			growthFactor = 1;
			break;
		case (int)TileType.tile.FOREST:
			growthFactor = 1;
			break;
		case (int)TileType.tile.LAKE:
			growthFactor = 1;
			break;
		case (int)TileType.tile.MOUNTAIN:
			growthFactor = 1;
			break;
		case (int)TileType.tile.PLAIN:
			growthFactor = 1;
			break;
		case (int)TileType.tile.CRAGS:
			growthFactor = 1;
			break;
		case (int)TileType.tile.GOAL:
			growthFactor = 1;
			break;
		}
	}
}
