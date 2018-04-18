using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour {

    public Texture normal;

    public Texture laser0;
    public Texture laser1;
    public Texture laser2;
    public Texture laser3;

    public Texture cone0;
    public Texture cone1;
    public Texture cone2;
    public Texture cone3;

    public GameObject Neutral;
    public GameObject Pink;
    public GameObject Green;
    public GameObject Orange;
    public GameObject Yellow;
    public GameObject Purple;

    public GameObject Current;

    OverlayController OverlayC;

    Light thisLight;
    GameObject Player;
    PlayerController PlayerC;

    bool lensSet;
    bool flickering;

	// Use this for initialization
	void Start () {
        OverlayC = GameObject.Find("Overlay(Clone)").GetComponent<OverlayController>();
        thisLight = this.GetComponent<Light>();
        Player = GameObject.Find("player(Clone)");
        PlayerC = Player.GetComponent<PlayerController>();
        lensSet = false;
        GameControl.control.curLens = 8;
        Current = Instantiate(Neutral);
        Current.transform.parent = Player.transform;
    }
	
	// Update is called once per frame
	void Update () {

        thisLight.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -2);

        if (Input.GetKeyDown(KeyCode.X) && lensSet == false && !GameControl.control.frozen)
        {
            StartCoroutine(setTheLens(GameControl.control.curLens));
        }

        if (lensSet == false)
        {
            setNormalLens();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (lensSet)
        {
            switch (GameControl.control.curLens)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }
        }
    }

    IEnumerator setTheLens(int curLens)
    {
        Destroy(Current);
        switch(curLens)
        {
            case 0:
                yield return StartCoroutine(setBlueLens());
                break;
            case 1:
                yield return StartCoroutine(setPinkLens());
                break;
            case 2:
                yield return StartCoroutine(setOrangeLens());
                break;
            case 3:
                yield return StartCoroutine(setGreenLens());
                break;
            case 4:
                yield return StartCoroutine(setRedLens());
                break;
            case 5:
                yield return StartCoroutine(setYellowLens());
                break;
            case 6:
                yield return StartCoroutine(setPurpleLens());
                break;  
        }
        lensSet = false;
        GameControl.control.Unfreeze();
        Current = Instantiate(Neutral);
        Current.transform.parent = Player.transform;
        yield return null;
    }

    void setNormalLens()
    {
        setBrightness();
        thisLight.color = new Color(1f, 1f, 1f, 1f);
        //thisLight.intensity = 0.7f;
    }

    IEnumerator setBlueLens()
    {
        PlayerC.Push();
        lensSet = true;
        StopCoroutine("flicker");
        flickering = false;
        GameControl.control.Unfreeze();
        thisLight.cookieSize = 1.2f;
        thisLight.color = new Color(0f, 0.25f, 1f, 1f);
        thisLight.intensity = 3f;
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(WaitForKeyDown(KeyCode.X));
        PlayerC.Unpush();
    }

    IEnumerator setPinkLens()
    {
        Current = Instantiate(Pink);
        StartCoroutine(OverlayC.timerDisplayOn(1f));
        Current.transform.parent = Player.transform;
        lensSet = true;
        StopCoroutine("flicker");
        flickering = false;
        GameControl.control.Freeze();
        thisLight.cookieSize = 6f;
        switch (PlayerC.direction)
        {
            case 0:
                thisLight.cookie = cone0;
                break;
            case 1:
                thisLight.cookie = cone1;
                break;
            case 2:
                thisLight.cookie = cone2;
                break;
            case 3:
                thisLight.cookie = cone3;
                break;
        }
        thisLight.color = new Color(1f, 0.5f, 1f, 1f);
        thisLight.intensity = 1f;
        yield return new WaitForSeconds(1f);
        Destroy(Current);
        thisLight.cookie = normal;
    }

    IEnumerator setOrangeLens()
    {
        Current = Instantiate(Orange);
        StartCoroutine(OverlayC.timerDisplayOn(1f));
        Current.transform.parent = Player.transform;
        lensSet = true;
        StopCoroutine("flicker");
        flickering = false;
        GameControl.control.Freeze();
        thisLight.cookieSize = 6f;
        switch (PlayerC.direction)
        {
            case 0:
                thisLight.cookie = cone0;
                break;
            case 1:
                thisLight.cookie = cone1;
                break;
            case 2:
                thisLight.cookie = cone2;
                break;
            case 3:
                thisLight.cookie = cone3;
                break;
        }
        thisLight.color = new Color(1f, 0.5f, 0f, 1f);
        thisLight.intensity = 3f;
        yield return new WaitForSeconds(1f);
        thisLight.cookie = normal;
        Destroy(Current);
    }

    IEnumerator setGreenLens()
    {
        Current = Instantiate(Green);
        StartCoroutine(OverlayC.timerDisplayOn(1f));
        Current.transform.parent = Player.transform;
        lensSet = true;
        StopCoroutine("flicker");
        flickering = false;
        GameControl.control.Freeze();
        thisLight.cookieSize = 6f;

        switch (PlayerC.direction)
        {
            case 0:
                thisLight.cookie = laser0;
                break;
            case 1:
                thisLight.cookie = laser1;
                break;
            case 2:
                thisLight.cookie = laser2;
                break;
            case 3:
                thisLight.cookie = laser3;
                break;
        }
        thisLight.color = new Color(0f, 1f, 0f, 1f);
        thisLight.intensity = 2f;
        yield return new WaitForSeconds(1f);
        thisLight.cookie = normal;
        Destroy(Current);
    }

    IEnumerator setRedLens()
    {
        lensSet = true;
        //StopCoroutine("flicker");
        GameControl.control.Unfreeze();
        setBrightness();
        thisLight.color = new Color(1f, 0f, 0f, 1f);
        thisLight.intensity = 0.5f;

        GameControl.control.aggroable = false;

        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(WaitForKeyDown(KeyCode.X));

        GameControl.control.aggroable = true;
    }

    IEnumerator setYellowLens()
    {
        Current = Instantiate(Yellow);
        Current.transform.parent = Player.transform;
        lensSet = true;
        StopCoroutine("flicker");
        flickering = false;
        GameControl.control.Freeze();
        thisLight.cookieSize = 4f;
        thisLight.color = new Color(1f, 1f, 0f, 1f);
        thisLight.intensity = 1f;
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(WaitForKeyDown(KeyCode.X));
        Destroy(Current);
    }

    IEnumerator setPurpleLens()
    {
        Current = Instantiate(Purple);
        Current.transform.parent = Player.transform;
        lensSet = true;
        StopCoroutine("flicker");
        flickering = false;
        GameControl.control.Freeze();
        thisLight.cookieSize = 4f;
        thisLight.color = new Color(0.5f, 0f, 1f, 1f);
        thisLight.intensity = 1f;
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(WaitForKeyDown(KeyCode.X));
        Destroy(Current);
    }


    void setBrightness()
    {
        if (!flickering && !GameControl.control.paused)
        {
            switch (GameControl.control.numMasks)
            {
                case 0:
                    StartCoroutine(flicker(4f));
                    break;
                case 1:
                    StartCoroutine(flicker(3.75f));
                    break;
                case 2:
                    StartCoroutine(flicker(3.5f));
                    break;
                case 3:
                    StartCoroutine(flicker(3.25f));
                    break;
                case 4:
                    StartCoroutine(flicker(3f));
                    break;
                case 5:
                    StartCoroutine(flicker(2.75f));
                    break;
                case 6:
                    StartCoroutine(flicker(2.5f));
                    break;
                case 7:
                    StartCoroutine(flicker(2.25f));
                    break;
            }
        }
    }

    IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        do
        {
            yield return null;
        } while (!Input.GetKeyDown(keyCode));
    }

    
    IEnumerator flicker(float val)
    {
        flickering = true;
        //if(flickering)
        float phi = Time.time / (0.75f) * 2 * Mathf.PI;
        float amplitude = (Mathf.Cos(phi) * (.05f));
        thisLight.intensity = (amplitude * 0.5f) + 0.5f;
        thisLight.cookieSize = val + (amplitude * 2);
        yield return new WaitForSeconds(0.1f);
        flickering = false;
    }

}
