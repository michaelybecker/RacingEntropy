using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class WinManager: MonoBehaviour
{
	//Tile manager to control game board
	public TileManager manager;

	//Holders for different win conditions
	public List<WinCondition> conditions;
	public List<WinCondition> tutorial;

	//The win condition and the difficulty of the win condition
	public List<WinCondition> currentConditions = new List<WinCondition>();

	//Delegates for setting up, winning and losing the game
	public delegate void setupFunction(int difficulty, int type);
	public delegate bool winFunction(int difficulty, int type, ref string printOut);
	public delegate bool loseFunction(int difficulty, int type, ref string printOut);

	public void Awake()
	{
		//Needs to be in a function as you cant call non-static variables
		//from outside of a function, which is lame...
		conditions = new List<WinCondition> ()
		{
			new WinCondition(SurvivalSetup,SurvivalWin,SurvivalLose, true), //survive for x amount of time
			new WinCondition(FlagSetup,FlagWin,FlagLose, true),//Original game mode
			new WinCondition(GrowPlantSetup,GrowPlantWin,GrowPlantLose, false), //grow a certain amount of plants
			new WinCondition(NumberOfTilesSetup,NumberOfTilesWin,NoLose, false), //Have a certain number of tile types
		};

		//Tutorial levels, can be added to ad-infinitum
		tutorial = new List<WinCondition> ()
		{
			new WinCondition(TutorialOneSetup,TutorialOneWin,NoLose,true),//Level one introduce water
			new WinCondition(TutorialTwoSetup,TutorialTwoWin,NoLose,true),//Level two introduce earth
			new WinCondition(TutorialThreeSetup,TutorialThreeWin,NoLose,true),//Level three introduce fire
			new WinCondition(TutorialFourSetup,TutorialFourWin,NoLose,true),//Level four introduce air, though it's totally fucking useless...
			new WinCondition(TutorialFiveSixSevenSetup,TutorialFiveWin,NoLose,true),//Level five putting it all together
			new WinCondition(TutorialFiveSixSevenSetup,TutorialSixWin,NoLose,true),//Level six putting it all together
			new WinCondition(TutorialFiveSixSevenSetup,TutorialSevenWin,NoLose,true),//Level seven putting it all together
			new WinCondition(TutorialEightSetup,TutorialEightWin,TutorialEightLose,true),//Level eight plant introduction
			new WinCondition(TutorialEightSetup,TutorialEightWin,TutorialEightLose,true),//Level nine plant introduction
			new WinCondition(TutorialNineSetup,TutorialNineWin,TutorialNineLose,true),//Level ten flag introduction
			new WinCondition(TutorialNineSetup,TutorialNineWin,TutorialNineLose,true),//Level eleven flag introduction
			new WinCondition(TutorialTenSetup,TutorialTenWin,NoLose,true),//Level twelve flag introduction
		};
	}

	//Start a new tutorial game
	//TODO maybe better in the standard newwinconditions function... just check for 0... meh...
	public void NewTutorial(int level)
	{
		currentConditions.Clear ();

		//if you have passed the tutorial, start loading random levels
		if(level >= tutorial.Count)
		{
			manager.NewRandomLevel(1);
			return;
		}
		WinCondition newCondition = new WinCondition(tutorial[level]);
		newCondition.difficulty = 0;
		newCondition.type = 0;
		
		//If it already exists in the dictionary, select a new one
		currentConditions.Add(newCondition);

		//Set the game up
		GameSetup ();
	}

	public void NewWinConditions(int quantity,int difficulty)
	{
		//Reset the goals
		currentConditions.Clear ();
		//if the game wants to add more win conditions than there are win abilites, set a cap and increase the difficulty insread
		/*if(quantity > conditions.Count) 
		{
			difficulty += (quantity - conditions.Count);
			quantity = conditions.Count;
		}*/
		//Create *quantity* new conditions for winning
		for(int i = 0; i < quantity; i++)
		{
			//Randomly select the type of terrain to grow...
			int newTile = Random.Range(0,TileType.tileString.Length);

			int newRule = Random.Range(0,conditions.Count);
			/*while(existingRules.Contains(newRule))
			{
				//If it's okay to duplicate the rule, then break
				int index = existingRules.IndexOf(newRule);
				if(!conditions[existingRules[index]].onlyOne) 
					break;
				newRule = Random.Range(0,conditions.Count);//find one that isn't being used...
			}*/

			//Select a new random win condition
			WinCondition newCondition = new WinCondition(conditions[newRule]);
			newCondition.difficulty = difficulty;
			newCondition.type = newTile;

			//If it already exists in the dictionary, select a new one
			currentConditions.Add(newCondition);
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

		//Setup all of the game conditions so that they are winnable...
		for(int i = 0; i < currentConditions.Count; i++)
		{
			//Setup the game according to the rules
			currentConditions[i].setup(currentConditions[i].difficulty,currentConditions[i].type);
		}
	}

	//Check to see if won or lost during a turn
	public void Turn()
	{
		HasLost ();
		HasWon ();
	}

	//check if you have won
	public void HasWon()
	{
		//partial win, or won all the condiitons
		bool fullWin = true;
		bool[] win = new bool[currentConditions.Count];

		//Check all the win conditions
		for(int i = 0; i < currentConditions.Count; i++)
		{
			if(currentConditions[i].win(currentConditions[i].difficulty, currentConditions[i].type, ref currentConditions[i].description)) 
				win[i] = true;
		}
		//if all the win conditions are true, you have won
		for(int i = 0; i < win.Length; i++)
		{
			if(!win[i]) fullWin = false;
		}
		//Set the global win to true and increase the difficulty
		Global.win = fullWin;
		if (fullWin)
		{
			goals.Clear();
			Global.levelNumber++;
		}
	}

	//check if the player has lost
	public void HasLost()
	{
		//Check all the lose conditions, just has to lose one
		for(int i = 0; i < currentConditions.Count; i++)
		{
			if(currentConditions[i].lose(currentConditions[i].difficulty, currentConditions[i].type, ref currentConditions[i].description)) 
				Global.lose = true;
		}
	}

	//Check to make sure that the check doesn't try to look for an object that doesn't exist
	private bool inRange(int newX, int newY)
	{
		if (newX >= 0 && newX < manager.getTile.GetLength (0) 
		    && newY >= 0 && newY < manager.getTile.GetLength (1))
			return true;
		else
			return false;
	}
}

//Win condition class, describes how the game is won
public class WinCondition
{
	//Descriptor is fed to the gui to tell the play WTF they are doing
	public string description;

	//Delegates to actually do soething
	public WinManager.setupFunction setup;
	public WinManager.winFunction win;
	public WinManager.loseFunction lose;

	//Holders for difficulty and type
	public int difficulty = new int();
	public int type = new int();
	public bool onlyOne = new bool();

	//Set everything up
	public WinCondition(WinManager.setupFunction setupFunction,WinManager.winFunction winFunction,WinManager.loseFunction loseFunction, bool one)
	{
		setup = setupFunction;
		win = winFunction;
		lose = loseFunction;
		onlyOne = one;
	}

	//Clone an existing win condition so you can have multiple of the same-ish type
	public WinCondition(WinCondition clone)
	{
		setup = (WinManager.setupFunction)clone.setup.Clone();
		win = (WinManager.winFunction)clone.win.Clone();
		lose = (WinManager.loseFunction)clone.lose.Clone();
		onlyOne = clone.onlyOne;

		difficulty = clone.difficulty;
		type = clone.type;
	}
}