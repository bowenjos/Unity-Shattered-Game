﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour {

    public GameObject LensPanel;
    public GameObject LensSelector;
    public GameObject TimerPanel;
    public GameObject LensBar;

    RectTransform timerBarRT;

    RectTransform lensSelectorRT;
    RectTransform[] lensicons;
    Image[] lensIconsImage;

    private Coroutine co;

	// Use this for initialization
	void Start () {
        lensicons = LensPanel.GetComponentsInChildren<RectTransform>();
        lensIconsImage = LensPanel.GetComponentsInChildren<Image>();
        lensSelectorRT = LensSelector.GetComponentInChildren<RectTransform>();
        timerBarRT = LensBar.GetComponent<RectTransform>();

        LensPanel.SetActive(false);
        TimerPanel.SetActive(false);

        for(int i = 0; i < 7; i++)
        {
            lensIconsImage[i+2].color = new Color(1f, 1f, 1f, 0f);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!(GameControl.control.encounter || GameControl.control.frozen || GameControl.control.paused))
        {


            if (Input.GetKeyDown(KeyCode.Alpha1) && GameControl.control.lens[0])
            {
                setLensSelector(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && GameControl.control.lens[1])
            {
                setLensSelector(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && GameControl.control.lens[2])
            {
                setLensSelector(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && GameControl.control.lens[3])
            {
                setLensSelector(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) && GameControl.control.lens[4])
            {
                setLensSelector(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) && GameControl.control.lens[5])
            {
                setLensSelector(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) && GameControl.control.lens[6])
            {
                setLensSelector(6);
            }
        }
    }

    public IEnumerator lensDisplayOn(float time)
    {
        LensPanel.SetActive(true);
        yield return new WaitForSeconds(time);
        LensPanel.SetActive(false);
        yield return null;
    }

    public void setLensSelector(int lens)
    {
        setDisplayLens();
        GameControl.control.curLens = lens;
        if(co != null)
        {
            StopCoroutine(co);
        }
        co = StartCoroutine(lensDisplayOn(1f));
        //Set anchors of the lens selector to the anchors of the selected lens 
        //(+3 because lenselector and lensPanel occupy 0 and 1 so everything is offset by 2)
        lensSelectorRT.anchorMax = lensicons[lens+2].anchorMax;
        lensSelectorRT.anchorMin = lensicons[lens+2].anchorMin;
    }

    public void setDisplayLens()
    {
        for(int i = 0; i < 7; i++)
        {
            if (GameControl.control.lens[i])
            {
                lensIconsImage[i+2].color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public IEnumerator timerDisplayOn(float time)
    {
        TimerPanel.SetActive(true);
        yield return StartCoroutine(increaseBar(time));
        TimerPanel.SetActive(false);
    }

    IEnumerator increaseBar(float time)
    {
        float dx = 0;
        while(dx < 0.986f)
        {
            dx += ((.1f / time)* 0.986f);
            timerBarRT.anchorMax = new Vector2(dx, timerBarRT.anchorMax.y);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
