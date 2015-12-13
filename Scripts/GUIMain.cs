using UnityEngine;
using System.Collections;

public class GUIMain : MonoBehaviour 
{
	public TileManager tiles;
	int Buffer = Screen.height/8; //measurement for spacing and button dimensions -- to scale with screen
	int ButtonHeight;
	int ButtonWidth;

	int element;

	void OnGUI ()
	{
		ButtonWidth = ButtonHeight = Buffer;

		//Buttons
		GUI.BeginGroup(new Rect (Screen.width-ButtonWidth,Buffer*2,ButtonWidth,Screen.height));

		//if(GUI.Button(new Rect(0,0,ButtonWidth,ButtonHeight),"Fire"))
		//{
		//	element = (int)TileType.element.FIRE;
		//}
		if(GUI.Button(new Rect(0,0,ButtonWidth,ButtonHeight),"Fire"))
		{
			element = (int)TileType.element.FIRE;
		}
		if(GUI.Button(new Rect(0,ButtonHeight,ButtonWidth,ButtonHeight),"Water"))
		{
			element = (int)TileType.element.WATER;
		}
		if(GUI.Button(new Rect(0,ButtonHeight*2,ButtonWidth,ButtonHeight),"Earth"))
		{
			element = (int)TileType.element.EARTH;
		}
		if(GUI.Button(new Rect(0,ButtonHeight*3,ButtonWidth,ButtonHeight),"Air"))
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