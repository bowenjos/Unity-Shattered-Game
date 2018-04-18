using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HelpMenuControl : MonoBehaviour {

    Button[] buttons;
    BattleFlow bf;

    // Use this for initialization
    void Start () {
        buttons = this.GetComponentsInChildren<Button>();
        bf = GameObject.Find("BattleControl").GetComponent<BattleFlow>();
    }
	
	// Update is called once per frame
	void Update () {
        switch (bf.enemyEmotion)
        {
            case "empty":
                ChangeButton(buttons[0], "TALK", Talk, true);
                ChangeButton(buttons[1], "SELFCARE", SelfCare, true);
                ChangeButton(buttons[2], "PASSION", Passion, true);
                ChangeButton(buttons[3], "INSPIRE", Inspire, true);
                ChangeButton(buttons[4], "", Void, false);
                ChangeButton(buttons[5], "", Void, false);
                ChangeButton(buttons[6], "", Void, false);
                ChangeButton(buttons[7], "", Void, false);
                ChangeButton(buttons[8], "", Void, false);
                ChangeButton(buttons[9], "", Void, false);
                break;
        }
    }

    void ChangeButton(Button button, string newText, UnityAction newFunction, bool active)
    {
        button.gameObject.SetActive(active);
        button.GetComponentInChildren<Text>().text = newText;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(newFunction);
    }

    void Talk()
    {
        if(bf.enemyHealth - ((10) * bf.playerDamageModifier) < 0)
        {
            bf.enemyHealth = 0;
        }
        else
        {
            bf.enemyHealth -= ((10) * bf.playerDamageModifier);
        }
        bf.playerAttackType = 1;
        StartCoroutine(bf.EndTurnPlayer());
    }

    void SelfCare()
    {
        if(GameControl.control.health + 10 >= GameControl.control.maxHealth)
        {
            GameControl.control.health = GameControl.control.maxHealth;
        }
        else
        {
            GameControl.control.health += (10);
        }
        bf.enemyDamageModifier = 0.5f;
        bf.enemyDamageModTurnCounter = 1;
        bf.playerAttackType = 2;
        StartCoroutine(bf.EndTurnPlayer());
    }

    void Passion()
    {
        bf.playerDamageModifier = 2;
        bf.playerDamageModTurnCounter = 3;
        bf.playerAttackType = 3;
        StartCoroutine(bf.EndTurnPlayer());
    }

    void Inspire()
    {
        bf.enemyDamageModifier = 0.5f;
        bf.enemyDamageModTurnCounter = 3;
        bf.playerAttackType = 4;
        StartCoroutine(bf.EndTurnPlayer());
    }

    void EmoAttack(int type)
    {
        if (bf.enemyHealth - ((10) * bf.playerDamageModifier * bf.enemyResistences[bf.enemyLevelState,type]) < 0)
        {
            bf.enemyHealth = 0;
        }
        else
        {
            bf.enemyHealth -= ((10) * bf.playerDamageModifier * bf.enemyResistences[bf.enemyLevelState,type]);
        }
        bf.playerAttackType = 1;
        StartCoroutine(bf.EndTurnPlayer());
    }

    void Calm()
    {
        EmoAttack(0);
    }

    void Void()
    {
        Debug.Log("You shouldn't be able to run this");
    }
}
