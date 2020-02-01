using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarData : EnemyCombatController
{
    // Start is called before the first frame update
    void Awake()
    {
        introDialogue = "Tar oozes close.";
        outroDialogue = "Tar melts away into the floorboards.";
        descriptionDialogue = "A mass of despair and sadness.";

        talkDialogue = new string[1];
        talkDialogue[0] = "";

        hugDialogue = new string[1];
        hugDialogue[0] = "";

        affirmDialogue = new string[1];
        affirmDialogue[0] = "";

        sitDialogue = new string[1];
        sitDialogue[0] = "";

        actDialogue = new string[1];
        actDialogue[0] = "";

        giftDialogue = new string[1];
        giftDialogue[0] = "";

        playerTurnIdle = new string[1];
        playerTurnIdle[0] = "You feel something crawling up your leg.";

        enemyName = "Tar";
        fleeable = false;
        enemyHealth = 30;
        enemyHealthMax = 30;
        enemyEmotion = "Anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 2;
        enemyResistances[2] = 1;
        enemyResistances[3] = 1;
        enemyResistances[4] = 0;
        enemyResistances[5] = 1;

        enemyLevel = 3;
        numAttacks = 1;

        rewardMoney = 100;
    }

    public override void ResolveEnemy()
    {
        GameControl.control.EnemyData.tarDefeated = true;
        base.ResolveEnemy();
    }

    public override bool CheckEnemyDead()
    {
        return GameControl.control.EnemyData.tarDefeated;
    }

    public override IEnumerator SelectAttack()
    {
        numAttacks = 1;
        yield return new WaitForSeconds(3f);

    }
}
