using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPDanceGhostInteraction : ChoiceInteraction
{
    SpriteRenderer thisSprite;

    int danceLevel;
    bool up;
    public float danceTime;
    bool paused;
    bool dancing;

    public Sprite[] danceUp;
    public Sprite[] danceMid;
    public Sprite[] danceLow;

    // Start is called before the first frame update
    void Start()
    {
        danceLevel = 1;
        up = false;
        danceTime = 0.5f;
        talkController = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        thisSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dancing)
        {
            if (danceLevel == 0)
            {
                StartCoroutine(DanceFuckerDance(danceLow));
                up = true;
                danceLevel = 1;
            }
            else if (danceLevel == 1)
            {
                StartCoroutine(DanceFuckerDance(danceMid));
                if (up)
                {
                    danceLevel = 2;
                }
                else
                {
                    danceLevel = 0;
                }
            }
            else if(danceLevel == 2)
            {
                StartCoroutine(DanceFuckerDance(danceUp));
                up = false;
                danceLevel = 1;
            }
        }
    }

    public override IEnumerator StartInteraction()
    {
        paused = true;
        yield return StartCoroutine(talkController.StartDialogueSprite(dialogue, "default", 2, 9));
        yield return StartCoroutine(talkController.StartDialogueChoice(choiceDialogue, leftButtonText, rightButtonText));
        if (talkController.result)
        {
            yield return StartCoroutine(talkController.StartDialogueSprite(leftDialogue, "default", 2, 9));
            danceTime = danceTime / 2f;
        }
        else
        {
            yield return StartCoroutine(talkController.StartDialogueSprite(rightDialogue, "default", 2, 9));
        }
        paused = false;
    }

    public IEnumerator DanceFuckerDance(Sprite[] sprites)
    {
        dancing = true;
        for(int i = 0; i < 1; i++)
        {
            thisSprite.sprite = sprites[0];
            yield return new WaitForSeconds(danceTime);
            yield return StartCoroutine(PauseDance());
            thisSprite.sprite = sprites[1];
            yield return new WaitForSeconds(danceTime);
            yield return StartCoroutine(PauseDance());
            thisSprite.sprite = sprites[0];
            yield return new WaitForSeconds(danceTime);
            yield return StartCoroutine(PauseDance());
            thisSprite.sprite = sprites[2];
            yield return new WaitForSeconds(danceTime);
            yield return StartCoroutine(PauseDance());
        }
        dancing = false;
    }

    public IEnumerator PauseDance()
    {
        while (paused)
        {
            yield return new WaitForEndOfFrame();
        }
    }
}
