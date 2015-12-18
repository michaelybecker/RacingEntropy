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

	//Holder for the flags
	public List<Tile> goals = new List<Tile> ();
	//Get to the flag with any plant
	public void FlagSetup (int difficulty)
	{
		PlainPlantSetup (difficulty);
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
	public void PlainPlantSetup (int difficulty)
	{
		int x = manager.getTile.GetLength (0)-1;
		int y = manager.getTile.GetLength (1)-1;
		int randx = Random.Range (0, x);
		int randy = Random.Range(0,y);
		manager.getTile [randx, randy].setTile((int)TileType.tile.PLAIN);
		manager.Change (manager.getTile [randx, randy]);

		for(int i = 0; i < dir4.Length; i++)
		{
			int newX = randx+dir4[i].x;
			int newY = randy+dir4[i].y;
			if(inRange(newX,newY))
			{
				manager.getTile [newX,newY].setTile((int)TileType.tile.PLAIN);
				manager.Change (manager.getTile [newX,newY]);
			}
			else i--;
		}
		manager.AddPlant (randx, randy, (int)TileType.tile.PLAIN);
	}

	//Have a certain number of tiles
	public void DesertTilesSetup(int difficulty)
	{
		//Don't need to do anything special for setup
	}
}

