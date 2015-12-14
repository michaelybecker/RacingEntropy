using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public AudioSource player;
	public AudioSource mainTheme;
	public AudioSource loseTheme;
	public AudioSource winTheme;

	void Awake()
	{
		player = gameObject.AddComponent<AudioSource> ();

		Global.playingTheme = true;
		mainTheme = gameObject.AddComponent<AudioSource> ();
		mainTheme.clip = Resource.mainTheme;
		mainTheme.loop = true;
		mainTheme.volume  = 0.5f;
		mainTheme.Play ();

		loseTheme = gameObject.AddComponent<AudioSource> ();
		loseTheme.clip = Resource.loseTheme;
		loseTheme.loop = true;
		loseTheme.volume  = 0.5f;

		winTheme = gameObject.AddComponent<AudioSource> ();
		winTheme.clip = Resource.winTheme;
		winTheme.loop = true;
		winTheme.volume  = 0.5f;
	}

	void Update()
	{
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

	public void Play(AudioClip audio, float volume)
	{
		player.PlayOneShot (audio,volume);
	}
}

