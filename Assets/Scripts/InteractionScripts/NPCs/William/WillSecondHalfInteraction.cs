using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WillSecondHalfInteraction : CharacterInteraction
{
    TalkController talkCanvas;
    public SpriteRenderer William;
    public Light thisLight;

    // Start is called before the first frame update
    void Start()
    {
        talkCanvas = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();

        dialogue = new string[1][];
        dialogue[0] = new string[5];
        dialogue[0][0] = "They're all missing...";
        dialogue[0][1] = "Well I guess we should just cancel the whole show...";
        dialogue[0][2] = "No... I should find them.";
        dialogue[0][3] = "Meet me in the reception room to the right of the stage.";
        dialogue[0][4] = "You should have gone through it if you took the elevator down.";
    }

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {

        GameControl.control.Freeze();
        yield return StartCoroutine(talkCanvas.StartDialogueSprite(dialogue[0], "default", 1, 0));
        GameControl.control.DPMainData.progression = 7;
        GameControl.control.DPMainData.viTalked = false;
        yield return StartCoroutine(Visability(false));
        Destroy(this.gameObject);
        yield return null;

    }

    IEnumerator Visability(bool appearing)
    {
        float dissappear = 1f;
        thisLight.intensity = 0.8f;
        if (!appearing)
        {
            while (dissappear > 0)
            {
                dissappear -= 0.1f;
                thisLight.intensity -= 0.08f;
                William.color = new Color(1f, 1f, 1f, dissappear);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            dissappear = 0f;
            while (dissappear < 1f)
            {
                dissappear += .1f;
                thisLight.intensity += 0.08f;
                William.color = new Color(1f, 1f, 1f, dissappear);
                yield return new WaitForSeconds(0.1f);
            }
        }


    }
}
