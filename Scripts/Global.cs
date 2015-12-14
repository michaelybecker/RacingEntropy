using UnityEngine;
using System.Collections;

public static class Global
{
	//Game running mode
	public static bool win = false;
	public static bool lose = false;
	public static bool pause = false;
	public static bool playingTheme = true;

	//Current level number
	public static int levelNumber = 0;
	public static int turns = 0;
	public static int score = 0;

	//Number of each type of tile
	public static int numOfTiles;
	public static int numOfPlants;
	public static int[] tileTypes = new int[10];
}