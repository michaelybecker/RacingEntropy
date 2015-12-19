using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class WinManager : MonoBehaviour
{
	public int tutorialProgression;

	public void TutorialOneSetup(int difficulty, int type)
	{
		Global.elementUnlock = new bool[]{false,false,true,false};
		int D = (int)TileType.tile.DESERT;
		int C = (int)TileType.tile.CRAGS;
		int[,] newMap = new int[,]
		{
			{D,D,D,D,C,D,D,D,D,C},
			{D,D,D,C,C,C,D,D,C,C},
			{D,D,D,D,C,D,D,D,C,D},
			{C,D,D,D,C,C,D,C,C,D},
			{C,D,D,D,D,D,D,D,D,D},
			{D,D,D,C,C,D,D,D,D,D},
			{D,D,D,C,C,C,D,D,D,D},
			{D,D,D,D,C,C,D,D,D,D},
			{D,D,D,D,C,C,D,D,D,D},
		};
		manager.CreateMap(newMap);
	}
	public void TutorialTwoSetup(int difficulty, int type)
	{
		Global.elementUnlock = new bool[]{true,false,false,false};
		int D = (int)TileType.tile.LAKE;
		int C = (int)TileType.tile.MARSH;
		int[,] newMap = new int[,]
		{
			{D,D,D,D,C,D,D,D,D,C},
			{D,D,D,C,C,C,D,D,C,C},
			{D,D,D,D,C,D,D,D,C,D},
			{C,D,D,D,C,C,D,C,C,D},
			{C,D,D,D,D,D,D,D,D,D},
			{D,D,D,C,C,D,D,D,D,D},
			{D,D,D,C,C,C,D,D,D,D},
			{D,D,D,D,C,C,D,D,D,D},
			{D,D,D,D,C,C,D,D,D,D},
		};
		manager.CreateMap(newMap);
	}
	public void TutorialThreeSetup(int difficulty, int type)
	{
		Global.elementUnlock = new bool[]{false,false,false,true};
		int D = (int)TileType.tile.PLAIN;
		int C = (int)TileType.tile.FOREST;
		int[,] newMap = new int[,]
		{
			{D,D,D,D,C,D,D,D,D,C},
			{D,D,D,C,C,C,D,D,C,C},
			{D,D,D,D,C,D,D,D,C,D},
			{C,D,D,D,C,C,D,C,C,D},
			{C,D,D,D,D,D,D,D,D,D},
			{D,D,D,C,C,D,D,D,D,D},
			{D,D,D,C,C,C,D,D,D,D},
			{D,D,D,D,C,C,D,D,D,D},
			{D,D,D,D,C,C,D,D,D,D},
		};
		manager.CreateMap(newMap);
	}
	public void TutorialFourSetup(int difficulty, int type)
	{
		Global.elementUnlock = new bool[]{false,true,false,false};
		int D = (int)TileType.tile.FOREST;
		int C = (int)TileType.tile.CRAGS;
		int[,] newMap = new int[,]
		{
			{D,D,D,D,C,D,D,D,D,C},
			{D,D,D,C,C,C,D,D,C,C},
			{D,D,D,D,C,D,D,D,C,D},
			{C,D,D,D,C,C,D,C,C,D},
			{C,D,D,D,D,D,D,D,D,D},
			{D,D,D,C,C,D,D,D,D,D},
			{D,D,D,C,C,C,D,D,D,D},
			{D,D,D,D,C,C,D,D,D,D},
			{D,D,D,D,C,C,D,D,D,D},
		};
		manager.CreateMap(newMap);
	}
	public void TutorialFiveSixSevenSetup(int difficulty, int type)
	{
		Global.elementUnlock = new bool[]{true,true,true,true};
		manager.mapControl.GenerateBiomes (5,5);
	}

	//Grow x number of plants
	public void TutorialEightSetup (int difficulty, int type)
	{
		type = (int)TileType.tile.PLAIN;

		int x = 5;
		int y = 5;
		manager.mapControl.GenerateBiomes (x,y);

		GrowPlantSetup (1, type);
	}

	//Introduce the flag
	public void TutorialNineSetup (int difficulty, int type)
	{
		manager.mapControl.GenerateBiomes (10,10);

		FlagSetup (1, 0);
	}

	//Introduce disasters
	public void TutorialTenSetup (int difficulty, int type)
	{
		int M = (int)TileType.tile.MARSH;
		int L = (int)TileType.tile.LAKE;
		int P = (int)TileType.tile.PLAIN;
		int C = (int)TileType.tile.CRAGS;
		Global.elementUnlock = new bool[]{false,false,false,false};
		Global.gameSpeed = 0.1f;
		int[,] newMap = new int[,]
		{
			{C,C,C,C,C,C,C},
			{C,P,P,P,P,P,C},
			{C,P,M,M,M,P,C},
			{C,P,M,L,M,P,C},
			{C,P,M,M,M,P,C},
			{C,P,P,P,P,P,C},
			{C,C,C,C,C,C,C},
		};
		manager.CreateMap(newMap);
		manager.AddDisaster (3, 3,(int)TileType.disaster.FLOOD);
	}
}

