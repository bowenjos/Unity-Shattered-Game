using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamMiniBossInteraction : TriggerInteraction
{
    TalkController talkCanvas;
    JukeBoxController jukebox;
    Shake cameraShake;

    public Light thisLight;
    public Texture cookie;
    public Texture getUpCookie;
    public InstrumentController wgc;
    public SpriteRenderer William;

    public SpriteRenderer[] entourage;
    public Light[] entourageLight;

    public SpriteRenderer chadley;
    public Light chadleyLight;

    public string[][] dialogue;

    // Start is called before the first frame update
    void Start()
    {

        if (GameControl.control.DPMainData.progression != 9)
        {
            Destroy(this.gameObject);
        }
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        jukebox = GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>();
        cameraShake = GameObject.Find("Camera(Clone)").GetComponent<Shake>();

        dialogue = new string[5][];
        dialogue[0] = new string[3];
        dialogue[0][0] = "Oh hey Will, my man.";
        dialogue[0][1] = "We were just getting some last minute reps in before the big show.";
        dialogue[0][2] = "You gotta stay in shape if you want to be able to do long performances.";

        dialogue[1] = new string[5];
        dialogue[1][0] = "Oh... No, I was just-";
        dialogue[1][1] = "The shows starting soon, so I wanted to-";
        dialogue[1][2] = "Make sure everything was in order.";
        dialogue[1][3] = "For the show...";
        dialogue[1][4] = "Cause it's starting... soon.";

        dialogue[2] = new string[3];
        dialogue[2][0] = "Oh!";
        dialogue[2][1] = "My bad, my guy.";
        dialogue[2][2] = "We will be at the stage uno momento.";

        dialogue[3] = new string[6];
        dialogue[3][0] = "Oh... By the way.";
        dialogue[3][1] = "Just wanted to let you know...";
        dialogue[3][2] = "You have something in your teeth.";
        dialogue[3][3] = "Anyway."; 
        dialogue[3][4] = "See you at the show, bro.";
        dialogue[3][5] = "Good luck.";

        dialogue[4] = new string[1];
        dialogue[4][0] = "...";

    }

    public override IEnumerator StartInteraction()
    {
        GameControl.control.Freeze();
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 4, 10));
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[1], "default", 1, 0));
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[2], "default", 4, 10));
        yield return new WaitForSeconds(0.5f);
        for(int i = 0; i < 5; i++)
        {
            StartCoroutine(Vanish(entourage[i], entourageLight[i]));
            yield return new WaitForSeconds(0.5f);
        }
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[3], "default", 4, 10));
        yield return StartCoroutine(Vanish(chadley, chadleyLight));
        yield return new WaitForSeconds(0.5f);
        cameraShake.SetSpeed(0.05f);
        StartCoroutine(cameraShake.ShakeObject(0.25f));
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[4], "default", 1, 0));
    }

    public IEnumerator Vanish(SpriteRenderer sprite, Light light)
    {
        float dissappear = 1f;
        while (dissappear > 0)
        {
            dissappear -= 0.1f;
            light.intensity -= 0.2f;
            sprite.color = new Color(1f, 1f, 1f, dissappear);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
