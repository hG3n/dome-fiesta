using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


    public int SoundVolume = 100;
    public int MusicVolume = 100;
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
        UIManager.UI_Sound += PlaySound;
        UIManager.UI_Music += PlayMusic;
        UIManager.UI_Sound_Setting += SetSound;
        UIManager.UI_Music_Setting += SetMusic;
    }

    private void OnDisable()
    {
        Ball.Score_Sound -= PlaySound;
        player.Jump_Sound -= PlaySound;
        UIManager.UI_Sound -= PlaySound;
        UIManager.UI_Music -= PlayMusic;
        UIManager.UI_Sound_Setting -= SetSound;
        UIManager.UI_Music_Setting -= SetMusic;
    }

    // Use this for initialization
    void Start ()
    {
        MusicSource = GetComponent<AudioSource>();
        PlayMusic("title");       
	}

    void SetSound(string value)
    {
        SoundVolume = int.Parse(value)/100;
    }

    void SetMusic(string value)
    {
        MusicVolume = int.Parse(value)/100;
    }

    void PlaySound(AudioSource source,string value)
    {
        if (value == "jump")
        {
            source.PlayOneShot(SoundJump,SoundVolume);
        }
        else if (value=="score")
        {
            source.PlayOneShot(SoundScore, SoundVolume);
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
        if (value == "title")
        {
            MusicSource.PlayOneShot(Music[0], MusicVolume);
        }
        else if (value == "world1")
        {
            MusicSource.PlayOneShot(Music[1], MusicVolume);
        }
        else if (value == "world2")
        {
            MusicSource.PlayOneShot(Music[2], MusicVolume);
        }
    }

    void StopMusic()
    {
        MusicSource.Stop();
    }
}
