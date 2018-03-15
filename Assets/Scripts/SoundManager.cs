using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


	public float SoundVolume = 100;
    public float MusicVolume = 100;
    public AudioClip SoundJump;
    public AudioClip SoundScore;
    public AudioClip SoundSelect;
    public AudioClip SoundPick;
    public AudioClip SoundWin;

    public List<AudioClip> Music;
    public AudioSource MusicSource;

    private void OnEnable()
    {
        Ball.Score_Sound += PlaySound;
        player.Jump_Sound += PlaySound;
    }

    private void OnDisable()
    {
        Ball.Score_Sound -= PlaySound;
        player.Jump_Sound -= PlaySound;
    }

    // Use this for initialization
    void Start ()
    {
        MusicSource = GetComponent<AudioSource>();
		SetMusic("100");
		SetSound("100");
        PlayMusic("title");       
	}

    void SetSound(string value)
    {
		SoundVolume = float.Parse(value)/1000;
    }

    void SetMusic(string value)
    {
        MusicVolume = float.Parse(value)/1000;
    }

    void PlaySound(AudioSource source,string value)
    {
		Debug.Log ("Play Sound" + value);
        if (value == "jump")
        {
            source.PlayOneShot(SoundJump,SoundVolume);
        }
        else if (value=="score")
        {
            MusicSource.PlayOneShot(SoundScore, SoundVolume);
        }
        else if (value == "pick")
        {
            source.PlayOneShot(SoundPick, SoundVolume);
        }
        else if (value == "select")
        {
            source.PlayOneShot(SoundSelect, SoundVolume);
        }
        else if (value == "win")
        {
            source.PlayOneShot(SoundWin, SoundVolume);
        }

    }

    void PlayMusic(string value)
    {
		Debug.Log("Play Music" + value);
		MusicSource.Stop ();
		MusicSource.volume = MusicVolume;
		if (value == "title")
		{
			MusicSource.clip = Music[0];
            MusicSource.Play();
        }
        else if (value == "world1")
        {
			MusicSource.clip = Music[1];
            MusicSource.Play();
        }
        else if (value == "world2")
        {
			MusicSource.clip = Music[2];
            MusicSource.Play();
        }
    }

    void StopMusic()
    {
        MusicSource.Stop();
    }
}
