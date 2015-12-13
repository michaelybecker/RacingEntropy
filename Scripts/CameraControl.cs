using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public TileManager tiles;
	public float yHeight;

	public void Init()
	{
		yHeight = tiles.boardSize.x-2;
		int centerX = (int)(tiles.getTile.GetLength (0) / 2);
		int centerY = (int)(tiles.getTile.GetLength (1) / 2);

		//Debug.Log (centerX);
		//Debug.Log (centerY);

		Vector3 LookHere = tiles.objectFromTile [tiles.getTile[centerX,centerY]].transform.position;
		//LookHere = Camera.main.ScreenToWorldPoint(LookHere); 

		transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime); //I have no idea why this works. But it does. Because rawr.

		//transform.position = new Vector3 (centerX+2, yHeight+2, tiles.boardSize.y-5); //old way

		//Debug.Log ("look at " + LookHere);
		transform.LookAt (LookHere);
	}
}