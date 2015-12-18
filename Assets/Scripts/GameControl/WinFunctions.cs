using UnityEngine;
using System.Collections;

public partial class WinManager: MonoBehaviour
{
	/**************************************
	 * Ideas for win conditions:
	 * Specific plant to flag,
	 * Pattern creation
	 * Kill all plants
	 * 0 of a specific tile on the map
	 * *************************************/
	//if a plant has reached the flay, you win!
	public bool FlagWin (int difficulty, ref string printOut)
	{
		int achieved = 0;
		foreach (Tile goal in goals) 
		{
			if(goal.plant != null) achieved++;
		}
		printOut = "Grow to all the flagposts!" + achieved + "/" + difficulty;
		if (achieved >= difficulty) 
		{
			goals.Clear();
			return true;
		}
		return false;
	}

	//If you have the required number of plants you win
	public bool PlainPlantWin (int difficulty, ref string printOut)
	{
		int check = 10 * difficulty;

		printOut = "Get " + Global.plantTypes [(int)TileType.tile.PLAIN] + "/" + check + " Plain plants";

		if (Global.plantTypes [(int)TileType.tile.PLAIN] >= check)
			return true;
		return false;
	}

	//If you have the required types of tiles
	public bool DesertTilesWin(int difficulty, ref string printOut)
	{
		int check = 10 * difficulty;

		printOut = "Have " + Global.tileTypes [(int)TileType.tile.DESERT] + "/" + check + " Desert tiles accross the map";

		if (Global.tileTypes [(int)TileType.tile.DESERT] >= check)
			return true;
		return false;
	}
}