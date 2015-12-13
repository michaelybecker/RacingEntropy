using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
	
	public GUISkin style;
	int MenuWidth;
	int MenuHeight;
	int Buffer;
	public bool StartWindowOpen =false;
	public bool SettingsWindowOpen = false;

	void OnGUI()
	{
		GUI.skin = style;
		if (StartWindowOpen) 
		{
			Rect window = new Rect (0, 0, Screen.width, Screen.height);
			window = GUI.ModalWindow (0, window, StartMenu, Resource.TitleBackground);
			Global.pause = true;
		}
	}

	void StartMenu(int ID)
	{
		MenuWidth = Screen.width / 3;
		MenuHeight = Screen.height / 3;

		Buffer = MenuHeight / 3;

		GUI.BeginGroup (new Rect ((Screen.width/2)-(MenuWidth/2), (Screen.height/2)-(MenuHeight/2), MenuWidth, MenuHeight));

		if(GUI.Button(new Rect(0,0,MenuWidth,Buffer),Resource.NG_Btn))
		{
			SettingsWindowOpen = true;
			StartWindowOpen = false;
		}
		if(GUI.Button(new Rect(0,Buffer,MenuWidth,Buffer),Resource.ContinueGame_Btn))
		{
			StartWindowOpen = false;
			Global.pause = false;
		}
		if(GUI.Button(new Rect(0,Buffer*2,MenuWidth,Buffer),Resource.QuitGame_Btn))
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

		if(GUI.Button(new Rect(0,0,MenuWidth,Buffer),Resource.LowDifficulty_Btn))
		{
			//send "1" to create map function
		}
		if(GUI.Button(new Rect(0,Buffer,MenuWidth,Buffer),Resource.MediumDifficulty_Btn))
		{
			//send "2" to create map function
		}
		if(GUI.Button(new Rect(0,Buffer*2,MenuWidth,Buffer),Resource.HardDifficulty_Btn))
		{
			//send "3" to create map function
		}
		if(GUI.Button(new Rect(0,Buffer*3,MenuWidth,Buffer),"Back"))
		{
			//back to the previous menu
			StartWindowOpen = true;
			SettingsWindowOpen = false;
		}
	}
}

