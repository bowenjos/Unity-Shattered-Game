using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViES1 : CharacterInteraction {

    // Use this for initialization
    public string saveLocation;
    public string[] saveDialogue;

	void Start () {
        if (GameControl.control.ViData.sEntr == true)
        {
            DestroyThis();
        }
        else
        {
            if (GameControl.control.numMasks == 0)
            {
                dialogue = new string[1][];
                dialogue[0] = new string[2];
                dialogue[0][0] = "Welcome, young master. I wish we could have met under better circumstances, but you are here now, so please enjoy your stay.";
                dialogue[0][1] = "If there are things you would like to <color=cyan>commit to your memory</color>, please do feel free to find me.";
            }
        }
	}

    // Update is called once per frame
    public override IEnumerator StartInteraction()
    {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        switch (GameControl.control.numMasks)
        {
            case 0:
                yield return StartCoroutine(talkControl.StartDialogueSprite(dialogue[0], "vi", 0, 0));
                GameControl.control.ViData.sEntr = true;
                DestroyThis();
                break;
            default:
                DestroyThis();
                break;
        }
    }

    private void DestroyThis()
    {
        this.transform.gameObject.AddComponent<SaveInteract>();
        this.transform.gameObject.GetComponent<SaveInteract>().saveDialogue = saveDialogue;
        this.transform.gameObject.GetComponent<SaveInteract>().location = saveLocation;
        Destroy(this);
    }
}
