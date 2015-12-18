using UnityEngine;
using System.Collections;

public partial class WinManager: MonoBehaviour
{
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

		if (Global.plantTypes [(int)TileType.tile.PLAIN] > check)
			return true;
		return false;
	}
}