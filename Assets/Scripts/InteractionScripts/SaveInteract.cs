using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInteract : CharacterInteraction {

    public string[] saveDialogue;
    public string location;

    // Use this for initialization
    void Start() {
        talkControl = GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>();
        GameControl.control.saved = false;
	}
	
	// Update is called once per frame
	public override IEnumerator StartInteraction()
    {
        yield return StartCoroutine(talkControl.StartDialogueSprite(saveDialogue, "vi", 0, 0));
        yield return StartCoroutine(talkControl.StartDialogueSave());
        if (GameControl.control.saved)
        {
            yield return StartCoroutine(talkControl.StartDialogueSaving(location));
        }
        yield return null;
    }
}
