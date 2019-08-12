using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBoxController : MonoBehaviour {

    private AudioSource AS;

    public bool PauseOverworld;
    public AudioClip[] Overworld = new AudioClip[9];
    public AudioClip[] AllSongs = new AudioClip[10];

	// Use this for initialization
	void Start () {
        AS = GetComponent<AudioSource>();
        ResumeOverworldSong();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /**********
    * Name: PlaySong
    * Function: Plays the song that corresponds to the string it's given
    **********/

    public void PlaySong(string songName)
    {
        switch (songName)
        {
            case "SE":
                AS.clip = Overworld[0];
                break;
            case "DP":
                AS.clip = Overworld[1];
                break;
            case "FM":
                AS.clip = Overworld[2];
                break;
            case "FK":
                AS.clip = Overworld[3];
                break;
            case "NB":
                AS.clip = Overworld[4];
                break;
            case "FL":
                AS.clip = Overworld[5];
                break;
            case "CD":
                AS.clip = Overworld[6];
                break;
            case "DS":
                AS.clip = Overworld[7];
                break;
            case "DH":
                AS.clip = Overworld[8];
                break;
            case "DPWeird":
                AS.clip = AllSongs[1];
                break;
            case "WillsTheme":
                AS.clip = AllSongs[2];
                break;
            default:
                AS.clip = AllSongs[0];
                break;
        }
        AS.Play();
    }

    public void ReplaceSongPartway(string songName)
    {
        float tempTime = AS.time;
        PlaySong(songName);
        AS.time = tempTime;
    }

    public void PlaySongPartway(string songName, float time)
    {
        PlaySong(songName);
        AS.time = time;
    }

    public void ResumeSongBeginning()
    {
        AS.Play();
    }

    public void PauseSongPartway()
    {
        AS.Pause();
    }

    public float CurrentTime()
    {
        return AS.time;
    }

    public void ResumeSongPartway()
    {
        AS.UnPause();
    }

    public void StopSong()
    {
        AS.Stop();
    }

    public bool IsPlaying()
    {
        return AS.isPlaying;
    }

    public void SetVolume(float vol)
    {
        AS.volume = vol;
    }

    public IEnumerator FadeIn(float vol)
    {
        for(float i = 0.1f; i < vol; i += 0.05f)
        {
            AS.volume = i;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator FadeOut(float vol)
    {
        for (float i = vol; i > 0; i -= 0.05f)
        {
            AS.volume = i;
            yield return new WaitForSeconds(0.1f);
        }
        AS.Stop();
    }

    public IEnumerator PauseOut(float vol)
    {
        for (float i = vol; i > 0; i -= 0.05f)
        {
            AS.volume = i;
            yield return new WaitForSeconds(0.1f);
        }
        AS.Pause();
    }

    /***********
    * Name: ResumeOverworldSong
    * Function: Stops the currently playing song and resumes the overworld song
    ***********/

    public void ResumeOverworldSong()
    {
        switch (GameControl.control.zone)
        {
            case "SE":
                AS.clip = Overworld[0];
                break;
            case "DP":
                AS.clip = Overworld[1];
                break;
            case "FM":
                AS.clip = Overworld[2];
                break;
            case "FK":
                AS.clip = Overworld[3];
                break;
            case "NB":
                AS.clip = Overworld[4];
                break;
            case "FL":
                AS.clip = Overworld[5];
                break;
            case "CD":
                AS.clip = Overworld[6];
                break;
            case "DS":
                AS.clip = Overworld[7];
                break;
            case "DH":
                AS.clip = Overworld[8];
                break;
        }

        AS.Play();
    }
}
