using UnityEngine;
using System.Collections;

public class Plant
{
	public Tile tile;
	public TileType.growFunction function;
	public int type;
	
	public Plant(TileType.growFunction newFunction, Tile newTile, int newType)
	{
		function = newFunction;
		tile = newTile;
		type = newType;
	}
}

