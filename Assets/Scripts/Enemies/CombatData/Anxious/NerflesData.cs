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
        descriptionDialogue = "Don't state directly into the light.";
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
        sitDialogue[0] = "You stare at Fresnetic. Fresnetic shines their light on you. It's a moment in time for sure.";
        sitDialogue[1] = "You sit... Nothing interesting happens as Fresnetic continues to flicker their light at you.";

        actDialogue = new string[2];
        actDialogue[0] = "You untangle Fresnetic's cables, they sigh a huge sigh of relief, well as much as a light fixture can.";
        actDialogue[1] = "After polishing Fresnetic's lens, they seem a bit more relaxed.";
        actDialogue[2] = "A fresh dusting seemed to be all Fresnetic needed to put their mind at ease.";

        giftDialogue = new string[2];
        giftDialogue[0] = "You attempt to give Fresnetic a gift, however they do not have any arms with which to lift it.";
        giftDialogue[1] = "You hold a gift out for Fresnetic to take... But they don't, because they have no hands.";

        playerTurnIdle = new string[5];
        playerTurnIdle[0] = "Is it hot in here? Or is it just the 1 kilowatt light beaming down on you.";
        playerTurnIdle[1] = "The light shines directly in your face, it's a little bit distracting.";
        playerTurnIdle[2] = "You close your eyes but the light sears blue through your eyelids.";
        playerTurnIdle[3] = "Fresnetic flickers silently.";
        playerTurnIdle[4] = "";

        enemyName = "Fresnetic";
        fleeable = true;
        enemyHealth = 4;
        enemyHealthMax = 4;
        enemyEmotion = "Anxiety";

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

    public override bool CheckEnemyDead()
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


                for (int i = 0; i < 5; i++)
                {
                    int rand2 = Random.Range(1, 13);
                    yield return MoveToSetpoint(SetPoints[rand2], .2f);
                    yield return new WaitForSeconds(1f);
                    yield return StartCoroutine(SpawnLaserProjectile(.3f, 10));
                    
                }
                yield return new WaitForSeconds(.1f);
                break;
        }

    }
}
