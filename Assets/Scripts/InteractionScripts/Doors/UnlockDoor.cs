using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : InteractionController
{
    public string zone;
    public string transitionName;
    public float targetx;
    public float targety;

    public int doorNumber;
    public string[] unlockText;
    public string[] lockedText;

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

        GameControl.control.doors[doorNumber] = true;
        GameObject.Find("Talk UI(Clone)");
        yield return StartCoroutine(GameObject.Find("Talk UI(Clone)").GetComponent<TalkController>().StartDialogueSolo(unlockText));
        SceneChangeInteract SCI = gameObject.AddComponent<SceneChangeInteract>();
        SCI.zone = zone;
        SCI.targetSceneName = transitionName;
        SCI.targetX = targetx;
        SCI.targetY = targety;
        Destroy(this);
        yield return null;
    }

}
