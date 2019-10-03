using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerflesData : EnemyCombatController
{
    // Start is called before the first frame update
    void Awake()
    {
        introDialogue = "Fresnetic shines their light in your eyes.";
        outroDialogue = "Fresnetic light stops flickering.";
        talkDialogue = new string[3];
        talkDialogue[0] = "You chat with Fresnetic about stagetech.";
        talkDialogue[1] = "You tell Fresnetic about a time you dressed up as a penguin for a school play once.";
        talkDialogue[2] = "Fresnetic flashes a message to you in morse code.";

        hugDialogue = new string[2];
        hugDialogue[0] = "You try to give Fresnetic a pat on the head, but they're incredibly hot and burn your hand.";
        hugDialogue[1] = "As you make a move to touch Fresnetic, they instinctively begin shining the light in your face.";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "You tell Fresnetic that their lighting skills are top notch. They don't believe you, but appreciate the sentiment.";
        affirmDialogue[1] = "Fresnetic shines their light on some objects and looks to you. You give them a thumbs up.";

        sitDialogue = new string[2];
        sitDialogue[0] = "";
        sitDialogue[1] = "";

        actDialogue = new string[1];
        actDialogue[0] = "You untangle Fresnetic's cables, they sigh a huge sigh of relief, well as much as a light fixture can.";

        giftDialogue = new string[1];
        giftDialogue[0] = "";

        playerTurnIdle = new string[5];
        playerTurnIdle[0] = "Is it hot in here? Or is it just the 1 kilowatt light beaming down on you.";
        playerTurnIdle[1] = "The light shines directly in your face, it's a little bit distracting.";
        playerTurnIdle[2] = "You close your eyes but the light sears blue through your eyelids.";
        playerTurnIdle[3] = "Fresnetic flickers silently.";
        playerTurnIdle[4] = "";

        enemyHealth = 4;
        enemyHealthMax = 4;
        enemyEmotion = "anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 2;
        enemyResistances[2] = 1;
        enemyResistances[3] = 1;
        enemyResistances[4] = 0;
        enemyResistances[5] = 1;

        enemyLevel = 1;
        numAttacks = 1;

        rewardMoney = 10;
    }

    public override void ResolveEnemy()
    {
        GameControl.control.EnemyData.fresneticDefeated[enemyNumber] = true;
        base.ResolveEnemy();
        //GameControl.control.MainRoom.monikaAlive = false;
    }

    public override bool CheckEnemy()
    {
        return GameControl.control.EnemyData.fresneticDefeated[enemyNumber];
    }

    // Update is called once per frame
    public override IEnumerator SelectAttack()
    {
        numAttacks = 1;
        int rand = Random.Range(0, numAttacks);
        switch (rand)
        {
            case 0:
                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(SpawnLaserProjectile(.3f, 30));
                yield return MoveToSetpoint(SetPoints[2], .2f);
                yield return StartCoroutine(SpawnLaserProjectile(.3f, 30));
                yield return MoveToSetpoint(SetPoints[3], .2f);
                yield return StartCoroutine(SpawnLaserProjectile(.3f, 30));
                yield return MoveToSetpoint(SetPoints[4], .2f);
                yield return StartCoroutine(SpawnLaserProjectile(.3f, 30));
                yield return MoveToSetpoint(SetPoints[5], .2f);
                yield return StartCoroutine(SpawnLaserProjectile(.3f, 30));
                yield return MoveToSetpoint(SetPoints[6], .2f);
                yield return StartCoroutine(SpawnLaserProjectile(.3f, 30));
                yield return MoveToSetpoint(SetPoints[9], .2f);
                yield return StartCoroutine(SpawnLaserProjectile(.3f, 30));

                yield return new WaitForSeconds(.1f);
                break;
        }

    }
}
