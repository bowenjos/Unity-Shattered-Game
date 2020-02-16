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
        talkDialogue[0] = "You talk to the tar mass. It does not respond, and yet you have an impression you're getting through.";

        hugDialogue = new string[2];
        hugDialogue[0] = "Somehow, touching the giant oozing mass doesn't seem like one of your better ideas.";
        hugDialogue[1] = "You're not touching that.";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "You stand firm, and tell Tar that it's going to be okay.";
        affirmDialogue[0] = "You tell Tar that things are messy, but the first step is to do the right thing.";
        affirmDialogue[0] = "You whisper something to Tar and the spreading sludge recedes just a bit.";

        sitDialogue = new string[1];
        sitDialogue[0] = "You pause for a moment with Tar, but the gunk sticks to your shoe and you're forced to retreat.";

        actDialogue = new string[1];
        actDialogue[0] = "There is nothing you can do for this pile of goo.";

        giftDialogue = new string[2];
        giftDialogue[0] = "You present tar with a gift, but it is consumed in the folds of the slime.";
        giftDialogue[1] = "Tar does not appreciate your gift, instead melting it within it's all consuming mass.";

        playerTurnIdle = new string[3];
        playerTurnIdle[0] = "You feel something crawling up your leg.";
        playerTurnIdle[1] = "Your shoes are stuck to the floor";
        playerTurnIdle[2] = "Squelches fill the air.";

        enemyName = "Tar";
        fleeable = false;
        enemyHealth = 30;
        enemyHealthMax = 30;
        enemyEmotion = "Anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 2;
        enemyResistances[2] = 1;
        enemyResistances[3] = 2;
        enemyResistances[4] = 2;
        enemyResistances[5] = 2;

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
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(CircleAttack());
        yield return new WaitForSeconds(1f);

    }

    public IEnumerator CircleAttack()
    {
        yield return MoveToPoint(4, 4, .2f);
        yield return new WaitForSeconds(1f);
        float speed = 1f;
        for (int i = 1; i < 13; i++)
        {
            TeleportToSetpoint(SetPoints[i]);
            SpawnDefaultProjectile(speed);
            speed += 0.25f;
        }
        TeleportToPoint(4, 4);
    }
}
