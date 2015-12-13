using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	public TileManager tiles;
	float yHeight;

	void Init()
	{
		yHeight = tiles.boardSize.x;
		int centerX = (int)(tiles.getTile.GetLength (0) / 2);
		int centerY = (int)(tiles.getTile.GetLength (1) / 2);

		Debug.Log (centerX);
		Debug.Log (centerY);

		Vector3 LookHere = tiles.tileFromCoordinate [new intVector2 (centerX, centerY)].transform.position;

		transform.LookAt (LookHere);

		transform.position = new Vector3 (tiles.boardSize.x, yHeight, tiles.boardSize.y);
	}
}