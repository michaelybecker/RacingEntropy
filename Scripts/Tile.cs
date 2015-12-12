using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile
{
	public Mesh mesh;
	public float growthFactor;
	public int type;
	public Vector3 position;
	
	public Tile(int newType, Vector3 newPosition)
	{
		//Set the tile parameters
		setTile (newType);
		position = newPosition;
	}

	private void setTile(int newType)
	{
		type = newType;
		switch(type)
		{
		case (int)TileType.tile.DESERT:
			growthFactor = 1;
			mesh = TileType.tileMesh[(int)TileType.tile.DESERT];
			break;
		case (int)TileType.tile.MARSH:
			growthFactor = 1;
			mesh = TileType.tileMesh[(int)TileType.tile.MARSH];
			break;
		case (int)TileType.tile.FOREST:
			growthFactor = 1;
			mesh = TileType.tileMesh[(int)TileType.tile.FOREST];
			break;
		case (int)TileType.tile.LAKE:
			growthFactor = 1;
			mesh = TileType.tileMesh[(int)TileType.tile.LAKE];
			break;
		case (int)TileType.tile.MOUNTAIN:
			growthFactor = 1;
			mesh = TileType.tileMesh[(int)TileType.tile.MOUNTAIN];
			break;
		case (int)TileType.tile.PLAIN:
			growthFactor = 1;
			mesh = TileType.tileMesh[(int)TileType.tile.PLAIN];
			break;
		case (int)TileType.tile.CRAGS:
			growthFactor = 1;
			mesh = TileType.tileMesh[(int)TileType.tile.CRAGS];
			break;
		case (int)TileType.tile.GOAL:
			growthFactor = 1;
			mesh = TileType.tileMesh[(int)TileType.tile.GOAL];
			break;
		}
	}
}
