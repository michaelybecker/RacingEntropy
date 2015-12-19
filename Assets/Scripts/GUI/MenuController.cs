using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour 
{
	
	public TileManager tiles;
	public GUISkin style;
	public AudioManager sound;
	int ButtonWidth = 150;
	int ButtonHeight = 40;
	int MenuWidth;
	int MenuHeight;
	int chosenLevel;
	public bool StartWindowOpen = true;
	//public bool SettingsWindowOpen = false;
	public bool LoseWindowOpen = false;
	public bool WinWindowOpen = false;
	public bool ExitWindowOpen = false;

	void OnGUI()
	{
		MenuWidth = Screen.width / 3;
		MenuHeight = Screen.height / 3;

		GUI.skin = style;
		GUI.backgroundColor = new Color (0f, 0f, 0f, 2f);

		if (StartWindowOpen) 
		{
			Rect window = new Rect (0, 0, Screen.width, Screen.height);
			window = GUI.ModalWindow (0, window, StartMenu, "");
			Global.pause = true;
		}
		/*if (SettingsWindowOpen) 
		{
			Rect window = new Rect (0, 0, Screen.width, Screen.height);
			window = GUI.ModalWindow (0, window, SettingsMenu, "");
			Global.pause = true;
		}*/
		if(ExitWindowOpen)
		{
			Rect window = new Rect (0, 0, Screen.width, Screen.height);
			window = GUI.ModalWindow (0, window, ExitMenu, "");
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
		Cursor.SetCursor(Resource.Menu_Cursor,new Vector2 (0.5f,0),CursorMode.Auto);

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.width/3), Resource.Title, ScaleMode.ScaleToFit);

		MenuWidth = ButtonWidth * 3;
		GUI.BeginGroup (new Rect ((Screen.width / 2) - (MenuWidth / 2), Screen.height - MenuHeight, MenuWidth, MenuHeight));

		//Low difficulty
		if(GUI.Button(new Rect(0,0,ButtonWidth,ButtonHeight),Resource.LowDifficulty_Btn))
		{
			//SettingsWindowOpen = true;
			//tiles.NewRandomLevel (1);
			tiles.TutorialLevel(Global.tutorialProgress);
			chosenLevel = 0;
			StartWindowOpen = false;
			Global.pause = false;
			sound.Play (Resource.startButton, 1f);
		}
		//Medium difficulty
		if(GUI.Button(new Rect(ButtonWidth,0,ButtonWidth,ButtonHeight),Resource.MediumDifficulty_Btn))
		{
			tiles.NewRandomLevel (2);
			chosenLevel = 2;
			StartWindowOpen = false;
			Global.pause = false;
			sound.Play (Resource.startButton, 1f);
		}
		//Hard difficulty
		if(GUI.Button(new Rect(ButtonWidth*2,0,ButtonWidth,ButtonHeight),Resource.HardDifficulty_Btn))
		{
			tiles.NewRandomLevel (3);
			chosenLevel = 3;
			StartWindowOpen = false;
			Global.pause = false;
			sound.Play (Resource.startButton, 1f);
		}
		//Continue
		if(GUI.Button(new Rect(ButtonWidth,ButtonHeight,ButtonWidth, ButtonHeight),Resource.ContinueGame_Btn))
		{
			sound.Play (Resource.startButton, 1f);
			StartWindowOpen = false;
			Global.pause = false;
		}
		//Quit
		if(GUI.Button(new Rect(ButtonWidth,ButtonHeight*2, ButtonWidth, ButtonHeight),Resource.QuitGame_Btn))
		{
			sound.Play (Resource.wannaQuit, 0.5f);
			ExitWindowOpen = true;
			StartWindowOpen = false;
		}

		GUI.EndGroup();
	}

	/*void SettingsMenu(int ID)
	{
		Buffer = MenuHeight / 4;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.width/3), Resource.Title, ScaleMode.ScaleToFit);

		GUI.BeginGroup (new Rect ((Screen.width / 2) - (MenuWidth / 2), Screen.height - MenuHeight, MenuWidth, MenuHeight));

		if (GUI.Button (new Rect (0, 0, MenuWidth, Buffer), Resource.LowDifficulty_Btn)) {
			//send "1" to create map function
			tiles.NewLevel (1);
			Global.pause = false;
			SettingsWindowOpen = false;
			sound.Play (Resource.startButton, 1f);
		}
		if (GUI.Button (new Rect (0, Buffer, MenuWidth, Buffer), Resource.MediumDifficulty_Btn)) {
			//send "2" to create map function
			tiles.NewLevel (2);
			Global.pause = false;
			SettingsWindowOpen = false;
			sound.Play (Resource.startButton, 1f);
		}
		if (GUI.Button (new Rect (0, Buffer * 2, MenuWidth, Buffer), Resource.HardDifficulty_Btn)) {
			//send "3" to create map function
			tiles.NewLevel (3);
			Global.pause = false;
			SettingsWindowOpen = false;
			sound.Play (Resource.startButton, 1f);
		}
		if (GUI.Button (new Rect (0, Buffer * 3, MenuWidth, Buffer), Resource.Back_Btn)) {
			//back to the previous menu
			StartWindowOpen = true;
			Global.pause = false;
			SettingsWindowOpen = false;
			sound.Play (Resource.startButton, 1f);
		}

		GUI.EndGroup ();
	}*/

	void LoseMenu(int ID)
	{
		//Debug.Log (Global.lose + "" + Global.pause);
		MenuWidth = ButtonWidth;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.width/3), Resource.Lose, ScaleMode.ScaleToFit);

		GUI.BeginGroup (new Rect ((Screen.width / 2) - (MenuWidth / 2), Screen.height - MenuHeight, MenuWidth, MenuHeight));

		if(GUI.Button(new Rect(0,0,MenuWidth,ButtonHeight),Resource.NG_Btn))
		{
			//SettingsWindowOpen = true;
			LoseWindowOpen = false;
			StartWindowOpen = true;
			Global.pause = false;
			Global.lose = false;
			Global.win = false;
		}
		if(GUI.Button(new Rect(0,ButtonHeight,MenuWidth,ButtonHeight),Resource.QuitGame_Btn))
		{
			sound.Play (Resource.wannaQuit, 0.5f);
			ExitWindowOpen = true;
			LoseWindowOpen = false;
		}

		GUI.EndGroup ();
	}

	void WinMenu(int ID)
	{
		MenuWidth = ButtonWidth;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.width/3), Resource.Win, ScaleMode.ScaleToFit);

		GUI.BeginGroup (new Rect ((Screen.width / 2) - (MenuWidth / 2), Screen.height - MenuHeight, MenuWidth, MenuHeight));

		if(GUI.Button(new Rect(0,0,MenuWidth,ButtonHeight),Resource.NG_Btn))
		{
			tiles.NewRandomLevel(chosenLevel);
			WinWindowOpen = false;
			Global.pause = false;
			Global.win = false;
			Global.lose = false;
		}
		if(GUI.Button(new Rect(0,ButtonHeight,MenuWidth,ButtonHeight),Resource.QuitGame_Btn))
		{
			sound.Play (Resource.wannaQuit, 0.5f);
			ExitWindowOpen = true;
			WinWindowOpen = false;
		}

		GUI.EndGroup ();
	}

	void ExitMenu(int ID)
	{
		MenuWidth = ButtonWidth;

		GUI.DrawTexture (new Rect (0, 35, Screen.width, Screen.width/3), Resource.Exit, ScaleMode.ScaleToFit);

		GUI.BeginGroup (new Rect ((Screen.width / 2) - (MenuWidth / 2), Screen.height - MenuHeight, MenuWidth, MenuHeight));

		if (GUI.Button (new Rect (0, 0, MenuWidth, ButtonHeight), Resource.Yes_Btn)) 
		{
			Application.Quit ();
		}
		if (GUI.Button (new Rect (0, ButtonHeight, MenuWidth, ButtonHeight), Resource.No_Btn)) 
		{
			ExitWindowOpen = false;
			StartWindowOpen = true;
		}

		GUI.EndGroup ();
	}
}