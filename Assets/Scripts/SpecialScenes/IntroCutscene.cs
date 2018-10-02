using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour {

    public Text editText;
    public Image editImage;
    public GameObject Storm;

    private int i;
    private bool marked;
    private string markdown = "";

    public Sprite image1;

    void Awake()
    {
        editImage.gameObject.SetActive(false);
        Storm.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(GameStart());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GameStart()
    {
        editText.text = "";
        yield return new WaitForSeconds(0.5f);
        yield return AnimateText("Hello?");
        yield return new WaitForSeconds(2f);
        yield return AnimateText("Can you help me?");
        yield return new WaitForSeconds(2f);
        yield return AnimateText("I seem to have lost my <color=cyan>way</color>.");
        yield return new WaitForSeconds(2f);
        editText.text = "";
        editText.color = new Color(0.65f, 0.65f, 0.65f, 1f);
        yield return AnimateText("Hoo hoo");
        yield return new WaitForSeconds(2f);
        yield return AnimateText("I cannot help you find your <color=cyan>way</color>, child.");
        yield return new WaitForSeconds(2f);
        yield return AnimateText("You must find it for yourself.");
        yield return new WaitForSeconds(2f);
        editText.text = "";
        editText.color = new Color(1f, 1f, 1f, 1f);
        yield return AnimateText("But... No matter how far I go,");
        yield return new WaitForSeconds(1.5f);
        yield return AnimateText("I never seem to get where I am going.");
        yield return new WaitForSeconds(2f);
        editText.text = "";
        editText.color = new Color(0.65f, 0.65f, 0.65f, 1f);
        yield return AnimateText("It sounds like you have already found your <color=cyan>way</color>.");
        yield return new WaitForSeconds(2f);
        yield return AnimateText("You just don't know it yet.");
        yield return new WaitForSeconds(2f);
        yield return FadeOut();
        yield return new WaitForSeconds(2f);
        editImage.gameObject.SetActive(true);
        StartCoroutine(StartStorm());
        editImage.sprite = image1;
        editImage.color = new Color(1, 1, 1, 1);
        yield return null;
    }





    public IEnumerator StartStorm()
    {
        Storm.SetActive(true);
        Storm.GetComponent<Rigidbody2D>().velocity = new Vector3(-0.1f, -0.3f, 0f);
        yield return new WaitForSeconds(20f);
        Storm.SetActive(false);
    }


    public IEnumerator FadeOut()
    {
        for (float i = 255f; i > 0f; i -= 17f)
        {
            editText.color = new Color(0.65f, 0.65f, 0.65f, i / 255f);
            yield return new WaitForSeconds(0.04f);
        }
        editText.color = new Color(0f, 0f, 0f, 0);
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
                        yield return new WaitForSeconds(0.075f);
                        break;
                }
            }
        }
        //Stop the coroutine that lets you skip past all the text if you don't want to watch it print
        editText.text = text;
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
        for (l = j + 2; l < text.Length; l++)
        {
            if (text[l] == '<')
            {
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

}
