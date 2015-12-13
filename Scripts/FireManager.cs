using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireManager : MonoBehaviour
{
	//Data holder for map data
	public TileManager manager;
	//list of tiles with fires
	public List<Tile> fireTiles = new List<Tile>();
	
	private intVector2[] directions = new intVector2[]{
		new intVector2(1,0),
		new intVector2(0,1),
		new intVector2(-1,0),
		new intVector2(0,-1),
		new intVector2(0,0)
	};
	
	public void Grow()
	{
		Debug.Log ("should be growing");
		intVector2 idealSpace = null;
		float mostFertile = 0;
		
		for(int i = 0 ; i < fireTiles.Count; i++)
		{
			foreach(intVector2 dir in directions)
			{
				if(fireTiles[i].x+dir.x >= 0 && fireTiles[i].x+dir.x < manager.getTile.GetLength(0) &&
				   fireTiles[i].y+dir.y >= 0 && fireTiles[i].y+dir.y < manager.getTile.GetLength(1))
				{
					Tile tempTile = manager.getTile[fireTiles[i].x+dir.x,fireTiles[i].y+dir.y];
					if(tempTile.fire == false)
					{
						if(tempTile.growthFactor > mostFertile)
						{
							idealSpace = new intVector2(fireTiles[i].x+dir.x,fireTiles[i].y+dir.y);
							mostFertile = tempTile.flammability;
						}
					}
					else
					{
						tempTile.burnout--;
						if(tempTile.burnout < 0) KillFire(tempTile);
					}
				}
			}
		}
		if(idealSpace != null) 
		{
			Tile growTile = manager.getTile[idealSpace.x,idealSpace.y];
			AddFire(idealSpace);
		}
	}
	
	public void KillFire(Tile tile)
	{
		tile.fire = false;
		tile.burnout = 0;
		Transform fire = manager.objectFromTile [tile].transform.Find ("fire");
		fireTiles.Remove (tile);
		if(fire != null)
		{
			DestroyImmediate(fire.gameObject);
		}
		//If you have succesfully put out the fires destroy it
		if(fireTiles.Count == 0) 
		{
			manager.fires.Remove(this);
			DestroyImmediate(transform.gameObject);
		}
	}
	
	public void KillFire(int x, int y)
	{
		KillFire (manager.getTile [x, y]);
	}
	
	public void AddFire(int x, int y)
	{
		GameObject tile = manager.objectFromTile [manager.getTile[x,y]];
		Tile newTile = manager.tileFromObject [tile];
		GameObject newfire = new GameObject ("fire");
		newTile.fire = true;
		fireTiles.Add (newTile);

		//Modify the tile according to if it were hit with a fire element
		manager.getTile [x, y].Change ((int)TileType.element.FIRE);
		manager.Change (tile,manager.getTile[x,y]);

		MeshFilter filter = newfire.AddComponent<MeshFilter> ();
		MeshRenderer renderer = newfire.AddComponent<MeshRenderer>();
		
		filter.mesh = Resource.fireMesh;
		renderer.material = Resource.fireMaterial;
		newTile.burnout = (int)(1 / newTile.flammability)+1;
		Debug.Log (newTile.burnout + " until burn out");
		
		newfire.transform.parent = tile.transform;
		newfire.transform.position = new Vector3 (tile.transform.position.x, tile.transform.position.y+manager.worldScale.y, tile.transform.position.z);
	}
	
	public void AddFire(intVector2 pos)
	{
		AddFire (pos.x, pos.y);
	}
}

