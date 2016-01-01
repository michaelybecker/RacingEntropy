using UnityEngine;
using System.Collections;

public class Plant
{
	//tile the plant if located on
	public Tile tile;
	//plant object which is rendered
	public GameObject instance;
	//delegate detailing the 
	public TileType.growFunction function;
	public int type;

	//if the plant is fully grown, then it can spread
	public bool grown;
	public float growthValue;

	//Creates a new plant with a function defining growth patterns
	public Plant(TileType.growFunction newFunction, GameObject newInstance, Tile newTile, int newType)
	{
		instance = newInstance;
		function = newFunction;
		tile = newTile;
		type = newType;
	}

	public void Kill()
	{
		GameObject.DestroyImmediate (instance);
	}
}

