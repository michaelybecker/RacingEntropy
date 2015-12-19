using UnityEngine;
using System.Collections;

public partial class WinManager: MonoBehaviour
{
	/**************************************
	 * Ideas for win conditions:
	 * Specific plant to flag,
	 * Stay alive for so long
	 * Kill all plants
	 * 0 of a specific tile on the map,
	 * Pattern creation
	 * *************************************/
	//if a plant has reached the flay, you win!
	public bool FlagWin (int difficulty, int type, ref string printOut)
	{
		int achieved = 0;
		foreach (Tile goal in goals) 
		{
			if(goal.plant != null) achieved++;
		}
		printOut = "Grow to all the flagposts!" + achieved + "/" + difficulty;
		if (achieved >= difficulty) 
		{
			Global.levelNumber++;
			goals.Clear();
			return true;
		}
		return false;
	}

	//If you have the required number of plants you win
	public bool GrowPlantWin (int difficulty, int type, ref string printOut)
	{
		int check = 10 * difficulty;

		printOut = "Get " + Global.plantTypes [type] + "/" + check + " " + TileType.tileString[type] + " plants";

		if (Global.plantTypes [type] >= check)
			return true;
		return false;
	}

	//If you have the required types of tiles
	public bool NumberOfTilesWin(int difficulty, int type, ref string printOut)
	{
		int check = 10 * difficulty;

		printOut = "Add " + Global.tileTypes [type] + "/" + check + " " + TileType.tileString[type] + " tiles accross the map";

		if (Global.tileTypes [type] >= check)
			return true;
		return false;
	}

	//If the timer has exceeded the needed amount of time
	public bool SurvivalWin(int difficulty, int type, ref string printOut)
	{
		int check = 10 * difficulty;
		float currentTime = Time.time - survivalTime;
		printOut = "Survive for " + check + "/" + currentTime.ToString ("F2") + "seconds";

		if (currentTime > check)
			return true;
		return false;
	}
}