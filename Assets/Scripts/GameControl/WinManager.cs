using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class WinManager: MonoBehaviour
{
	public TileManager manager;
	public List<WinCondition> conditions;
	//The win condition and the difficulty of the win condition
	public Dictionary<WinCondition,int> currentConditions;

	//Delegates for setting up, winning and losing the game
	public delegate void setupFunction(int difficulty);
	public delegate bool winFunction(int difficulty, ref string printOut);
	public delegate bool loseFunction(int difficulty, ref string printOut);

	public void Awake()
	{
		//Needs to be in a function as you cant call non-static variables
		//from outside of a function, which is lame...
		conditions = new List<WinCondition> (){
			new WinCondition(FlagSetup,FlagWin,FlagLose),//Original game mode
			new WinCondition(PlainPlantSetup,PlainPlantWin,PlainPlantLose), //grow a certain amount of plants
			new WinCondition(DesertTilesSetup,DesertTilesWin,DesertTilesLose), //Have a certain number of tile types
		};
	}

	public void NewWinConditions(int quantity,int difficulty)
	{
		currentConditions = new Dictionary<WinCondition, int> ();
		//if the game wants to add more win conditions than there are win abilites, set a cap and increase the difficulty insread
		if(quantity > conditions.Count) 
		{
			difficulty += (quantity - conditions.Count);
			quantity = conditions.Count;
		}
		for(int i = 0; i < quantity; i++)
		{
			//Select a new random win condition
			WinCondition newCondition = conditions[Random.Range(0,conditions.Count)];
			//If it already exists in the dictionary, select a new one
			if(!currentConditions.ContainsKey(newCondition))
				currentConditions.Add(newCondition,difficulty);
			else i--;
		}
		GameSetup ();
	}

	public void GameSetup()
	{
		//Set the game states
		Global.win = false;
		Global.lose = false;
		Global.playingTheme = false;

		//reset the score
		Global.turns = 0;
		Global.tileTypes = new int[10];
		Global.plantTypes = new int[10];

		
		List<WinCondition> conditionList = new List<WinCondition>(currentConditions.Keys);
		for(int i = 0; i < conditionList.Count; i++)
		{
			//Setup the game according to the rules
			conditionList[i].setup(currentConditions[conditionList[i]]);
		}
	}

	public void Turn()
	{
		HasLost ();
		HasWon ();
	}

	public void HasWon()
	{
		bool fullWin = true;
		bool[] win = new bool[currentConditions.Keys.Count];
		List<WinCondition> conditionList = new List<WinCondition>(currentConditions.Keys);
		//Check all the win conditions
		for(int i = 0; i < conditionList.Count; i++)
		{
			WinCondition condition = conditionList[i];
			if(condition.win(currentConditions[condition],ref condition.description)) 
				win[i] = true;
		}
		//if all the win conditions are true, you have won
		for(int i = 0; i < win.Length; i++)
		{
			if(!win[i]) fullWin = false;
		}
		//Set the global win to true
		Global.win = fullWin;
		if (fullWin)
			Global.levelNumber++;
	}

	public void HasLost()
	{
		List<WinCondition> conditionList = new List<WinCondition>(currentConditions.Keys);
		//Check all the win conditions
		for(int i = 0; i < conditionList.Count; i++)
		{
			WinCondition condition = conditionList[i];
			if(condition.lose(currentConditions[condition],ref condition.description)) 
				Global.lose = true;
		}
	}

	private bool inRange(int newX, int newY)
	{
		if (newX >= 0 && newX < manager.getTile.GetLength (0) 
		    && newY >= 0 && newY < manager.getTile.GetLength (1))
			return true;
		else
			return false;
	}
}

public class WinCondition
{
	public string description;
	public WinManager.setupFunction setup;
	public WinManager.winFunction win;
	public WinManager.loseFunction lose;

	public WinCondition(WinManager.setupFunction setupFunction,WinManager.winFunction winFunction,WinManager.loseFunction loseFunction)
	{
		setup = setupFunction;
		win = winFunction;
		lose = loseFunction;
	}
}