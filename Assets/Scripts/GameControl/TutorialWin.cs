using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO place annoying pop ups to help direct the player as to what the fuck is going on...
public partial class WinManager : MonoBehaviour
{
	//With only water available add 10 water tiles
	public bool TutorialOneWin(int difficulty, int type, ref string printOut)
	{
		int check = 10;
		type = (int)TileType.tile.LAKE;
		
		printOut = "Add " + Global.tileTypes [type] + "/" + check + " " + TileType.tileString[type] + " tiles accross the map";
		
		if (Global.tileTypes [type] >= check)
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			return true;
		}
		return false;
	}
	//With only earth available add 10 mountain tiles
	public bool TutorialTwoWin(int difficulty, int type, ref string printOut)
	{
		int check = 10;
		type = (int)TileType.tile.MOUNTAIN;
		
		printOut = "Add " + Global.tileTypes [type] + "/" + check + " " + TileType.tileString[type] + " tiles accross the map";
		
		if (Global.tileTypes [type] >= check)
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			return true;
		}
		return false;
	}
	//With only fire available get to 50% desert
	public bool TutorialThreeWin(int difficulty, int type, ref string printOut)
	{
		int check = (int)(manager.getTile.Length/2);
		type = (int)TileType.tile.DESERT;
		
		printOut = "Add " + Global.tileTypes [type] + "/" + check + " " + TileType.tileString[type] + " tiles accross the map";
		
		if (Global.tileTypes [type] >= check)
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			return true;
		}
		return false;
	}
	//With only wind available add 10 marsh or desert
	public bool TutorialFourWin(int difficulty, int type, ref string printOut)
	{
		int check = 10;
		int typeA = (int)TileType.tile.MARSH;
		int typeB = (int)TileType.tile.DESERT;
		
		printOut = "Add " + Global.tileTypes [typeA] + "/" + check + " " + TileType.tileString[typeA] + " tiles accross the map \r\n" +
			"Add " + Global.tileTypes [typeB] + "/" + check + " " + TileType.tileString[typeB] + " tiles accross the map";
		
		if (Global.tileTypes [typeA] >= check && Global.tileTypes [typeB] >= check)
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			return true;
		}
		return false;
	}
	//With only wind available add 10 marsh or desert
	public bool TutorialFiveWin(int difficulty, int type, ref string printOut)
	{
		int check = 8;
		int typeA = (int)TileType.tile.FOREST;
		int typeB = (int)TileType.tile.PLAIN;
		int typeC = (int)TileType.tile.CRAGS;
		
		printOut = "Add " + Global.tileTypes [typeA] + "/" + check + " " + TileType.tileString[typeA] + " tiles accross the map \r\n" +
			"Add " + Global.tileTypes [typeB] + "/" + check + " " + TileType.tileString[typeB] + " tiles accross the map \r\n" +
				"Add " + Global.tileTypes [typeC] + "/" + check + " " + TileType.tileString[typeC] + " tiles accross the map";
		
		if (Global.tileTypes [typeA] >= check && Global.tileTypes [typeB] >= check && Global.tileTypes [typeC] >= check)
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			return true;
		}
		return false;
	}
	//With only wind available add 10 marsh or desert
	public bool TutorialSixWin(int difficulty, int type, ref string printOut)
	{
		int check = 8;
		int typeA = (int)TileType.tile.MARSH;
		int typeB = (int)TileType.tile.CRAGS;
		int typeC = (int)TileType.tile.MOUNTAIN;
		
		printOut = "Add " + Global.tileTypes [typeA] + "/" + check + " " + TileType.tileString[typeA] + " tiles accross the map \r\n" +
			"Add " + Global.tileTypes [typeB] + "/" + check + " " + TileType.tileString[typeB] + " tiles accross the map \r\n" +
				"Add " + Global.tileTypes [typeC] + "/" + check + " " + TileType.tileString[typeC] + " tiles accross the map";
		
		if (Global.tileTypes [typeA] >= check && Global.tileTypes [typeB] >= check && Global.tileTypes [typeC] >= check)
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			return true;
		}
		return false;
	}
	//With only wind available add 10 marsh or desert
	public bool TutorialSevenWin(int difficulty, int type, ref string printOut)
	{
		int check = 8;
		int typeA = (int)TileType.tile.DESERT;
		int typeB = (int)TileType.tile.FOREST;
		int typeC = (int)TileType.tile.PLAIN;
		
		printOut = "Add " + Global.tileTypes [typeA] + "/" + check + " " + TileType.tileString[typeA] + " tiles accross the map \r\n" +
			"Add " + Global.tileTypes [typeB] + "/" + check + " " + TileType.tileString[typeB] + " tiles accross the map \r\n" +
				"Add " + Global.tileTypes [typeC] + "/" + check + " " + TileType.tileString[typeC] + " tiles accross the map";
		
		if (Global.tileTypes [typeA] >= check && Global.tileTypes [typeB] >= check && Global.tileTypes [typeC] >= check)
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			return true;
		}
		return false;
	}
	
	//If you have the required number of plants you win
	public bool TutorialEightWin (int difficulty, int type, ref string printOut)
	{
		int check = 10;
		type = (int)TileType.tile.PLAIN;
		
		printOut = "Get " + Global.plantTypes [type] + "/" + check + " " + TileType.tileString[type] + " plants";
		
		if (Global.plantTypes [type] >= check)
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			return true;
		}
		return false;
	}

	public bool TutorialNineWin (int difficulty, int type, ref string printOut)
	{
		int achieved = 0;
		foreach (Tile goal in goals) 
		{
			if(goal.plant != null) achieved++;
		}
		printOut = "Grow to all the flagposts!" + achieved + "/" + difficulty;
		if (achieved >= difficulty) 
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			goals.Clear();
			return true;
		}
		return false;
	}
	//With only water available add 10 water tiles
	public bool TutorialTenWin(int difficulty, int type, ref string printOut)
	{
		int check = 30;
		type = (int)TileType.tile.LAKE;
		
		printOut = "Add " + Global.tileTypes [type] + "/" + check + " " + TileType.tileString[type] + " tiles accross the map";
		
		if (Global.tileTypes [type] >= check)
		{
			Global.tutorialProgress++;
			Global.levelNumber--;
			Global.elementUnlock = new bool[]{true,true,true,true};
			Global.gameSpeed = 0.5f;
			return true;
		}
		return false;
	}
}

