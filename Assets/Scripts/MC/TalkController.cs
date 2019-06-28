using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.Animations;

public class TalkController : MonoBehaviour {

    protected Text editText;
    protected Text editSpriteText;
    public GameObject talkText;
    public GameObject talkSpriteText;
    public GameObject talkCanvas;
    //public GameObject player;
    public GameObject headPanel;

    public GameObject savePanel;
    public GameObject saveGamePanel;

    public GameObject savingPanel;

    public AudioSource audioPlayer;

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
    private bool marked;
    private string markdown = "";

    public Image talkSprite;
    public Sprite[] neutral;
    public Sprite[] neutralNoMask;

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
        editSpriteText = talkSpriteText.GetComponent<Text>();
        audioPlayer = this.gameObject.GetComponent<AudioSource>();
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
        SpriteChange(character, emotion);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        FontChange(newFont);

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
        editSpriteText.text = "";
    }

    protected IEnumerator DialogueWithSprites(string text){

        yield return StartCoroutine(AnimateSpriteText(text));
        yield return StartCoroutine(WaitForKeyDown(KeyCode.Z));
        editText.text = "";
        editSpriteText.text = "";
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
    ********************/

    protected IEnumerator AnimateText(string text)
    {
        //for each character in the dialogue string, 
        //update the display string with the current string plus an extra character every 0.03 seconds

        Coroutine co = StartCoroutine(SkipText(text));
        yield return new WaitForEndOfFrame();
        //For each character in the dialogue string
        for (i = 0; i < (text.Length); i++)
        {
            //First, check to see if the character is the beginning (or beginning of the end) of a Markdown styliing
            switch (text[i])
            {
                //If the character is < (the beginning of a markdown styling)
                case '<':
                    //If the text is not current marked
                    if (marked == false)
                    {
                        //Begin markdown
                        i = ResolveMarkdown(i, text);
                    }
                    else {
                        //End Markdown
                        i = FinishMarkdown(i, text);
                    }
                    break;
                //If the character is not a markdown character
                default:
                    //If the text being written is currently styled with markdown
                    if (marked == true)
                    {
                        //Append the markdown string to the end of the substring (EX: </color>)
                        editText.text = text.Substring(0, i) + markdown;
                    }
                    else
                    {
                        //Act natural (print the current step of the typewritter text string
                        editText.text = text.Substring(0, i);
                    }
                    break;
            }

            //If this character isn't the very first character
            audioPlayer.Play();
            if (i != 0)
            {
                //Check the last printed character
                //This switch case essentially servers to add slight time delays after periods and commas, or defaults to the normal time delay
                //I might/will probably add future functionality for variable delays, but I also might just do that in a different function, or maybe just put this in it's own function Generally
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
        //Stop the coroutine that lets you skip past all the text if you don't want to watch it print
        StopCoroutine(co);
        editText.text = text;
        editSpriteText.text = "";
    }

    protected IEnumerator AnimateSpriteText(string text)
    {
        //for each character in the dialogue string, 
        //update the display string with the current string plus an extra character every 0.03 seconds

        Coroutine co = StartCoroutine(SkipText(text));
        yield return new WaitForEndOfFrame();
        //For each character in the dialogue string
        for (i = 0; i < (text.Length); i++)
        {
            //First, check to see if the character is the beginning (or beginning of the end) of a Markdown styliing
            switch (text[i])
            {
                //If the character is < (the beginning of a markdown styling)
                case '<':
                    //If the text is not current marked
                    if (marked == false)
                    {
                        //Begin markdown
                        i = ResolveMarkdown(i, text);
                    }
                    else {
                        //End Markdown
                        i = FinishMarkdown(i, text);
                    }
                    break;
                //If the character is not a markdown character
                default:
                    //If the text being written is currently styled with markdown
                    if (marked == true)
                    {
                        //Append the markdown string to the end of the substring (EX: </color>)
                        editSpriteText.text = text.Substring(0, i) + markdown;
                    }
                    else
                    {
                        //Act natural (print the current step of the typewritter text string
                        editSpriteText.text = text.Substring(0, i);
                    }
                    break;
            }

            //If this character isn't the very first character
            audioPlayer.Play();
            if (i != 0)
            {
                //Check the last printed character
                //This switch case essentially servers to add slight time delays after periods and commas, or defaults to the normal time delay
                //I might/will probably add future functionality for variable delays, but I also might just do that in a different function, or maybe just put this in it's own function Generally
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
        //Stop the coroutine that lets you skip past all the text if you don't want to watch it print
        StopCoroutine(co);
        editSpriteText.text = text;
        editText.text = "";
    }

    /********************
    Function Name: ResolveMarkdown
    Function Type: Int
    Purpose: Allows the usage of Markdown style in the text and also typewriting
    Pre: A line of dialogue must be given, and a position in the string must be given
    Post: The end of the markdown string has been received, the string has been updated
    ********************/

    protected int ResolveMarkdown(int i, string text)
    {
        int j = 0;
        int k = 0;
        int l = 0;
        //Set the marked variable to true (for use in the main Animate Text function)
        marked = true;
        //Find the end of the first set of markdown (EX: <color=blue>)
        for (j = i; j < text.Length; j++)
        {
            if (text[j] == '>')
            {
                break;
            }
        }
        //Find the beginning of the end of the markdown (EX: </color>) Save that substring into a string
        for (l = j+2; l < text.Length; l++) { 
            if (text[l] == '<') {
                k = l;
            }
            if (text[l] == '>')
            {
                markdown = text.Substring(k, l - k + 1);
            }
        }
        //Set the current string to whatever the substring was, but skipped forward to the end of the beginning of the markdown and with the end appended on
        //It should look like this (EX: this part of the string isn't markdown but <i></i>)
        //When the string continues on the rest of the string wille be within the markdown until the markdown ends
        editText.text = text.Substring(0, j + 1) + markdown;
        //Return the value of the position inthe substring that is after the first set of markdown
        return j;

    }

    /********************
    Function Name: FinishMarkdown
    Function Type: Int
    Purpose: Concludes the usage of Markdown style in the text and also typewriting
    Pre: A line of dialogue must be given
    Post: traversing the markdown is completed
    ********************/

    protected int FinishMarkdown(int i, string text)
    {
        //Set marked to false, and skip forward the length of the markdown string
        marked = false;
        i = i + markdown.Length;
        editText.text = text.Substring(0, i);
        return i;
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
                editText.fontSize = 55;
                editText.lineSpacing = 1.2f;
                break;
            case "vi":
                editText.font = Vi;
                editText.fontSize = 80;
                editText.lineSpacing = 1.1f;
                break;
            case "will":
                editText.font = William;
                break;
            default:
                editText.font = estro;
                editText.fontSize = 55;
                editText.lineSpacing = 1.2f;
                break;
        }
    }

    //Character: 0 Vi, 1 Will, 2 Yolanda, 3 Elise, 4 Thongsai, 5 Aeron, 6 Selene, 7 Des
    //Mood: 0 Neutral,


    protected void SpriteChange(int character, int mood)
    {
        switch (mood)
        {
            case 0:
                talkSprite.sprite = neutral[character];
                break;
            case 1:
                talkSprite.sprite = neutralNoMask[character];
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
