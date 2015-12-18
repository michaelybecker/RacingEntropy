using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	
	public LayerMask castMask = -257;
	
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

			//Score and level
			GUI.BeginGroup (new Rect(ButtonWidth * 2, ButtonWidth/3,Screen.width-(ButtonWidth * 4),ButtonWidth));
			GUI.DrawTexture(new Rect(0,0,Screen.width-(ButtonWidth * 4),ButtonWidth),Resource.CenterConsole);
			for(int i = 0; i < tiles.winControl.currentConditions.Count; i++)
			{
				GUI.Label(new Rect(0,i*20,Screen.width-(ButtonWidth * 4),ButtonWidth),tiles.winControl.currentConditions[i].description);
			}
			GUI.EndGroup();

			//Buttons
			GUI.BeginGroup (new Rect (Screen.width - ButtonWidth, Buffer * 3, ButtonWidth, Screen.height));

			if(Input.GetKeyDown(KeyCode.Q))
			{
				element = (int)TileType.element.FIRE;
				Cursor.SetCursor (Resource.Fire_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if(Input.GetKeyDown(KeyCode.W))
			{
				element = (int)TileType.element.WATER;
				Cursor.SetCursor (Resource.Water_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if(Input.GetKeyDown(KeyCode.E))
			{
				element = (int)TileType.element.EARTH;
				Cursor.SetCursor (Resource.Earth_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}
			if(Input.GetKeyDown(KeyCode.R))
			{
				element = (int)TileType.element.AIR;
				Cursor.SetCursor (Resource.Air_Cursor, Vector2.zero, CursorMode.Auto);
				sounds.Play (Resource.Click, 1f);
			}

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
			if (Physics.Raycast (ray, out click, Mathf.Infinity, castMask)) // Added layerMask to avoid hitting mouseDrag plane.
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