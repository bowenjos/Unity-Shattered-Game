using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.Animations;

public class TalkController : MonoBehaviour {

    protected UnityEngine.UI.Text editText;
    public GameObject talkText;
    public GameObject talkCanvas;
    //public GameObject player;
    public GameObject headPanel;
    public GameObject headImage;

    public GameObject savePanel;
    public GameObject saveGamePanel;

    public GameObject savingPanel;

    public Text characterNameText;
    public Text playTimeText;
    public Text locationText;
    public GameObject saveLens;
    public GameObject saveMasks;

    public Button YesButton;

    public Font estro;
    public Font Vi;
    public Font William;

    private int i;

    enum DialogueStates { NoDialogue, SoloDialogue, SpriteDialogue, SaveDialogue, SavingDialogue };
    DialogueStates currentState;

    // Use this for initialization
    void Start () {
        /*
        //Find the prefab initialized versions of the player, talk ui, and text from the talk ui.
        talkCanvas = GameObject.Find("Talk UI(Clone)");
        player = GameObject.Find("player(Clone)");
        talkText = GameObject.Find("talkText");
        //Get the edittable text component of the text gameobject from the talking ui;
        */
        editText = talkText.GetComponent<Text>();
        
    }
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case DialogueStates.NoDialogue:
                talkCanvas.SetActive(false);
                headPanel.SetActive(false);
                savePanel.SetActive(false);
                saveGamePanel.SetActive(false);
                savingPanel.SetActive(false);
                break;
            case DialogueStates.SoloDialogue:
                talkCanvas.SetActive(true);
                headPanel.SetActive(false);
                savePanel.SetActive(false);
                saveGamePanel.SetActive(false);
                savingPanel.SetActive(false);
                break;
            case DialogueStates.SpriteDialogue:
                talkCanvas.SetActive(true);
                headPanel.SetActive(true);
                savePanel.SetActive(false);
                saveGamePanel.SetActive(false);
                savingPanel.SetActive(false);
                break;
            case DialogueStates.SaveDialogue:
                talkCanvas.SetActive(false);
                headPanel.SetActive(false);
                savePanel.SetActive(true);
                saveGamePanel.SetActive(true);
                savingPanel.SetActive(false);
                break;
            case DialogueStates.SavingDialogue:
                talkCanvas.SetActive(false);
                headPanel.SetActive(false);
                savePanel.SetActive(false);
                saveGamePanel.SetActive(true);
                savingPanel.SetActive(true);
                break;
        }
	}

    public IEnumerator StartDialogueSolo(string[] dialogue)
    {
        //The player is frozen in place to prevent them from moving around while dialogue is occuring
        GameControl.control.Freeze();
        //The dialogue window is made visible
        currentState = DialogueStates.SoloDialogue;
        FontChange("default");
        //Begin iterating over the dialogue lines
        foreach (string text in dialogue)
        {
            //Start the dialogue function for a specific line of dialogue and pause this function until it returns
            //When it returns, continue the loop
            yield return StartCoroutine(Dialogue(text));
        }
        //Make the dialogue window transparent again
        currentState = DialogueStates.NoDialogue;
        //Return control to the player
        GameControl.control.Unfreeze();
    }

    public IEnumerator StartDialogueSprite(string[] dialogue, string newFont, int character, int emotion)
    {
        //The player is frozen in place to prevent them from moving around while dialogue is occuring
        GameControl.control.Freeze();
        //The dialogue window is made visible
        currentState = DialogueStates.SpriteDialogue;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        FontChange(newFont);
        headImage.GetComponent<Animator>().SetInteger("Character", character);
        headImage.GetComponent<Animator>().SetInteger("Mood", emotion);

        //Begin iterating over the dialogue lines
        foreach (string text in dialogue)
        {
            //Start the dialogue function for a specific line of dialogue and pause this function until it returns
            //When it returns, continue the loop
            yield return StartCoroutine(DialogueWithSprites(text));
        }
        //Make the dialogue window transparent again
        currentState = DialogueStates.NoDialogue;
        //Return control to the player
        GameControl.control.Unfreeze();
    }

    public IEnumerator StartDialogueSave()
    {
        currentState = DialogueStates.SaveDialogue;
        GameControl.control.Freeze();
        GameControl.control.LoadTemp();
        characterNameText.text = GameControl.control.playerName;
        System.TimeSpan ts = System.TimeSpan.FromSeconds((int)GameControl.control.playedTimeTemp);
        playTimeText.text = ts.ToString();
        locationText.text = GameControl.control.saveRoomNameTemp;

        for (int i = 0; i < 7; i++)
        {
            saveLens.transform.GetChild(i).gameObject.SetActive(GameControl.control.lensTemp[i]);
            saveMasks.transform.GetChild(i).gameObject.SetActive(GameControl.control.masksTemp[i]);
        }

        YesButton.Select();
        YesButton.OnSelect(null);
        yield return WaitForKeyDown(KeyCode.Z);
        yield return new WaitForEndOfFrame();
        GameControl.control.Unfreeze();
    }

    public IEnumerator StartDialogueSaving(string location)
    {
        GameControl.control.Freeze();
        currentState = DialogueStates.SavingDialogue;
        GameControl.control.saveRoomName = location;
        savingPanel.GetComponentInChildren<Text>().text = "Saving...";
        yield return StartCoroutine(GameControl.control.Save());
        yield return new WaitForSeconds(0.2f);
        savingPanel.GetComponentInChildren<Text>().text = "Saved!";

        GameControl.control.LoadTemp();
        characterNameText.text = GameControl.control.playerName;
        System.TimeSpan ts = System.TimeSpan.FromSeconds((int)GameControl.control.playedTimeTemp);
        playTimeText.text = ts.ToString();
        locationText.text = GameControl.control.saveRoomNameTemp;

        for (int i = 0; i < 7; i++)
        {
            saveLens.transform.GetChild(i).gameObject.SetActive(GameControl.control.lensTemp[i]);
            saveMasks.transform.GetChild(i).gameObject.SetActive(GameControl.control.masksTemp[i]);
        }

        YesButton.Select();
        YesButton.OnSelect(null);
        yield return new WaitForEndOfFrame();
        yield return WaitForKeyDown(KeyCode.Z);
        GameControl.control.Unfreeze();
        currentState = DialogueStates.NoDialogue;

    }


    /********************
    Function Name: Dialogue
    Function Type: IEnumerator
    Purpose: Displays a given line of dialogue on the screen and waits for the player to continue
    Pre: A line of dialogue must be provided
    Post: The DialogueStart function continues or finishes
    ********************/
    protected virtual IEnumerator Dialogue(string text)
    {
        //Starts the animate text function which runs until the text has finished being printed to the screen
        yield return StartCoroutine(AnimateText(text));
        //Starts the wait for keydown function which ways for the player to push the Z button before continuing
        yield return StartCoroutine(WaitForKeyDown(KeyCode.Z));
        editText.text = "";
    }

    protected IEnumerator DialogueWithSprites(string text){

        yield return StartCoroutine(AnimateText(text));
        yield return StartCoroutine(WaitForKeyDown(KeyCode.Z));
        editText.text = "";
    }

    /*******************
    Function Name: WaitForKeyDown
    Function Type: IEnumerator
    Purpose: Waits for the player to press a given key before continuing
    Pre: Nothing specific, but usually a line of dialogue will be on the screen waiting to be continued
    Post: The line of dialogue will end and a new one might start (or not depending on if there is any left)
    *******************/
    protected IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        do
        {
            yield return null;
        } while (!Input.GetKeyDown(keyCode));
    }

    /********************
    Function Name: AnimateText
    Function Type: IEnumerator
    Purpose: Animates a given text string in the style of "Typewriter Text" where text is printed character by character
    Pre: A line of dialogue must be given
    Post: The text has finished printing character by character and the function returns
    To-Do: Add option to skip and not wait for the dialogue to print
    ********************/

    protected IEnumerator AnimateText(string text)
    {
        //for each character in the dialogue string, 
        //update the display string with the current string plus an extra character every 0.03 seconds

        Coroutine co = StartCoroutine(SkipText(text));
        yield return new WaitForEndOfFrame();
        for (i = 0; i < (text.Length + 1); i++)
        {
            editText.text = text.Substring(0, i);
            if (i != 0)
            {
                switch (text[i - 1])
                {
                    case ',':
                        yield return new WaitForSeconds(0.2f);
                        break;
                    case '.':
                        yield return new WaitForSeconds(0.3f);
                        break;
                    case '?':
                        yield return new WaitForSeconds(0.3f);
                        break;
                    case '!':
                        yield return new WaitForSeconds(0.3f);
                        break;
                    default:
                        yield return new WaitForSeconds(0.02f);
                        break;
                }
            }
        }
        StopCoroutine(co);
    }

    protected IEnumerator SkipText(string text)
    {
        yield return WaitForKeyDown(KeyCode.Z);
        i = text.Length-1;
    }

    protected void FontChange(string newFont)
    {
        switch (newFont)
        {
            case "default":
                editText.font = estro;
                editText.fontSize = 24;
                editText.lineSpacing = 1.5f;
                break;
            case "vi":
                editText.font = Vi;
                editText.fontSize = 40;
                editText.lineSpacing = 1f;
                break;
            case "will":
                editText.font = William;
                break;
            default:
                editText.font = estro;
                editText.fontSize = 24;
                editText.lineSpacing = 1.5f;
                break;
        }
    }

    public void OnYesPress()
    {
        GameControl.control.saved = true;
        currentState = DialogueStates.NoDialogue;
        //GameControl.control.Unfreeze();
    }

    public void OnNoPress()
    {
        currentState = DialogueStates.NoDialogue;
        GameControl.control.saved = false;
        //GameControl.control.Unfreeze();
    }
}
