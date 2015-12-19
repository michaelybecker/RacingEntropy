using UnityEngine;
using System.Collections;

public static class Global
{
	//Stuff for the tutorial
	//Unlocks
	public static bool[] elementUnlock = new bool[]
	{//int{EARTH,AIR,WATER,FIRE};
		true,true,true,true
	};
	public static int tutorialProgress = 0;

	//Game running mode
	public static bool win = false;
	public static bool lose = false;
	public static bool pause = false;
	public static bool playingTheme = true;
	public static float gameSpeed = 0.5f;

	//Current level number
	public static int levelNumber = 0;
	public static int turns = 0;
	public static int score = 0;

	//Number of each type of tile
	public static int numOfTiles;
	public static int[] plantTypes = new int[10];
	public static int[] tileTypes = new int[10];

	//Center of the map
	public static Vector3 center;
}