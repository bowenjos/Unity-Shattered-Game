using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuControl : MonoBehaviour {

    Button[] buttons;
    Text buttonText;

    // Use this for initialization
    void Start () {
        buttons = this.GetComponentsInChildren<Button>();

    }
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i < 10; i++)
        {
            if(GameControl.control.items[i] != "")
            {
                buttons[i].gameObject.SetActive(true);
                buttonText = buttons[i].GetComponentInChildren<Text>();
                buttonText.text = GameControl.control.items[i];

                //Dynamically set button function based on item, hmmmm

            }
            else if(GameControl.control.items[i] == "")
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
	}
}
