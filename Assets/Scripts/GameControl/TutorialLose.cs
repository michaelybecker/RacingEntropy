using UnityEngine;
using System.Collections;

public partial class WinManager : MonoBehaviour
{
	//If there are no more plants to grow... you lose
	public bool TutorialEightLose (int difficulty, int type, ref string printOut)
	{
		type = (int)TileType.tile.PLAIN;
		if (Global.plantTypes [type] < 1)
			return true;
		return false;
	}

	//If there are no more plants to grow... you lose
	public bool TutorialNineLose (int difficulty, int type, ref string printOut)
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
}

