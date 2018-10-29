using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBoxController : MonoBehaviour {

    private AudioSource AS;

    public bool PauseOverworld;
    public AudioClip[] Overworld = new AudioClip[9];

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

    }

    public void StopSong()
    {
        AS.Stop();
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
