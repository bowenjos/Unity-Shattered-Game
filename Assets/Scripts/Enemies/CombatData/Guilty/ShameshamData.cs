using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShameshamData : EnemyCombatController
{
    // Start is called before the first frame update
    void Awake()
    {
        introDialogue = "Shamesham is here to throw down.";
        outroDialogue = "Shamesham lays down for a nice nap.";
        descriptionDialogue = "Wouldn't you like to just lay down and sleep for a while...";
        talkDialogue = new string[1];
        talkDialogue[0] = "You tell Shamesham you're not interested in taking a nap. It respects that.";

        hugDialogue = new string[2];
        hugDialogue[0] = "You give Shamesham a squeeze, it's not sure how to process that.";
        hugDialogue[1] = "You hold Shamesham close. Shamesham doesn't hate it.";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "You tell Shamesham it looks like a very comfy pillow.";
        affirmDialogue[1] = "You give Shamesham a thumbs up. It stares back, confused.";

        sitDialogue = new string[1];
        sitDialogue[0] = "You lay your head down on Shamesham but they try to suffocate you.";

        actDialogue = new string[1];
        actDialogue[0] = "You fluff up Shamesham, they don't appreciate it one bit."; 

        giftDialogue = new string[2];
        giftDialogue[0] = "You present Shamesham with a new cover. If they had eyes they'd be hearts.";
        giftDialogue[1] = "Out of your pocket you pull the finest fabric softner. Shamesham is gaga.";

        playerTurnIdle = new string[3];
        playerTurnIdle[0] = "Pillow fight!";
        playerTurnIdle[1] = "Shamesham fluffs up.";
        playerTurnIdle[2] = "You feel an overwhelming urge to take a nap.";

        enemyID = EnemyIDs.shamesham;
        enemyName = "Shamesham";
        fleeable = true;
        enemyHealth = 4;
        enemyHealthMax = 4;
        enemyEmotion = "Guilt";

        enemyResistances = new int[6];
        enemyResistances[0] = 0;
        enemyResistances[1] = 1;
        enemyResistances[2] = 1;
        enemyResistances[3] = 2;
        enemyResistances[4] = 2;
        enemyResistances[5] = 0;

        enemyLevel = 1;
        numAttacks = 3;

        rewardMoney = 10;
    }

    // Update is called once per frame
    public override void ResolveEnemy()
    {
        GameControl.control.EnemyData.shameshamDefeated[enemyNumber] = true;
        base.ResolveEnemy();
        //GameControl.control.MainRoom.monikaAlive = false;
    }

    public override bool CheckEnemyDead()
    {
        return GameControl.control.EnemyData.shameshamDefeated[enemyNumber];
    }

    public override IEnumerator SelectAttack()
    {
        yield return null;
    }
}
