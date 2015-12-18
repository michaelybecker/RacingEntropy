using UnityEngine;
using System.Collections;

public partial class WinManager : MonoBehaviour
{
	//Lose the flag challenge
	public bool FlagLose(int difficulty, ref string printOut)
	{
		int totalPlants = 0;
		for(int i = 0; i < Global.plantTypes.Length; i++)totalPlants += Global.plantTypes[i];
		if (totalPlants <= 0)
		{
			goals.Clear();
			return true;
		}
		return false;
	}

	//If there are no more plants to grow... you lose
	public bool PlainPlantLose (int difficulty, ref string printOut)
	{
		if (Global.plantTypes [(int)TileType.tile.PLAIN] < 1)
			return true;
		return false;
	}
}

