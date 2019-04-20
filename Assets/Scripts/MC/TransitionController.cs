﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour {

    private bool dark = false;
    protected Image ZoneTransition;
    protected Text zoneText;
    public string currentZone;
    public string newZone;

    public bool elevatorTransfer;

    private JukeBoxController jukebox;
    private Coroutine co;

    void Start()
    {
        elevatorTransfer = false;
        ZoneTransition = this.GetComponentInChildren<Image>();
        zoneText = this.GetComponentInChildren<Text>();
        //Debug.Log(ZoneTransition);
        currentZone = GameControl.control.zone;
        newZone = currentZone;
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
    }
    
    void Update()
    {
        if(dark == true)
        {
            StartCoroutine(transitionIn());
        }
        if(newZone != currentZone && currentZone != "")
        {
            currentZone = newZone;
            if(co != null)
            {
                StopCoroutine(co);
            }
            jukebox.StopSong();
            co = StartCoroutine(displayZone());
        }
        else
        {
            currentZone = newZone;
        }
    }

    public IEnumerator displayZone()
    {
        
        switch (newZone)
        {
            case "SE":
                zoneText.text = "Silent Entryhall";
                break;
            case "DP":
                zoneText.text = "Dead Performance";
                break;
            case "FM":
                zoneText.text = "Forgotten Mezzanine";
                break;
            case "FK":
                zoneText.text = "Festered Kiln";
                break;
            case "NB":
                zoneText.text = "Natural Banquet";
                break;
            case "FL":
                zoneText.text = "Frigid Loft";
                break;
            case "CD":
                zoneText.text = "Cavernous Decline";
                break;
            case "DS":
                zoneText.text = "Departure Sandbox";
                break;
            case "DH":
                zoneText.text = "Desolate Heart";
                break;
        }

        //Debug.Log(newZone);
        for (float i = 0f; i < 255f; i += 4.25f)
        {
            zoneText.color = new Color(255f, 255f, 255f, i / 255f);
            yield return new WaitForSeconds(0.025f);
        }
        jukebox.ResumeOverworldSong();
        zoneText.color = new Color(255f, 255f, 255f, 255f);
        yield return new WaitForSeconds(3f);
        for (float i = 255f; i > 0f; i -= 8.5f) 
        {
            zoneText.color = new Color(255f, 255f, 255f, i / 255f);
            yield return new WaitForSeconds(0.005f);
        }
        zoneText.color = new Color(255f, 255f, 255f, 0f);
        yield return null;
    }

    public IEnumerator transitionOut()
    {
        for(float i = 0f; i < 255f; i += 17f)
        {
            ZoneTransition.color = new Color( 0f, 0f, 0f, i/255f);
            yield return new WaitForSeconds(0.005f);
        }
        ZoneTransition.color = new Color(0f, 0f, 0f, 1);
        dark = true;
    }

    public IEnumerator transitionIn()
    {
        if (elevatorTransfer)
        {
            GameObject.Find("player(Clone)").transform.position = GameObject.Find("ElevatorDataTransfer").transform.position;
            elevatorTransfer = false;
        }
        GameControl.control.Unfreeze();
        dark = false;
        yield return new WaitForSeconds(0.05f);
        for (float i = 255f; i > 0f; i -= 17f)
        {
            ZoneTransition.color = new Color(0f, 0f, 0f, i/255f);
            yield return new WaitForSeconds(0.005f);
        }
        ZoneTransition.color = new Color(0f, 0f, 0f, 0f);
        
    }

    
}
