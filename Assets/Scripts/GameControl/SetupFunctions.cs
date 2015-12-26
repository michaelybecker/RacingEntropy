using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class WinManager : MonoBehaviour
{
	public intVector2[] dir4 = new  intVector2[]{
		new intVector2(0,1),
		new intVector2(1,0),
		new intVector2(0,-1),
		new intVector2(-1,0),
	};

	public void BaseSetup(int difficulty)
	{
		//build the map
		//TODO move the map building to here
	}

	//Holder for the flags
	public List<Tile> goals = new List<Tile> ();
	//Get to the flag with any plant
	public void FlagSetup (int difficulty, int type)
	{
		goals = new List<Tile> ();
		GrowPlantSetup (difficulty, type);
		//Place the flag somewhere
		int x = manager.getTile.GetLength (0)-1;
		int y = manager.getTile.GetLength (1)-1;

		for(int i = 0; i < difficulty; i++)
		{
			int randx = Random.Range (0, x);
			int randy = Random.Range(0,y);

			if(inRange(randx,randy))
			{
				manager.getTile [randx, randy].setTile((int)TileType.tile.GOAL);
				manager.Change (manager.getTile [randx, randy]);
				goals.Add(manager.getTile [randx, randy]);
			}
			else i--;
		}
	}

	//Grow x number of plants
	public void GrowPlantSetup (int difficulty, int type)
	{
		int x = manager.getTile.GetLength (0)-1;
		int y = manager.getTile.GetLength (1)-1;
		int randx = Random.Range (0, x);
		int randy = Random.Range(0,y);
		manager.getTile [randx, randy].setTile(type);
		manager.Change (manager.getTile [randx, randy]);

		for(int i = 0; i < dir4.Length; i++)
		{
			int newX = randx+dir4[i].x;
			int newY = randy+dir4[i].y;
			if(inRange(newX,newY))
			{
				manager.getTile [newX,newY].setTile(type);
				manager.Change (manager.getTile [newX,newY]);
			}
		}
		manager.AddPlant (randx, randy, type);
	}

	//Have a certain number of tiles
	public void NumberOfTilesSetup(int difficulty, int type){}//Don't need to do anything special for setup

	//Survive
	public float survivalTime;
	public void SurvivalSetup(int difficulty, int type)//Just get the starting time and add a few more disasters to make things interesting
	{
		int x = manager.getTile.GetLength (0)-1;
		int y = manager.getTile.GetLength (1)-1;

		GrowPlantSetup (difficulty, type);

		for(int i = 0; i < difficulty; i++)
		{
			int randx = Random.Range (0, x);
			int randy = Random.Range(0,y);
			
			if(inRange(randx,randy))
			{
				manager.AddDisaster(randx,randy);
			}
		}
		survivalTime = Time.time;
	} 
}

