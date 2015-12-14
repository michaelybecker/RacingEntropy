using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
	
	public TileManager tiles;
	public GUISkin style;
	int MenuWidth;
	int MenuHeight;
	int Buffer;
	public bool StartWindowOpen = true;
	public bool SettingsWindowOpen = false;
	public bool LoseWindowOpen = false;
	public bool WinWindowOpen = false;

	void OnGUI()
	{
		MenuWidth = Screen.width / 3;
		MenuHeight = Screen.height / 3;

		GUI.skin = style;
		if (StartWindowOpen) 
		{
			Rect window = new Rect (0, 0, Screen.width, Screen.height);
			window = GUI.ModalWindow (0, window, StartMenu, Resource.TitleBackground);
			Global.pause = true;
		}
		if (SettingsWindowOpen) 
		{
			Rect window = new Rect (0, 0, Screen.width, Screen.height);
			window = GUI.ModalWindow (0, window, SettingsMenu, Resource.TitleBackground);
			Global.pause = true;
		}
		if (Global.lose) 
		{
			Rect window = new Rect (0, 0, Screen.width, Screen.height);
			window = GUI.ModalWindow (0, window, LoseMenu, "");
			Global.pause = true;
		}
		if (Global.win)
		{
			Rect window = new Rect (0, 0, Screen.width, Screen.height);
			window = GUI.ModalWindow (0, window, WinMenu, "");
			Global.pause = true;
		}
	}

	void StartMenu(int ID)
	{
		/*MenuWidth = Screen.width / 3;
		MenuHeight = Screen.height / 3;*/

		Buffer = MenuHeight / 3;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.width/3), Resource.Title, ScaleMode.ScaleToFit);

		GUI.BeginGroup (new Rect ((Screen.width / 2) - (MenuWidth / 2), Screen.height - MenuHeight, MenuWidth, MenuHeight));

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
			//exit game
		}

		GUI.EndGroup();
	}

	void SettingsMenu(int ID)
	{
		/*MenuWidth = Screen.width / 3;
		MenuHeight = Screen.height / 3;*/

		Buffer = MenuHeight / 4;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.width/3), Resource.Title, ScaleMode.ScaleToFit);

		GUI.BeginGroup (new Rect ((Screen.width / 2) - (MenuWidth / 2), Screen.height - MenuHeight, MenuWidth, MenuHeight));

		if (GUI.Button (new Rect (0, 0, MenuWidth, Buffer), Resource.LowDifficulty_Btn)) {
			//send "1" to create map function
			tiles.NewLevel (1);
			SettingsWindowOpen = false;
		}
		if (GUI.Button (new Rect (0, Buffer, MenuWidth, Buffer), Resource.MediumDifficulty_Btn)) {
			//send "2" to create map function
			tiles.NewLevel (2);
			SettingsWindowOpen = false;
		}
		if (GUI.Button (new Rect (0, Buffer * 2, MenuWidth, Buffer), Resource.HardDifficulty_Btn)) {
			//send "3" to create map function
			tiles.NewLevel (3);
			SettingsWindowOpen = false;
		}
		if (GUI.Button (new Rect (0, Buffer * 3, MenuWidth, Buffer), Resource.Back_Btn)) {
			//back to the previous menu
			StartWindowOpen = true;
			SettingsWindowOpen = false;
		}

		GUI.EndGroup ();
	}

	void LoseMenu(int ID)
	{
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.width/3), Resource.Lose, ScaleMode.ScaleToFit);

		GUI.BeginGroup (new Rect ((Screen.width / 2) - (MenuWidth / 2), Screen.height - MenuHeight, MenuWidth, MenuHeight));

		if(GUI.Button(new Rect(0,0,MenuWidth,Buffer),Resource.NG_Btn))
		{
			SettingsWindowOpen = true;
			LoseWindowOpen = false;
			Debug.Log ("Settings :" + SettingsWindowOpen + "loseWindow" + LoseWindowOpen + "start " + StartWindowOpen);
		}
		if(GUI.Button(new Rect(0,Buffer,MenuWidth,Buffer),Resource.QuitGame_Btn))
		{
			//Exit Game
		}

		GUI.EndGroup ();
	}

	void WinMenu(int ID)
	{
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.width/3), Resource.Win, ScaleMode.ScaleToFit);

		GUI.BeginGroup (new Rect ((Screen.width / 2) - (MenuWidth / 2), Screen.height - MenuHeight, MenuWidth, MenuHeight));

		if(GUI.Button(new Rect(0,0,MenuWidth,Buffer),Resource.NG_Btn))
		{
			SettingsWindowOpen = true;
			WinWindowOpen = false;
			Debug.Log ("Settings :" + SettingsWindowOpen + "winWindow" + WinWindowOpen + "start " + StartWindowOpen);
		}
		if(GUI.Button(new Rect(0,Buffer,MenuWidth,Buffer),Resource.QuitGame_Btn))
		{
			//Exit Game
		}

		GUI.EndGroup ();
	}
}

