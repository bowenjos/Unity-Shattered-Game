using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViES1 : CharacterInteraction {

	// Use this for initialization
	void Start () {
        if (GameControl.control.ViData.sEntr != 0)
        {
            DestroyThis();
        }
        else
        {
            dialogue = new string[1][];
            dialogue[0] = new string[2];
            dialogue[0][0] = "Welcome, young master. I wish we could have met under better circumstances, but you are here now, so please enjoy your stay.";
            dialogue[0][1] = "If there are things you would like to commit to your memory, please do feel free to find me.";

        }
	}

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        if (GameControl.control.ViData.sEntr == 0)
        {
            yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "vi", 0, 0));
            GameControl.control.ViData.sEntr += 1;
            DestroyThis();
        }
        else
        {
            yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[1], "vi", 0, 0));
        }
    }

    private void DestroyThis()
    {
        this.transform.gameObject.AddComponent<SaveInteract>();
        this.transform.gameObject.GetComponent<SaveInteract>().saveDialogue = new string[] { "Would you like some tea?" };
        this.transform.gameObject.GetComponent<SaveInteract>().location = "Silent Entryhall - SE";
        Destroy(this);
    }
}
