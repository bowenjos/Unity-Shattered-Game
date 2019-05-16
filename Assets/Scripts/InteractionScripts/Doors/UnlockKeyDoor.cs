using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockKeyDoor : UnlockDoor
{
    public int keyNumber;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Awake");
        if (GameControl.control.doors[doorNumber])
        {
            Debug.Log("Already Unlocked");
            SceneChangeInteract SCI = gameObject.AddComponent<SceneChangeInteract>();
            SCI.zone = zone;
            SCI.targetSceneName = transitionName;
            SCI.targetX = targetx;
            SCI.targetY = targety;
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator StartInteraction()
    {
        if (!GameControl.control.keys[keyNumber])
        {
            yield return StartCoroutine(GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>().StartDialogueSolo(lockedText));
        }
        else
        {
            GameControl.control.doors[doorNumber] = true;
            yield return StartCoroutine(GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>().StartDialogueSolo(unlockText));
            SceneChangeInteract SCI = gameObject.AddComponent<SceneChangeInteract>();
            SCI.zone = zone;
            SCI.targetSceneName = transitionName;
            SCI.targetX = targetx;
            SCI.targetY = targety;
            Destroy(this);
            yield return null;
        }
        yield return null;
    }
}
