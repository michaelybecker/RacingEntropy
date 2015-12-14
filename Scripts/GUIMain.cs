using UnityEngine;
using System.Collections;

public class GUIMain : MonoBehaviour 
{
	public TileManager tiles;
	public MenuController menu;
	int Buffer = Screen.height/10; //measurement for spacing and button dimensions -- to scale with screen
	int ButtonHeight;
	int ButtonWidth;
	public GUISkin style;

	bool firstRun = true;

	int element;

	void OnGUI ()
	{
		if (firstRun) {
			menu.StartWindowOpen = true;
			firstRun = false;
		}

		GUI.skin = style;
		ButtonWidth = ButtonHeight = Buffer;
		if (GUI.Button (new Rect (0, 0, ButtonWidth * 2, ButtonWidth), Resource.Menu_Btn)) {
			menu.StartWindowOpen = true;
		}

		//Buttons
		GUI.BeginGroup (new Rect (Screen.width - ButtonWidth, Buffer * 3, ButtonWidth, Screen.height));

		if (GUI.Button (new Rect (0, 0, ButtonWidth, ButtonHeight), Resource.Fire_Btn)) {
			element = (int)TileType.element.FIRE;
		}
		if (GUI.Button (new Rect (0, ButtonHeight, ButtonWidth, ButtonHeight), Resource.Water_Btn)) {
			element = (int)TileType.element.WATER;
		}
		if (GUI.Button (new Rect (0, ButtonHeight * 2, ButtonWidth, ButtonHeight), Resource.Earth_Btn)) {
			element = (int)TileType.element.EARTH;
		}
		if (GUI.Button (new Rect (0, ButtonHeight * 3, ButtonWidth, ButtonHeight), Resource.Air_Btn)) {
			element = (int)TileType.element.AIR;
		}

		GUI.EndGroup ();

	}

	void Update()
	{
		Global.pause = false;
		if (Input.GetMouseButtonDown (0)) 
		{
			if (!Global.pause) 
			{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit click;
				if (Physics.Raycast (ray, out click)) 
				{
					tiles.ChangeType (click.transform.gameObject, element);
				}
			}
		}
	}
}