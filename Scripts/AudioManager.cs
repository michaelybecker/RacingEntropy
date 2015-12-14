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
		mainTheme.Play ();

		loseTheme = gameObject.AddComponent<AudioSource> ();
		loseTheme.clip = Resource.loseTheme;
		loseTheme.loop = true;

		winTheme = gameObject.AddComponent<AudioSource> ();
		winTheme.clip = Resource.winTheme;
		winTheme.loop = true;
	}

	void Update()
	{
		if (Global.lose)
		{
			Debug.Log("Playing sad music");
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
			Debug.Log("Playing happy music");
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
			Debug.Log("Reset music");
			if(!Global.playingTheme)
			{
				loseTheme.Stop ();
				winTheme.Stop ();
				mainTheme.Play ();
				Global.playingTheme = true;
			}
		}
	}

	public void Play(AudioClip audio)
	{
		player.PlayOneShot (audio,0.5f);
	}
}

