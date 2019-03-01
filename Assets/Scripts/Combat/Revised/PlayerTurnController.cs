using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurnController : MonoBehaviour {

    public enum MenuStates { MainSelect, HelpSelect, ItemSelect, EnemyTurn };
    public MenuStates currentState;

    public GameObject MenuPanel;
    public GameObject TalkPanel;
    public GameObject CombatPanel;

    protected Image MenuVisibility;
    protected Image TalkVisibility;
    protected Image CombatVisibility;

    public Button HelpButton;
    public Button ItemButton;
    public Button RestButton;
    public Button FleeButton;

    public Button TalkButton;
    public Button HugButton;
    public Button AffirmButton;
    public Button SitButton;
    public Button ActButton;
    public Button GiftButton;

    public TalkControl textBox;

    public Text enemyText;

	// Use this for initialization
	void Start () {
        currentState = MenuStates.MainSelect;
        MenuVisibility = MenuPanel.gameObject.GetComponent<Image>();
        TalkVisibility = TalkPanel.gameObject.GetComponent<Image>();
        CombatVisibility = CombatPanel.gameObject.GetComponent<Image>();
        HelpButton.Select();

    }
	
	// Update is called once per frame
	void Update () {

        /*
        if(BattleController.BC.currentState != BattleController.BattleState.PlayerTurn)
        {
            currentState = MenuStates.EnemyTurn;
        }
        */

        switch (currentState)
        {
            case MenuStates.MainSelect:
                MenuVisibility.color = new Color(1f, 1f, 1f, 1f);
                HelpButton.interactable = true;
                ItemButton.interactable = true;
                RestButton.interactable = true;
                FleeButton.interactable = true;
                TalkVisibility.color = new Color(1f, 1f, 1f, 1f);
                CombatVisibility.color = new Color(1f, 1f, 1f, 0f);
                TalkButton.gameObject.SetActive(false);
                HugButton.gameObject.SetActive(false);
                AffirmButton.gameObject.SetActive(false);
                SitButton.gameObject.SetActive(false);
                ActButton.gameObject.SetActive(false);
                GiftButton.gameObject.SetActive(false);

                break;
            case MenuStates.HelpSelect:
                MenuVisibility.color = new Color(1f, 1f, 1f, 0.7f);
                HelpButton.interactable = false;
                ItemButton.interactable = false;
                RestButton.interactable = false;
                FleeButton.interactable = false;
                //TalkVisibility.color = new Color(1f, 1f, 1f, 0f);
                CombatVisibility.color = new Color(1f, 1f, 1f, 1f);
                //CombatPanel.SetActive(true);
                TalkButton.gameObject.SetActive(true);
                HugButton.gameObject.SetActive(true);
                AffirmButton.gameObject.SetActive(true);
                SitButton.gameObject.SetActive(true);
                ActButton.gameObject.SetActive(true);
                GiftButton.gameObject.SetActive(true);
                break;
            case MenuStates.ItemSelect:
                break;
            case MenuStates.EnemyTurn:
                MenuVisibility.color = new Color(1f, 1f, 1f, 0.7f);
                HelpButton.interactable = false;
                ItemButton.interactable = false;
                RestButton.interactable = false;
                FleeButton.interactable = false;
                CombatVisibility.color = new Color(1f, 1f, 1f, 0f);
                TalkButton.gameObject.SetActive(false);
                HugButton.gameObject.SetActive(false);
                AffirmButton.gameObject.SetActive(false);
                SitButton.gameObject.SetActive(false);
                ActButton.gameObject.SetActive(false);
                GiftButton.gameObject.SetActive(false);
                break;
            default:
                currentState = MenuStates.MainSelect;
                break;
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            if(currentState == MenuStates.HelpSelect)
            {
                StartCoroutine(MoveTalkBack());
            }
            else
            {
                currentState = MenuStates.MainSelect;
            }
        }
        
	}

    public IEnumerator MoveTalkBack()
    {
        RectTransform TPRT = TalkPanel.GetComponent<RectTransform>();
        enemyText.gameObject.SetActive(false);
        CombatPanel.SetActive(false);
        float dx = 0.7f;
        while (dx < 0.9f)
        {
            dx += ((.01f / 0.2f) * 0.9f);
            TPRT.anchorMax = new Vector2(dx, TPRT.anchorMax.y);
            TPRT.anchorMin = new Vector2(1 - dx, TPRT.anchorMin.y);
            yield return new WaitForSeconds(0.02f);
        }
        enemyText.gameObject.SetActive(true);
        currentState = MenuStates.MainSelect;
        yield return new WaitForEndOfFrame();
        HelpButton.Select();
        yield return null;
    }

    public IEnumerator MoveTalk()
    {
        RectTransform TPRT = TalkPanel.GetComponent<RectTransform>();
        enemyText.gameObject.SetActive(false);
        float dx = 0.9f;
        while(dx > 0.7f)
        {
            dx -= ((.01f / 0.2f) * 0.7f);
            TPRT.anchorMax = new Vector2(dx, TPRT.anchorMax.y);
            TPRT.anchorMin = new Vector2(1 - dx, TPRT.anchorMin.y);
            yield return new WaitForSeconds(0.02f);
        }  
        currentState = MenuStates.HelpSelect;
        CombatPanel.SetActive(true);
        TalkButton.gameObject.SetActive(true);
        HugButton.gameObject.SetActive(true);
        AffirmButton.gameObject.SetActive(true);
        SitButton.gameObject.SetActive(true);
        ActButton.gameObject.SetActive(true);
        GiftButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        enemyText.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        TalkButton.Select();
        yield return null;
    }

    //MAIN MENU FUNCTIONS

    public void OnHelpButtonPress()
    {
        StartCoroutine(MoveTalk());
        
    }

    public void OnItemButtonPress()
    {
        currentState = MenuStates.HelpSelect;
    }

    public void OnRestButtonPress()
    {

    }

    public void OnFleeButtonPress()
    {

    }

    //HELP MENU FUNCTIONS

    public void OnTalkButtonPress()
    {

        BattleController.BC.EndTurnPlayer();
    }

    public void OnHugButtonPress()
    {

        BattleController.BC.EndTurnPlayer();
    }

    public void OnAffirmButtonPress()
    {

        BattleController.BC.EndTurnPlayer();
    }

    public void OnSitButtonPress()
    {

        BattleController.BC.EndTurnPlayer();
    }

    public void OnActButtonPress()
    {

        BattleController.BC.EndTurnPlayer();
    }

    public void OnGiftButtonPress()
    {

        BattleController.BC.EndTurnPlayer();
    }
}
