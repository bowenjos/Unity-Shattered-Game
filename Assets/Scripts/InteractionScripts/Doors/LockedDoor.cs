using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public int doorNumber;
    public string[] lockedDialogue;

    // Start is called before the first frame update
    void Start()
    {
        if (GameControl.control.doors[doorNumber])
        {
            Destroy(this);
        }
        else
        {
            Destroy(GetComponent<SceneChangeInteract>());
            DialogueInteraction DI = gameObject.AddComponent<DialogueInteraction>();
            DI.dialogue = lockedDialogue;
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
