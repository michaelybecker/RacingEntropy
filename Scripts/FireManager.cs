using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireManager : MonoBehaviour
{
	//Data holder for map data
	public TileManager manager;
	//list of tiles with fires
	public Dictionary<Tile,GameObject> fireTiles = new Dictionary<Tile,GameObject>();
	
	private intVector2[] directions = new intVector2[]{
		new intVector2(1,0),
		new intVector2(0,1),
		new intVector2(-1,0),
		new intVector2(0,-1),
		new intVector2(0,0)
	};
	
	public void Grow()
	{
		intVector2 idealSpace = null;
		float mostFertile = 0;
		
		//foreach(KeyValuePair<Tile,GameObject> fire in fireTiles)
		List<Tile> fire = new List<Tile> (fireTiles.Keys);
		for(int i = 0; i < fire.Count; i++)
		{
			foreach(intVector2 dir in directions)
			{
				if(fire[i].x+dir.x >= 0 && fire[i].x+dir.x < manager.getTile.GetLength(0) &&
				   fire[i].y+dir.y >= 0 && fire[i].y+dir.y < manager.getTile.GetLength(1))
				{
					Tile tempTile = manager.getTile[fire[i].x+dir.x,fire[i].y+dir.y];
					if(tempTile.fire == false)
					{
						if(tempTile.growthFactor > mostFertile)
						{
							idealSpace = new intVector2(fire[i].x+dir.x,fire[i].y+dir.y);
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
			AddFire(idealSpace.x,idealSpace.y);
		}
	}
	
	public void KillFire(Tile tile)
	{
		tile.fire = false;
		tile.burnout = 0;
		if(fireTiles.ContainsKey(tile))
		{
			GameObject fire = fireTiles[tile];
			fireTiles.Remove (tile);
			DestroyImmediate(fire);
		}
		//If you have succesfully put out the fires destroy it
		if(fireTiles.Count == 0) 
		{
			Debug.Log("Deleting fire object");
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
		if(!fireTiles.ContainsKey(newTile))fireTiles.Add (newTile,newfire);

		//Modify the tile according to if it were hit with a fire element
		manager.getTile [x, y].Change ((int)TileType.element.FIRE);
		manager.Change (tile,manager.getTile[x,y]);

		MeshFilter filter = newfire.AddComponent<MeshFilter> ();
		MeshRenderer renderer = newfire.AddComponent<MeshRenderer>();
		
		filter.mesh = Resource.fireMesh;
		renderer.material = Resource.fireMaterial;
		newTile.burnout = newTile.flammability;
		
		newfire.transform.parent = transform;
		newfire.transform.position = new Vector3 (tile.transform.position.x, tile.transform.position.y+manager.worldScale.y, tile.transform.position.z);
	}
}

