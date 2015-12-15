using UnityEngine;
using System.Collections;

public class GUIMain : MonoBehaviour 
{
	public TileManager tiles;
	public MenuController menu;
	public AudioManager sounds;
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

		if (!Global.pause) 
		{	
			ButtonWidth = ButtonHeight = Buffer;
			if (GUI.Button (new Rect (0, 0, ButtonWidth * 2, ButtonWidth), Resource.Menu_Btn)) {
				sounds.Play (Resource.Click, 1f);
				menu.StartWindowOpen = true;
			}

			//Buttons
			GUI.BeginGroup (new Rect (Screen.width - ButtonWidth, Buffer * 3, ButtonWidth, Screen.height));

			if (GUI.Button (new Rect (0, 0, ButtonWidth, ButtonHeight), Resource.Fire_Btn)) {
				element = (int)TileType.element.FIRE;
				Cursor.SetCursor (Resource.Fire_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if (GUI.Button (new Rect (0, ButtonHeight, ButtonWidth, ButtonHeight), Resource.Water_Btn)) {
				element = (int)TileType.element.WATER;
				Cursor.SetCursor (Resource.Water_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if (GUI.Button (new Rect (0, ButtonHeight * 2, ButtonWidth, ButtonHeight), Resource.Earth_Btn)) {
				element = (int)TileType.element.EARTH;
				Cursor.SetCursor (Resource.Earth_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if (GUI.Button (new Rect (0, ButtonHeight * 3, ButtonWidth, ButtonHeight), Resource.Air_Btn)) {
				element = (int)TileType.element.AIR;
				Cursor.SetCursor (Resource.Air_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}

			GUI.EndGroup ();
		}
	}

	void Update()
	{
		if (!Global.pause) 
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit click;
			if (Physics.Raycast (ray, out click)) 
			{
				if (Input.GetMouseButtonDown (0)) 
				{
					tiles.ChangeType (click.transform.gameObject, element);
				}
				tiles.OnHover (click.transform.gameObject);
			}
		}
	}
}