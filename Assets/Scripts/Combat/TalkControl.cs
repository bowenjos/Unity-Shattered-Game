using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Dialogue(string text)
    {
        for(int i = 0; i < (text.Length+1); i++)
        {
            this.GetComponent<Text>().text = text.Substring(0, i);
            yield return new WaitForSeconds(.03f);
        }
        yield return null;
    }

    public IEnumerator StartDialogue(string[] dialogue)
    {
        for(int i = 0; i < dialogue.Length; i++)
        {
            yield return StartCoroutine(Dialogue(dialogue[i]));
            yield return new WaitForSeconds(1f);
        }
    }

}
