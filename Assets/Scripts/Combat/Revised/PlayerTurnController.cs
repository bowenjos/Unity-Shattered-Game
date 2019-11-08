
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
    public SwaySideToSide enemySway;
    public Shake enemyShake;

    // Use this for initialization
    void Start() {
        currentState = MenuStates.MainSelect;
        MenuVisibility = MenuPanel.gameObject.GetComponent<Image>();
        TalkVisibility = TalkPanel.gameObject.GetComponent<Image>();
        CombatVisibility = CombatPanel.gameObject.GetComponent<Image>();
        HelpButton.Select();

    }

    // Update is called once per frame
    void Update() {

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

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (currentState == MenuStates.HelpSelect)
            {
                StartCoroutine(MoveTalkBack());
            }
            else
            {
                currentState = MenuStates.MainSelect;
            }
        }

    }

    //Slides the bottom menu back to it's full size
    public IEnumerator MoveTalkBack()
    {
        RectTransform TPRT = TalkPanel.GetComponent<RectTransform>();
        enemyText.gameObject.SetActive(false);
        CombatPanel.SetActive(false);
        float dx = 0.65f;
        while (dx < 0.8f)
        {
            dx += ((.01f / 0.2f) * 0.8f);
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

    //Slides the bottom menu into it's smaller size
    public IEnumerator MoveTalk()
    {
        RectTransform TPRT = TalkPanel.GetComponent<RectTransform>();
        enemyText.gameObject.SetActive(false);
        float dx = 0.8f;
        while (dx > 0.65f)
        {
            dx -= ((.01f / 0.2f) * 0.65f);
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


    public void OnRestButtonPress()
    {
        StartCoroutine(HealPlayer());
        //Heal Player
        //End turn
    }

    public IEnumerator HealPlayer()
    {
        currentState = MenuStates.EnemyTurn;
        int targetHealth = GameControl.control.health + GameControl.control.healFactor;
        if (targetHealth > GameControl.control.maxHealth)
        {
            targetHealth = GameControl.control.maxHealth;
        }
        yield return StartCoroutine(textBox.Dialogue("You took some time to take care of yourself."));
        BattleController.BC.healingTouched = true;
        for (int i = GameControl.control.health; i <= targetHealth; i++)
        {
            GameControl.control.health = i;
            yield return new WaitForEndOfFrame();
        }
        //sound effect
        yield return new WaitForSeconds(0.2f);
        currentState = MenuStates.EnemyTurn;
        StartCoroutine(BattleController.BC.EndTurnPlayer());
    }

    public void OnLookButtonPress()
    {
        currentState = MenuStates.EnemyTurn;
        StartCoroutine(BattleController.BC.DisplayEnemyData());
    }

    public void OnFleeButtonPress()
    {
        //Try to flee
        //Flee if successful
        //otherwise end turn
    }

    //HELP MENU FUNCTIONS

    public void OnTalkButtonPress()
    {
        StartCoroutine(textBox.Dialogue(""));
        StartCoroutine(talkButtonPress());
    }

    IEnumerator talkButtonPress()
    {
        yield return StartCoroutine(MoveTalkBack());
        currentState = MenuStates.EnemyTurn;
        int rand = Random.Range(0, BattleController.BC.Enemy.talkDialogue.Length);
        yield return StartCoroutine(textBox.Dialogue(BattleController.BC.Enemy.talkDialogue[rand]));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(EnemyResponse(0));
    }

    public void OnHugButtonPress()
    {
        StartCoroutine(textBox.Dialogue(""));
        StartCoroutine(hugButtonPress());

    }

    IEnumerator hugButtonPress()
    {
        yield return StartCoroutine(MoveTalkBack());
        currentState = MenuStates.EnemyTurn;
        int rand = Random.Range(0, BattleController.BC.Enemy.hugDialogue.Length);
        yield return StartCoroutine(textBox.Dialogue(BattleController.BC.Enemy.hugDialogue[rand]));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(EnemyResponse(1));
    }

    public void OnAffirmButtonPress()
    {
        StartCoroutine(textBox.Dialogue(""));
        StartCoroutine(affirmButtonPress());

    }

    IEnumerator affirmButtonPress()
    {
        yield return StartCoroutine(MoveTalkBack());
        currentState = MenuStates.EnemyTurn;
        int rand = Random.Range(0, BattleController.BC.Enemy.affirmDialogue.Length);
        yield return StartCoroutine(textBox.Dialogue(BattleController.BC.Enemy.affirmDialogue[rand]));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(EnemyResponse(2));
    }

    public void OnSitButtonPress()
    {
        StartCoroutine(textBox.Dialogue(""));
        StartCoroutine(sitButtonPress());

    }

    IEnumerator sitButtonPress()
    {
        yield return StartCoroutine(MoveTalkBack());
        currentState = MenuStates.EnemyTurn;
        int rand = Random.Range(0, BattleController.BC.Enemy.sitDialogue.Length);
        yield return StartCoroutine(textBox.Dialogue(BattleController.BC.Enemy.sitDialogue[rand]));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(EnemyResponse(3));
    }

    public void OnActButtonPress()
    {
        StartCoroutine(textBox.Dialogue(""));
        StartCoroutine(actButtonPress());

    }

    IEnumerator actButtonPress()
    {
        yield return StartCoroutine(MoveTalkBack());
        currentState = MenuStates.EnemyTurn;
        int rand = Random.Range(0, BattleController.BC.Enemy.actDialogue.Length);
        yield return StartCoroutine(textBox.Dialogue(BattleController.BC.Enemy.actDialogue[rand]));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(EnemyResponse(4));
    }

    public void OnGiftButtonPress()
    {
        StartCoroutine(textBox.Dialogue(""));
        StartCoroutine(giftButtonPress());

    }

    IEnumerator giftButtonPress()
    {
        yield return StartCoroutine(MoveTalkBack());
        currentState = MenuStates.EnemyTurn;
        int rand = Random.Range(0, BattleController.BC.Enemy.giftDialogue.Length);
        yield return StartCoroutine(textBox.Dialogue(BattleController.BC.Enemy.giftDialogue[rand]));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(EnemyResponse(5));
        
    }

    //HOW THE ENEMY RESPONDS

    IEnumerator EnemyResponse(int type)
    {
        float modifier = 1f;

        switch(BattleController.BC.Enemy.enemyResistances[type])
        {
            case 0:
                StartCoroutine(enemySway.StartSway(4f, 8f, 1f));
                modifier = 1.5f;
                break;
            case 1:
                modifier = 1f;
                break;
            case 2:
                StartCoroutine(enemyShake.ShakeObject(.5f));
                modifier = 0.5f;
                break;
        }
        float flo = GameControl.control.numMasks;
        int math = (int)Mathf.Round((flo + 1f) * modifier);
        BattleController.BC.EnemyTakeDamage(math);
        yield return new WaitForSeconds(1f);
        if (BattleController.BC.Enemy.enemyHealth <= 0)
        {
            yield return StartCoroutine(BattleController.BC.ResolveCombat());
        }
        else
        {
            StartCoroutine(BattleController.BC.EndTurnPlayer());
        }
    }
}
