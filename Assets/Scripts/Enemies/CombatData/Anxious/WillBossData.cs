using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WillBossData : EnemyCombatController
{

    // Start is called before the first frame update
    void Awake()
    {
        introDialogue = "Will is barely keeping it together.";
        outroDialogue = "Will's fists clench, and then unfurl, falling to his side.";
        descriptionDialogue = "A nervous guitarist who longs to be, and is terrified of, being heard.";
        talkDialogue = new string[1];
        talkDialogue[0] = "";

        sitDialogue = new string[1];
        sitDialogue[0] = "";

        hugDialogue = new string[1];
        hugDialogue[0] = "";

        actDialogue = new string[1];
        actDialogue[0] = "";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "";

        giftDialogue = new string[1];
        giftDialogue[0] = "";

        playerTurnIdle = new string[4];
        playerTurnIdle[0] = "Tears spill out from beneath his mask.";
        playerTurnIdle[1] = "You can't see straight.";
        playerTurnIdle[2] = "Will grasps his head in pain.";
        playerTurnIdle[3] = "";

        enemyID = EnemyIDs.william;
        enemyName = "William";
        fleeable = false;
        enemyHealth = 25;
        enemyHealthMax = 25;
        enemyEmotion = "Anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 1;
        enemyResistances[2] = 0;
        enemyResistances[3] = 1;
        enemyResistances[4] = 2;
        enemyResistances[5] = 2;

        enemyLevel = 3;
        numAttacks = 3;

        rewardMoney = 100;
    }

    // Update is called once per frame
    public override void ResolveEnemy()
    {
        GameControl.control.EnemyData.willDefeated = true;
        GameControl.control.masks[0] = true;
        base.ResolveEnemy();
        //GameControl.control.MainRoom.monikaAlive = false;
    }

    public override bool CheckEnemyDead()
    {
        return GameControl.control.EnemyData.willDefeated;
    }

    public override IEnumerator SelectAttack()
    {
        yield return null;
    }
}
