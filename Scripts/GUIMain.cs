using UnityEngine;
using System.Collections;

public class GUIMain : MonoBehaviour 
{
	public TileManager tiles;
	int ButtonHeight = 80;
	int ButtonWidth = Screen.width/4;
	int element;

	void OnGUI ()
	{
		//Buttons
		GUI.BeginGroup(new Rect (0,Screen.height-ButtonHeight,Screen.width,ButtonHeight));

		if(GUI.Button(new Rect(0,0,ButtonWidth,ButtonHeight),"Fire"))
		{
			element = (int)TileType.element.FIRE;
		}
		if(GUI.Button(new Rect(ButtonWidth,0,ButtonWidth,ButtonHeight),"Water"))
		{
			element = (int)TileType.element.WATER;
		}
		if(GUI.Button(new Rect(ButtonWidth*2,0,ButtonWidth,ButtonHeight),"Earth"))
		{
			element = (int)TileType.element.EARTH;
		}
		if(GUI.Button(new Rect(ButtonWidth*3,0,ButtonWidth,ButtonHeight),"Air"))
		{
			element = (int)TileType.element.AIR;
		}

		GUI.EndGroup ();

		//If you click on a tile, call the function to change that tile using the element variable
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit click;
			if (Physics.Raycast (ray, out click)) 
			{
				tiles.ChangeType (click.transform.gameObject, element);
			}		
		}
	}

	//Lookup which terrain combo results in which new tile type
}