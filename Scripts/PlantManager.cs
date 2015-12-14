using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlantManager : MonoBehaviour
{
	//Data holder for map data
	public TileManager manager;
	//list of tiles with plants
	public List<Tile> plantTiles = new List<Tile>();

	private intVector2[] directions = new intVector2[]{
		new intVector2(1,0),
		new intVector2(0,1),
		new intVector2(-1,0),
		new intVector2(0,-1)
	};

	public void Grow()
	{
		intVector2 idealSpace = null;
		float mostFertile = 0;

		for(int i = 0 ; i < plantTiles.Count; i++)
		{
			foreach(intVector2 dir in directions)
			{
				if(plantTiles[i].x+dir.x >= 0 && plantTiles[i].x+dir.x < manager.getTile.GetLength(0) &&
				   plantTiles[i].y+dir.y >= 0 && plantTiles[i].y+dir.y < manager.getTile.GetLength(1))
				{
					Tile tempTile = manager.getTile[plantTiles[i].x+dir.x,plantTiles[i].y+dir.y];
					if(tempTile.plant == false)
					{
						if(tempTile.growthFactor > mostFertile)
						{
							idealSpace = new intVector2(plantTiles[i].x+dir.x,plantTiles[i].y+dir.y);
							mostFertile = tempTile.growthFactor;
						}
					}
				}
			}
		}
		if(idealSpace != null) 
		{
			Tile growTile = manager.getTile[idealSpace.x,idealSpace.y];
			growTile.growthFactor += mostFertile;
			if(growTile.growthFactor > 1)
			{
				AddPlant(idealSpace);
				growTile.growthFactor = 0;
			}
		}
	}

	public void KillPlant(Tile tile)
	{
		tile.plant = false;
		Transform plant = manager.objectFromTile [tile].transform.Find ("Plant");
		plantTiles.Remove (tile);
		if(plant != null)
		{
			DestroyImmediate(plant.gameObject);
		}
		if(plantTiles.Count == 0) 
		{
			Global.lose = true;
		}
	}

	public void KillPlant(int x, int y)
	{
		KillPlant (manager.getTile [x, y]);
	}

	public void AddPlant(int x, int y)
	{
		GameObject tile = manager.objectFromTile [manager.getTile[x,y]];
		Tile newTile = manager.tileFromObject [tile];
		GameObject newPlant = new GameObject ("Plant");
		newPlant.transform.Rotate (new Vector3 (0,135,0));
		newTile.plant = true;
		plantTiles.Add (newTile);
		if(newTile.type == (int)TileType.tile.GOAL)
		{
			Global.win = true;
			Global.levelNumber++;
		}
		//TODO add plant mesh thingy
		MeshFilter filter = newPlant.AddComponent<MeshFilter> ();
		MeshRenderer renderer = newPlant.AddComponent<MeshRenderer>();

		filter.mesh = Resource.plantMesh;
		renderer.material = Resource.plantMaterial;

		newPlant.transform.parent = tile.transform;
		newPlant.transform.position = new Vector3 (tile.transform.position.x, tile.transform.position.y+manager.worldScale.y, tile.transform.position.z);
	}

	public void AddPlant(intVector2 pos)
	{
		AddPlant (pos.x, pos.y);
	}
}

