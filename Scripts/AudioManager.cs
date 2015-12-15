using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	private AudioSource player;
	private AudioSource ambiance;
	private AudioSource mainTheme;
	private AudioSource loseTheme;
	private AudioSource winTheme;

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

