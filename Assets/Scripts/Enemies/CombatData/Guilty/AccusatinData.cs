using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccusatinData : EnemyCombatController
{
    // Start is called before the first frame update
    void Awake()
    {
        introDialogue = "Accusatin floats your way";
        outroDialogue = "Accusatin floats towards the heavens, but gets stuck on the ceiling.";
        descriptionDialogue = "This is what happens when you don't hang your clothes up.";
        talkDialogue = new string[3];
        talkDialogue[0] = "";

        sitDialogue = new string[2];
        sitDialogue[0] = "";

        hugDialogue = new string[2];
        hugDialogue[0] = "";

        actDialogue = new string[1];
        actDialogue[0] = "You hand wash Accusatin's... body? They appreciate it.";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "";

        giftDialogue = new string[1];
        giftDialogue[0] = "";

        playerTurnIdle = new string[3];
        playerTurnIdle[0] = "Accusatin does some photoshoot poses.";
        playerTurnIdle[1] = "Accusatin just begs to be cleaned.";
        playerTurnIdle[2] = "Hand wash only!";

        enemyID = EnemyIDs.accusatin;
        enemyName = "Accusatin";
        fleeable = true;
        enemyHealth = 4;
        enemyHealthMax = 4;
        enemyEmotion = "Guilt";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 1;
        enemyResistances[2] = 1;
        enemyResistances[3] = 1;
        enemyResistances[4] = 0;
        enemyResistances[5] = 1;

        enemyLevel = 1;
        numAttacks = 3;

        rewardMoney = 10;
    }

    // Update is called once per frame
    public override void ResolveEnemy()
    {
        GameControl.control.EnemyData.accusatinDefeated[enemyNumber] = true;
        base.ResolveEnemy();
        //GameControl.control.MainRoom.monikaAlive = false;
    }

    public override bool CheckEnemyDead()
    {
        return GameControl.control.EnemyData.accusatinDefeated[enemyNumber];
    }

    public override IEnumerator SelectAttack()
    {
        yield return null;
    }
}
