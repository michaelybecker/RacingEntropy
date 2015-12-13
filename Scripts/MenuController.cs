using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
	
	int MenuWidth;
	int MenuHeight;
	int Buffer;
	public bool StartWindowOpen =false;
	public bool SettingsWindowOpen = false;

	void OnGUI()
	{
		if (StartWindowOpen) 
		{
			Rect window = new Rect (0, 0, Screen.width, Screen.height);
			window = GUI.ModalWindow (0, window, StartMenu, "");
			Global.pause = true;
		}
	}

	void StartMenu(int ID)
	{
		MenuWidth = Screen.width / 3;
		MenuHeight = Screen.height / 3;

		Buffer = MenuHeight / 3;

		GUI.BeginGroup (new Rect ((Screen.width/2)-(MenuWidth/2), (Screen.height/2)-(MenuHeight/2), MenuWidth, MenuHeight));

		if(GUI.Button(new Rect(0,0,MenuWidth,Buffer),"Start New Game"))
		{
			//create a new map
		}
		if(GUI.Button(new Rect(0,Buffer,MenuWidth,Buffer),"Continue"))
		{
			StartWindowOpen = false;
			Global.pause = false;
		}
		if(GUI.Button(new Rect(0,Buffer*2,MenuWidth,Buffer),"Exit"))
		{
			//exit the game
		}

		GUI.EndGroup();
	}

	void SettingsMenu(int ID)
	{
		MenuWidth = Screen.width / 3;
		MenuHeight = Screen.height / 3;

		Buffer = MenuHeight / 4;

		if(GUI.Button(new Rect(0,0,MenuWidth,Buffer),"Low"))
		{
			//send "1" to create map function
		}
		if(GUI.Button(new Rect(0,Buffer,MenuWidth,Buffer),"Medium"))
		{
			//send "2" to create map function
		}
		if(GUI.Button(new Rect(0,Buffer*2,MenuWidth,Buffer),"Hard"))
		{
			//send "3" to create map function
		}
		if(GUI.Button(new Rect(0,Buffer*3,MenuWidth,Buffer),"Back"))
		{
			//back to the previous menu
			SettingsWindowOpen = false;
			StartWindowOpen = true;
		}
	}
}

