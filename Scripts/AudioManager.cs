using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	public AudioSource player;
	public AudioSource mainTheme;
	public AudioSource loseTheme;

	void Awake()
	{
		player = gameObject.AddComponent<AudioSource> ();

		mainTheme = gameObject.AddComponent<AudioSource> ();
		mainTheme.clip = Resource.mainTheme;
		mainTheme.loop = true;
		mainTheme.Play ();

		loseTheme = gameObject.AddComponent<AudioSource> ();
		loseTheme.clip = Resource.loseTheme;
		loseTheme.loop = true;
	}

	void Update()
	{
		if (Global.lose)
		{
			if(Global.playingTheme)
			{
				mainTheme.Stop ();
				loseTheme.Play ();
				Global.playingTheme = false;
			}
		}
	}

	public void Play(AudioClip audio)
	{
		player.PlayOneShot (audio,0.5f);
	}
}

