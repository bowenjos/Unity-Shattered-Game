using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilliamMiniBossInteraction : TriggerInteraction
{
    TalkController talkCanvas;
    JukeBoxController jukebox;
    AudioSource heartbeat;
    CameraController cameraControl;

    public Light thisLight;
    public Texture cookie;
    public Texture getUpCookie;
    public InstrumentController wgc;
    public SpriteRenderer William;

    public SpriteRenderer[] entourage;
    public Light[] entourageLight;

    public SpriteRenderer chadley;
    public Light chadleyLight;

    public SpriteRenderer tar;
    public SpriteRenderer mirrorTar;
    public Sprite[] tarForms;

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
        heartbeat = this.GetComponent<AudioSource>();
        cameraControl = GameObject.Find("Camera(Clone)").GetComponent<CameraController>();

        dialogue = new string[8][];
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

        dialogue[3] = new string[10];
        dialogue[3][0] = "Oh... By the way.";
        dialogue[3][1] = "Just wanted to let you know...";
        dialogue[3][2] = "I've been hearing you practicing.";
        dialogue[3][3] = "You're pretty good.";
        dialogue[3][4] = "But your endurance is pretty lacking, my guy.";
        dialogue[3][5] = "When you get to the end of a piece it just starts to fall apart.";
        dialogue[3][6] = "And your sixteenth notes? Sloppy bro, Sloppy.";
        dialogue[3][6] = "You should consider joining in on our reps,";
        dialogue[3][7] = "I think it'd do you some good, yeah bruh?"; 
        dialogue[3][8] = "Anyway, see you at the show, bro.";
        dialogue[3][9] = "Good luck.";

        dialogue[4] = new string[1];
        dialogue[4][0] = "...";

        dialogue[5] = new string[2];
        dialogue[5][0] = "...full of himself...";
        dialogue[5][1] = "...making me run around...";

        dialogue[6] = new string[3];
        dialogue[6][0] = "...not good enough...";
        dialogue[6][1] = "...sloppy...";
        dialogue[6][2] = "...sloppy sloppy sloppy...";

        dialogue[7] = new string[2];
        dialogue[7][0] = "Why...";
        dialogue[7][1] = "Am I like this...";
    }

    public override IEnumerator StartInteraction()
    {
        GameControl.control.Freeze();
        jukebox.StopSong();


        StartCoroutine(cameraControl.FadeIn(2f));

        yield return StartCoroutine(cameraControl.MoveFromPointToPoint(new Vector2(0, -0.48f), new Vector2(0, 0f), 1.5f));

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
        jukebox.PlaySong("InTheDeepestDarkness");
        StartCoroutine(jukebox.FadeIn(1f));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(cameraControl.ShakeCamera(0.25f, 0.1f));
        heartbeat.Play();
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[4], "default", 1, 0));
        StartCoroutine(cameraControl.ShakeCamera(0.25f, 0.15f));
        tar.sprite = tarForms[0];
        mirrorTar.sprite = tarForms[0];
        heartbeat.Play();
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[4], "default", 1, 0));
        StartCoroutine(cameraControl.ShakeCamera(0.25f, 0.2f));
        tar.sprite = tarForms[1];
        mirrorTar.sprite = tarForms[1];
        heartbeat.Play();
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[5], "default", 1, 0));
        StartCoroutine(cameraControl.ShakeCamera(0.25f, 0.2f));
        tar.sprite = tarForms[2];
        mirrorTar.sprite = tarForms[2];
        heartbeat.Play();
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[6], "default", 1, 0));
        heartbeat.Play();
        yield return StartCoroutine(cameraControl.ShakeCamera(0.25f, 0.2f));
        heartbeat.Play();
        yield return StartCoroutine(cameraControl.ShakeCamera(0.25f, 0.2f));
        tar.sprite = tarForms[3];
        mirrorTar.sprite = tarForms[3];
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[7], "default", 1, 0));
        yield return StartCoroutine(Vanish(William, thisLight));
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
