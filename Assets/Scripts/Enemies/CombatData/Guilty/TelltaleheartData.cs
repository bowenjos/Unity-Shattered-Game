using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelltaleheartData : EnemyCombatController
{
    // Start is called before the first frame update
    void Awake()
    {
        introDialogue = "The tell tale ticking of a Tell-Tale-Heart ticks towards.";
        outroDialogue = "The ticking stops, or at least it's a normal clock ticking now.";
        descriptionDialogue = "A grandfather clocked moved to life by the constant beating of a heart.";
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

        playerTurnIdle = new string[5];
        playerTurnIdle[0] = "Tick. Tock. Tick. Tock.";
        playerTurnIdle[1] = "Tell-Tale-Heart chimes, and yet no clear hour is stated.";
        playerTurnIdle[2] = "The hands on Tell-Tale-Heart's clockface spin wildly.";
        playerTurnIdle[3] = "The pendalum swings. Back. Forth. Back. Forth.";
        playerTurnIdle[4] = "Tell-Tale-Heart looms over you.";

        enemyID = EnemyIDs.telltaleheart;
        enemyName = "Tell-Tale-Heart";
        fleeable = true;
        enemyHealth = 6;
        enemyHealthMax = 6;
        enemyEmotion = "Guilt";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 1;
        enemyResistances[2] = 1;
        enemyResistances[3] = 1;
        enemyResistances[4] = 1;
        enemyResistances[5] = 1;

        enemyLevel = 1;
        numAttacks = 3;

        rewardMoney = 10;
    }


    public override void ResolveEnemy()
    {
        GameControl.control.EnemyData.telltaleheartDefeated[enemyNumber] = true;
        base.ResolveEnemy();
        //GameControl.control.MainRoom.monikaAlive = false;
    }

    public override bool CheckEnemyDead()
    {
        return GameControl.control.EnemyData.telltaleheartDefeated[enemyNumber];
    }

    public override IEnumerator SelectAttack()
    {
        yield return null;
    }
}
