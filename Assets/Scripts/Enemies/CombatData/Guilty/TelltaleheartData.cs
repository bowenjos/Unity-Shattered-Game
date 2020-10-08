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
        talkDialogue[0] = "You strike up a conversation. Tell-Tale-Heart tells a novel about their youth.";
        talkDialogue[1] = "You say hello. Tell-Tale-Heart calls you a 'such a nice young lad'.";
        talkDialogue[2] = "'Finally a youth that respects their elders' says Tell-Tale-Heart.";

        hugDialogue = new string[2];
        hugDialogue[0] = "You try to wrap your arms around the clock, but it's too big.";
        hugDialogue[1] = "You consider a strategy for hugging this clock, but get nowhere.";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "You tell Tell-Tale-Heart it's a good clock. It says it knows.";
        affirmDialogue[1] = "You give Tell-Tale-Heart a thumbs up. It maybe liked that, maybe not, hard to tell, it's a clock.";

        sitDialogue = new string[2];
        sitDialogue[0] = "You sit in silence, except it's not silence, as the tick tick ticking continues.";
        sitDialogue[1] = "Your head is pounding and ringing.";

        actDialogue = new string[1];
        actDialogue[0] = "You clean down Tell-Tale-Heart's outside, but it's concerned the whole time for its wood finish.";

        giftDialogue = new string[1];
        giftDialogue[0] = "You give Tell-Tale-Heart some new clockwork, but it's attached to it's current parts.";

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
        enemyResistances[0] = 0;
        enemyResistances[1] = 2;
        enemyResistances[2] = 1;
        enemyResistances[3] = 2;
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
