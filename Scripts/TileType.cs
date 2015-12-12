using UnityEngine;
using System.Collections;

public static class TileType
{
	public enum tile: int{DESERT,MARSH,FOREST,LAKE,MOUNTAIN,PLAIN,CRAGS,GOAL};
	public enum element: int{EARTH,AIR,WIND,FIRE};

	public static Mesh[] tileMesh = new Mesh[]{
		(Mesh)Resources.Load("Tiles/Desert"),
		(Mesh)Resources.Load("Tiles/Marsh"),
		(Mesh)Resources.Load("Tiles/Forest"),
		(Mesh)Resources.Load("Tiles/Lake"),
		(Mesh)Resources.Load("Tiles/Mountain"),
		(Mesh)Resources.Load("Tiles/Plain"),
		(Mesh)Resources.Load("Tiles/Crags"),
		(Mesh)Resources.Load("Tiles/Goal"),
	};
}