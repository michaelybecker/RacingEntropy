using UnityEngine;
using System.Collections;

public static class Resource
{
	public static Mesh plantMesh = (Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh ));
	public static Mesh fireMesh = (Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh ));

	public static Mesh baseMesh = (Mesh)Resources.Load(("Tiles/Base"), typeof( Mesh ));

	//Meshes for the different Tiles
	public static Mesh[] tileMesh = new Mesh[]{
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Forest"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Water"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Mountain"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Desert"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Crags"), typeof( Mesh )),
		(Mesh)Resources.Load(("Tiles/Goal"), typeof( Mesh ))
	};

	//GUI buttons
	public static Texture2D Air_Btn = (Texture2D)Resources.Load (("Gui/Air_Btn"), typeof(Texture2D));
	public static Texture2D Earth_Btn = (Texture2D)Resources.Load (("Gui/Earth_Btn"), typeof(Texture2D));
	public static Texture2D Fire_Btn = (Texture2D)Resources.Load (("Gui/Fire_Btn"), typeof(Texture2D));
	public static Texture2D Water_Btn = (Texture2D)Resources.Load (("Gui/Water_Btn"), typeof(Texture2D));
	public static Texture2D Menu_Btn = (Texture2D)Resources.Load (("Gui/Menu_Btn"), typeof(Texture2D));

	public static Texture2D NG_Btn = (Texture2D)Resources.Load (("Gui/NG_Btn"), typeof(Texture2D));
	public static Texture2D ContinueGame_Btn = (Texture2D)Resources.Load (("Gui/ContinueGame_Btn"), typeof(Texture2D));
	public static Texture2D QuitGame_Btn = (Texture2D)Resources.Load (("Gui/QuitGame_Btn"), typeof(Texture2D));
	public static Texture2D RestartGame_Btn = (Texture2D)Resources.Load (("Gui/RestartGame_Btn"), typeof(Texture2D));

	public static Texture2D HardDifficulty_Btn = (Texture2D)Resources.Load (("Gui/HardDifficulty_Btn"), typeof(Texture2D));
	public static Texture2D MediumDifficulty_Btn = (Texture2D)Resources.Load (("Gui/MediumDifficulty_Btn"), typeof(Texture2D));
	public static Texture2D LowDifficulty_Btn = (Texture2D)Resources.Load (("Gui/LowDifficulty_Btn"), typeof(Texture2D));
	public static Texture2D Back_Btn = (Texture2D)Resources.Load (("Gui/Back_Btn"), typeof(Texture2D));

	public static Texture2D Title = (Texture2D)Resources.Load (("Gui/Title"), typeof(Texture2D));
	public static Texture2D TitleBackground = (Texture2D)Resources.Load (("Gui/TitleBackground"), typeof(Texture2D));
	public static Texture2D Lose = (Texture2D)Resources.Load (("Gui/Game Over 2"), typeof(Texture2D));
	public static Texture2D Win = (Texture2D)Resources.Load (("Gui/Game Win"), typeof(Texture2D));

	//Materials
	public static Material plantMaterial = (Material)Resources.Load (("Tiles/Materials/Plant"), typeof(Material));
	public static Material fireMaterial = (Material)Resources.Load (("Tiles/Materials/Fire"), typeof(Material));

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

	//Music 
	public static AudioClip mainTheme = (AudioClip)Resources.Load ("Sound/Music/Main_Theme/Main", typeof(AudioClip));
	public static AudioClip loseTheme = (AudioClip)Resources.Load ("Sound/Music/GameOver_Theme/GameOverRev2", typeof(AudioClip));

	public static AudioClip[] elementSound = new AudioClip[]
	{
		(AudioClip)Resources.Load ("Sound/FX/elements/earth", typeof(AudioClip)),
		(AudioClip)Resources.Load ("Sound/FX/elements/air", typeof(AudioClip)),
		(AudioClip)Resources.Load ("Sound/FX/elements/water", typeof(AudioClip)),
		(AudioClip)Resources.Load ("Sound/FX/elements/fire", typeof(AudioClip)),
	};
}

