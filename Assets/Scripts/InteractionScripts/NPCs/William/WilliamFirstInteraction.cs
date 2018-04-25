using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamFirstInteraction : TriggerInteraction {

    Animator WillAnim;
    TalkController talkCanvas;

    public Light thisLight;
    public Texture cookie;
    public WilliamGuitarController wgc;
    public SpriteRenderer William;

    public string[][] dialogue;

	// Use this for initialization
	void Start () {
        if(GameControl.control.DPMainData.progression != 0)
        {
            Destroy(this);
        }
        WillAnim = GameObject.FindGameObjectWithTag("William").GetComponent<Animator>();
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();

        dialogue = new string[2][];
        dialogue[0] = new string[3];
        dialogue[0][0] = "Oh no...";
        dialogue[0][1] = "Please leave, please leave.";
        dialogue[0][2] = "Nobody was supposed to know I was down here...";
        dialogue[1] = new string[1];
        dialogue[1][0] = "Um. Bye.";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override IEnumerator StartInteraction()
    {
        float dissappear = 1f;

        Debug.Log("Hello");
        GameControl.control.Freeze();
        yield return new WaitForSeconds(2.0f);
        WillAnim.SetBool("Paused", true);
        //stop music
        Destroy(wgc);
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 1, 0));
        GameControl.control.Freeze();
        thisLight.cookie = cookie;
        WillAnim.SetBool("Finished", true);
        yield return new WaitForSeconds(0.2f);
        WillAnim.SetBool("Spin", true);
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[1], "default", 1, 0));
        GameControl.control.Freeze();
        while (dissappear > 0)
        {
            dissappear -= 0.1f;
            thisLight.intensity -= 0.2f;
            William.color = new Color(1f, 1f, 1f, dissappear);
            yield return new WaitForSeconds(0.1f);
        }
        DestroyImmediate(William.gameObject);
        GameControl.control.DPMainData.progression = 1;
        GameControl.control.Unfreeze();
        DestroyImmediate(this.gameObject);
        yield return null;
    }
}
