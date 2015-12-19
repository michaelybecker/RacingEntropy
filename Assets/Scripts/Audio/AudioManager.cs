using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	//Audio for random sound effects
	private AudioSource player;
	//ambiance playing in the background at all times
	private AudioSource ambiance;
	//Theme music for when particular actions happen
	private AudioSource mainTheme;
	private AudioSource loseTheme;
	private AudioSource winTheme;

	//On startup set up the audio objects
	void Awake()
	{
		player = gameObject.AddComponent<AudioSource> ();

		Global.playingTheme = true;
		ambiance = gameObject.AddComponent<AudioSource> ();
		ambiance.clip = Resource.ambience;
		ambiance.loop = true;
		ambiance.volume  = 0.025f;
		ambiance.Play ();

		mainTheme = gameObject.AddComponent<AudioSource> ();
		mainTheme.clip = Resource.mainTheme;
		mainTheme.loop = true;
		mainTheme.volume  = 0.5f;

		loseTheme = gameObject.AddComponent<AudioSource> ();
		loseTheme.clip = Resource.loseTheme;
		loseTheme.loop = true;
		loseTheme.volume  = 0.5f;

		winTheme = gameObject.AddComponent<AudioSource> ();
		winTheme.clip = Resource.winTheme;
		winTheme.loop = true;
		winTheme.volume  = 0.5f;
	}

	//Check to see the status of the game and if it needs to change themes
	void Update()
	{
		//Make sure that only one theme is playing at a time
		if (Global.lose)
		{
			if(Global.playingTheme)
			{
				mainTheme.Stop ();
				winTheme.Stop ();
				loseTheme.Play ();
				Global.playingTheme = false;
			}
		}
		else if (Global.win)
		{
			if(Global.playingTheme)
			{
				mainTheme.Stop ();
				loseTheme.Stop ();
				winTheme.Play ();
				Global.playingTheme = false;
			}
		}
		else
		{
			if(!Global.playingTheme)
			{
				loseTheme.Stop ();
				winTheme.Stop ();
				mainTheme.Play ();
				Global.playingTheme = true;
			}
		}
	}

	//Play a sound effect
	public void Play(AudioClip audio, float volume)
	{
		player.PlayOneShot (audio,volume);
	}
}

