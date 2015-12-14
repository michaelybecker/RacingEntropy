using UnityEngine;
using System.Collections;

public static class TileHelper
{
	//public enum tile: int{DESERT,MARSH,FOREST,LAKE,MOUNTAIN,PLAIN,CRAGS,GOAL};
	//public enum element: int{EARTH,AIR,WATER,FIRE};
	public static int CombinationLookup(int terrain, int element)
	{
		switch(terrain)
		{
		case (int)TileType.tile.DESERT:
			switch(element)
			{
			case (int)TileType.element.WATER:
				return (int)TileType.tile.MARSH;
			case (int)TileType.element.EARTH:
				return (int)TileType.tile.CRAGS;	
			case (int)TileType.element.FIRE:
				return -1;//(int)TileType.tile.DESERT;
			case (int)TileType.element.AIR:
				return -1;//(int)TileType.tile.DESERT;
			}
			return -1;
		case (int)TileType.tile.MARSH:
			switch(element)
			{
			case (int)TileType.element.WATER:
				return (int)TileType.tile.LAKE;
			case (int)TileType.element.EARTH:
				return (int)TileType.tile.PLAIN;	
			case (int)TileType.element.FIRE:
				return (int)TileType.tile.FOREST;
			case (int)TileType.element.AIR:
				return -1;//(int)TileType.tile.DESERT;
			}
			return -1;
		case (int)TileType.tile.FOREST:
			switch(element)
			{
			case (int)TileType.element.WATER:
				return (int)TileType.tile.MARSH;
			case (int)TileType.element.EARTH:
				return (int)TileType.tile.CRAGS;	
			case (int)TileType.element.FIRE:
				return (int)TileType.tile.PLAIN;
			case (int)TileType.element.AIR:
				return (int)TileType.tile.PLAIN;
			}
			return -1;
		case (int)TileType.tile.LAKE:
			switch(element)
			{
			case (int)TileType.element.WATER:
				return -1;//(int)TileType.tile.MARSH;
			case (int)TileType.element.EARTH:
				return (int)TileType.tile.MARSH;	
			case (int)TileType.element.FIRE:
				return (int)TileType.tile.MARSH;
			case (int)TileType.element.AIR:
				return -1;//(int)TileType.tile.DESERT;
			}
			return -1;
		case (int)TileType.tile.MOUNTAIN:
			switch(element)
			{
			case (int)TileType.element.WATER:
				return (int)TileType.tile.CRAGS;
			case (int)TileType.element.EARTH:
				return -1;//(int)TileType.tile.CRAGS;	
			case (int)TileType.element.FIRE:
				return -1;//(int)TileType.tile.DESERT;
			case (int)TileType.element.AIR:
				return (int)TileType.tile.CRAGS;
			}
			return -1;
		case (int)TileType.tile.PLAIN:
			switch(element)
			{
			case (int)TileType.element.WATER:
				return (int)TileType.tile.FOREST;
			case (int)TileType.element.EARTH:
				return (int)TileType.tile.CRAGS;	
			case (int)TileType.element.FIRE:
				return (int)TileType.tile.DESERT;
			case (int)TileType.element.AIR:
				return -1;//(int)TileType.tile.DESERT;
			}
			return -1;
		case (int)TileType.tile.CRAGS:
			switch(element)
			{
			case (int)TileType.element.WATER:
				return (int)TileType.tile.PLAIN;
			case (int)TileType.element.EARTH:
				return (int)TileType.tile.MOUNTAIN;	
			case (int)TileType.element.FIRE:
				return (int)TileType.tile.DESERT;
			case (int)TileType.element.AIR:
				return (int)TileType.tile.DESERT;
			}
			return -1;
		case (int)TileType.tile.GOAL:
			return -1;
		default:
			return -1;
		}
	}
}
