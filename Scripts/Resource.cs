using UnityEngine;
using System.Collections;

public static class Resource
{
	/********************************************************************************
	 * MESHES
	 * ******************************************************************************/
	public static Mesh plantMesh = (Mesh)Resources.Load(("Tiles/Plant"), typeof( Mesh ));

	//Disaster meshes
	public static Mesh fireMesh = (Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh ));
	public static Mesh stormMesh = (Mesh)Resources.Load(("Tiles/stormClouds"), typeof( Mesh ));
	//{STORM,VOLCANO,FLOOD,EARTHQUAKE};
	public static Mesh[] disasterMesh = new Mesh[]
	{
		(Mesh)Resources.Load(("Tiles/stormClouds"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Volcano"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Earthquake"), typeof( Mesh )),
	};

	public static Mesh baseMesh = (Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh ));

	public static Mesh[] tileMesh = new Mesh[]
	{
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Marsh"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Forest"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Water"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Mountain"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Grass"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Crags"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Goal"), typeof( Mesh ))
	};

	/*************************************************************************************
	 * MATERIALS FOR MESHES
	 * ************************************************************************************/
	public static Material plantMaterial = (Material)Resources.Load (("Tiles/Materials/Plant"), typeof(Material));
	public static Material fireMaterial = (Material)Resources.Load (("Tiles/Materials/Fire"), typeof(Material));
	public static Material stormMaterial = (Material)Resources.Load (("Tiles/Materials/Storm"), typeof(Material));
	public static Material[] disasterMaterial = new Material[]
	{
		(Material)Resources.Load (("Tiles/Materials/Thunder"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Volcano"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Flood"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Quake"), typeof(Material)),
	};

	public static Material[] tileMaterial = new Material[]{
		(Material)Resources.Load (("Tiles/Materials/Desert"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Marsh"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Forest"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Water"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Mountain"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Plain"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Crags"), typeof(Material)),
		(Material)Resources.Load (("Tiles/Materials/Goal"), typeof(Material))
	};

	/******************************************************************
	 * PARTICLE EFFECTS
	 * ****************************************************************/
	public static GameObject fireParticle = ((GameObject)Resources.Load (("Particles/Fire"), typeof(GameObject)));
	public static GameObject[] disasterParticles = new GameObject[]
	{
		(GameObject)Resources.Load (("Particles/Cloud"), typeof(GameObject)),
		(GameObject)Resources.Load (("Particles/VolcanoSmoke"), typeof(GameObject)),
		(GameObject)Resources.Load (("Particles/Fire"), typeof(GameObject)),
		(GameObject)Resources.Load (("Particles/Fire"), typeof(GameObject)),
	};

	/******************************************************************
	 * UV MAPS
	 * ******************************************************************/

	/**********************************************************
	 * SOUND OBJECTS
	 * ******************************************************/
	public static AudioClip mainTheme = (AudioClip)Resources.Load ("Sound/Music/Main_Theme/Main", typeof(AudioClip));
	public static AudioClip loseTheme = (AudioClip)Resources.Load ("Sound/Music/GameOver_Theme/GameOverRev2", typeof(AudioClip));
	public static AudioClip winTheme = (AudioClip)Resources.Load ("Sound/Music/Winning_Theme/WinMusic", typeof(AudioClip));

	public static AudioClip Click = (AudioClip)Resources.Load ("Sound/FX/gameplay/Click", typeof(AudioClip));
	public static AudioClip startButton = (AudioClip)Resources.Load ("Sound/FX/gameplay/StartButton", typeof(AudioClip));
	public static AudioClip wannaQuit = (AudioClip)Resources.Load ("Sound/FX/gameplay/WannaQuit", typeof(AudioClip));

	public static AudioClip[] elementSound = new AudioClip[]
	{
		(AudioClip)Resources.Load ("Sound/FX/Elements/earth", typeof(AudioClip)),
		(AudioClip)Resources.Load ("Sound/FX/Elements/air", typeof(AudioClip)),
		(AudioClip)Resources.Load ("Sound/FX/Elements/water", typeof(AudioClip)),
		(AudioClip)Resources.Load ("Sound/FX/Elements/fire", typeof(AudioClip)),
	};

	/****************************************************************************
	 * GUI OBJECTS
	 * **************************************************************************/
	public static Texture2D Air_Btn = (Texture2D)Resources.Load (("Gui/Air_Btn"), typeof(Texture2D));
	public static Texture2D Earth_Btn = (Texture2D)Resources.Load (("Gui/Earth_Btn"), typeof(Texture2D));
	public static Texture2D Fire_Btn = (Texture2D)Resources.Load (("Gui/Fire_Btn"), typeof(Texture2D));
	public static Texture2D Water_Btn = (Texture2D)Resources.Load (("Gui/Water_Btn"), typeof(Texture2D));
	public static Texture2D Menu_Btn = (Texture2D)Resources.Load (("Gui/Menu_Btn"), typeof(Texture2D));
	
	public static Texture2D Air_Cursor = (Texture2D)Resources.Load (("Gui/Air_Cursor"), typeof(Texture2D));
	public static Texture2D Earth_Cursor = (Texture2D)Resources.Load (("Gui/Earth_Cursor"), typeof(Texture2D));
	public static Texture2D Fire_Cursor = (Texture2D)Resources.Load (("Gui/Fire_Cursor"), typeof(Texture2D));
	public static Texture2D Water_Cursor = (Texture2D)Resources.Load (("Gui/Water_Cursor"), typeof(Texture2D));
	public static Texture2D Menu_Cursor = (Texture2D)Resources.Load (("Gui/Blk_Cursor"), typeof(Texture2D));
	
	public static Texture2D NG_Btn = (Texture2D)Resources.Load (("Gui/NG_Btn"), typeof(Texture2D));
	public static Texture2D ContinueGame_Btn = (Texture2D)Resources.Load (("Gui/ContinueGame_Btn"), typeof(Texture2D));
	public static Texture2D QuitGame_Btn = (Texture2D)Resources.Load (("Gui/QuitGame_Btn"), typeof(Texture2D));
	public static Texture2D RestartGame_Btn = (Texture2D)Resources.Load (("Gui/RestartGame_Btn"), typeof(Texture2D));
	public static Texture2D Yes_Btn = (Texture2D)Resources.Load (("Gui/Yes_Btn"), typeof(Texture2D));
	public static Texture2D No_Btn = (Texture2D)Resources.Load (("Gui/No_Btn"), typeof(Texture2D));
	
	public static Texture2D HardDifficulty_Btn = (Texture2D)Resources.Load (("Gui/HardDifficulty_Btn"), typeof(Texture2D));
	public static Texture2D MediumDifficulty_Btn = (Texture2D)Resources.Load (("Gui/MediumDifficulty_Btn"), typeof(Texture2D));
	public static Texture2D LowDifficulty_Btn = (Texture2D)Resources.Load (("Gui/LowDifficulty_Btn"), typeof(Texture2D));
	public static Texture2D Back_Btn = (Texture2D)Resources.Load (("Gui/Back_Btn"), typeof(Texture2D));
	
	public static Texture2D Title = (Texture2D)Resources.Load (("Gui/Title"), typeof(Texture2D));
	public static Texture2D TitleBackground = (Texture2D)Resources.Load (("Gui/TitleBackground"), typeof(Texture2D));
	public static Texture2D Lose = (Texture2D)Resources.Load (("Gui/GameOver2"), typeof(Texture2D));
	public static Texture2D Win = (Texture2D)Resources.Load (("Gui/GameWin"), typeof(Texture2D));
	public static Texture2D Exit = (Texture2D)Resources.Load (("Gui/AreYouSure"), typeof(Texture2D));

}