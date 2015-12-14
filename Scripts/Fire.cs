using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{
	public TileManager manager;
	public Tile tile;

	private int burnout;
	
	private intVector2[] directions = new intVector2[]{
		new intVector2(1,0),
		new intVector2(0,1),
		new intVector2(-1,0),
		new intVector2(0,-1)
	};

	public void StartFire(Tile newTile)
	{
		tile = newTile;
		tile.fire = true;
		burnout = tile.burnout;
		MeshFilter filter = gameObject.AddComponent<MeshFilter> ();
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
		
		filter.mesh = Resource.fireMesh;
		renderer.material = Resource.fireMaterial;

		Vector3 FirePosition = 
		transform.position = new Vector3 (
				manager.objectFromTile[tile].transform.position.x,
				manager.objectFromTile[tile].transform.position.y+manager.worldScale.y, 
				manager.objectFromTile[tile].transform.position.z);

		newTile.Change ((int)TileType.element.FIRE);
		manager.Change (manager.objectFromTile [tile], tile);
	}

	public void Grow()
	{
		int flamability = 0;
		Tile bestTile = tile;

		foreach(intVector2 dir in directions)
		{
			int xCheck = dir.x + tile.x;
			int yCheck = dir.y + tile.y;

			if(xCheck >= 0 && xCheck < manager.getTile.GetLength(0) &&
			   yCheck >= 0 && yCheck < manager.getTile.GetLength(1))
			{
				Tile checkTile = manager.getTile[xCheck,yCheck];
				if(!checkTile.fire)//if there isn't a fire here
				{
					if(checkTile.flammability > flamability)
					{
						bestTile = checkTile;
					}
				}
			}
		}
		if(bestTile != tile)manager.AddFire(bestTile.x,bestTile.y);
		else Debug.Log("nothing else flammable around");
		burnout--;
		if (burnout < 0)
			Kill ();
	}

	public void Kill()
	{
		Debug.Log ("killing self");
		manager.fires.Remove (this);
		DestroyImmediate (gameObject);
	}
}

