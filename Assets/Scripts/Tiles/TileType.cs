using UnityEngine;
using System.Collections;

public static class TileType
{
	//Enumerators for tile and element types
	public enum tile: int{DESERT,MARSH,FOREST,LAKE,MOUNTAIN,PLAIN,CRAGS,GOAL};
	public static int[] elevations = new int[]{1,0,1,-1,3,1,2,1};

	//What each of the tiles is called... probably a better way of doing this
	public static string[] tileString = new string[]{
		"Desert","Marsh","Forest","Lake","Mountain","Plain","Crags",
	};
	//Elements used to terraform
	public enum element: int{EARTH,AIR,WATER,FIRE};
	//Current disasters,
	//TODO move disaster functions to delegates... probably...
	public enum disaster: int{STORM,VOLCANO,FLOOD,EARTHQUAKE};

	//Delegate template for calculating points at any given tile
	public delegate void growFunction (Tile tile);
	public delegate void spreadFunction (Tile tile);
}