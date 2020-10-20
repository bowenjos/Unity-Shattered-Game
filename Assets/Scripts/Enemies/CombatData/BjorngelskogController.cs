using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BjorngelskogController : EnemyCombatController
{
    // Start is called before the first frame update
    void Awake()
    {
        introDialogue = "Bjorn blocks your path";
        outroDialogue = "Bjorn bows to your new found conflict resolution skills.";
        descriptionDialogue = "A bodacious bear.";
        talkDialogue = new string[3];
        talkDialogue[0] = "";

        sitDialogue = new string[2];
        sitDialogue[0] = "";

        hugDialogue = new string[2];
        hugDialogue[0] = "";

        actDialogue = new string[1];
        actDialogue[0] = "";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "";

        giftDialogue = new string[1];
        giftDialogue[0] = "";

        playerTurnIdle = new string[3];
        playerTurnIdle[0] = "Bjorn waits for your action.";
        playerTurnIdle[1] = "You admire this big ol' bear.";
        playerTurnIdle[2] = "You can't NOT hug him.";

        enemyID = EnemyIDs.bjorn;
        enemyName = "Bjorn";
        fleeable = true;
        enemyHealth = 4;
        enemyHealthMax = 4;
        enemyEmotion = "Neutral";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 1;
        enemyResistances[2] = 0;
        enemyResistances[3] = 1;
        enemyResistances[4] = 1;
        enemyResistances[5] = 1;

        enemyLevel = 1;
        numAttacks = 3;

        rewardMoney = 0;
    }

    // Update is called once per frame
    public override void ResolveEnemy()
    {
        GameControl.control.tutorialComplete = true;
        base.ResolveEnemy();
        //GameControl.control.MainRoom.monikaAlive = false;
    }

    public override bool CheckEnemyDead()
    {
        return false;
    }

    public override IEnumerator SelectAttack()
    {
        yield return null;
    }

    public override IEnumerator BattleStart()
    {
        //Make this opject stop moving
        GameControl.control.Freeze();
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
        //GetComponentInChildren<EnemyAggro>().stop = true;

        //Mark this as the object to be used in the next scene (as opposed to the other objects which get deleted soon after entering and cause my huge pains)
        thisObjectOfficer = true;

        //Play combat enter sound effect
        this.GetComponent<AudioSource>().Play();
        //Fade out music
        StartCoroutine(GameObject.Find("JukeBox(Clone)").GetComponent<JukeBoxController>().PauseOut(0.4f));

        //Enter Combat Transition
        yield return StartCoroutine(GameObject.Find("TransitionControl(Clone)").GetComponent<TransitionController>().EnterCombat());
        //Set object name to "enemy" so it can be found by the Battle Controller in next scene
        this.gameObject.name = "Enemy";


        //Store data in the node
        END.enemyID = enemyID;
        END.enemyNumber = enemyNumber;
        END.enemyX = transform.position.x;
        END.enemyY = transform.position.y;

        //Load battle scene
        SceneManager.LoadScene("Encounter");
    }
}
