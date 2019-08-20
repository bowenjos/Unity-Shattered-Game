using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour {

    private bool dark = false;
    private bool now = false;
    private bool transitioning = false;
    protected Image ZoneTransition;
    protected Text zoneText;
    public string currentZone;
    public string newZone;

    public bool elevatorTransfer;

    private JukeBoxController jukebox;
    private Coroutine co;

    public RectTransform enterCombatZero;
    public RectTransform enterCombatOne;

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
        if (newZone != currentZone && currentZone != "")
        {
            transitioning = true;
            currentZone = newZone;
            if (co != null)
            {
                StopCoroutine(co);
            }
            co = StartCoroutine(displayZone());
        }
        else
        {
            currentZone = newZone;
        }

        if (dark == true && transitioning == false)
        {
            StartCoroutine(transitionIn());
        }
        if(now == true)
        {
            StartCoroutine(transitionInNow());
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
        StartCoroutine(jukebox.FadeOut(0.6f));
        yield return new WaitForSeconds(1f);
        for (float i = 0f; i < 255f; i += 4.25f)
        {
            zoneText.color = new Color(255f, 255f, 255f, i / 255f);
            yield return new WaitForSeconds(0.025f);
        }
        jukebox.ResumeOverworldSong();
        StartCoroutine(jukebox.FadeIn(0.6f));
        zoneText.color = new Color(255f, 255f, 255f, 255f);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(transitionIn());
       
        
        yield return new WaitForSeconds(3f);
        for (float i = 255f; i > 0f; i -= 8.5f) 
        {
            zoneText.color = new Color(255f, 255f, 255f, i / 255f);
            yield return new WaitForSeconds(0.005f);
        }
        zoneText.color = new Color(255f, 255f, 255f, 0f);
        transitioning = false;
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

    public IEnumerator transitionNow()
    {
        ZoneTransition.color = new Color(0f, 0f, 0f, 1);
        yield return new WaitForSeconds(0.4f);
        now = true;
        yield return null;
    }

    public IEnumerator transitionInNow()
    {
        yield return new WaitForSeconds(0.4f);
        GameControl.control.Unfreeze();
        ZoneTransition.color = new Color(0f, 0f, 0f, 0f);
        now = false;
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

    public IEnumerator EnterCombat()
    {
        yield return null;
        while (enterCombatOne.position.x >= 800)
        {
            enterCombatZero.position = new Vector2(enterCombatZero.position.x - 100, 540);
            enterCombatOne.position = new Vector2(enterCombatOne.position.x - 100, 540);
            yield return new WaitForSeconds(0.001f);
        }
        yield return StartCoroutine(transitionOut());
        enterCombatZero.position = new Vector2(2000, 540);
        enterCombatOne.position = new Vector2(5000, 540);
    }
    
}
