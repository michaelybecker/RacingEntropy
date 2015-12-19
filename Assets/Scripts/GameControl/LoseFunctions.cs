using UnityEngine;
using System.Collections;

public partial class WinManager : MonoBehaviour
{
	//Lose the flag challenge
	public bool FlagLose(int difficulty, int type, ref string printOut)
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
	public bool GrowPlantLose (int difficulty, int type, ref string printOut)
	{
		if (Global.plantTypes [type] < 1)
			return true;
		return false;
	}

	//Can't really lose this one...
	public bool NoLose(int difficulty, int type, ref string printOut){return false;}

	//If there are no more plants you lose
	public bool SurvivalLose(int difficulty, int type, ref string printOut)
	{
		return FlagLose (difficulty, type, ref printOut);
	}
}

