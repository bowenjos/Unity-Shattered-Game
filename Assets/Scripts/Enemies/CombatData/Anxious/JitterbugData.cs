﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JitterbugData : EnemyCombatController
{
    // Start is called before the first frame update
    public GameObject UpPrefab;
    public GameObject RightPrefab;
    public GameObject DownPrefab;
    public GameObject LeftPrefab;
    // Update is called once per frame

    void Awake()
    {
        introDialogue = "Jitterbug jitters close.";
        outroDialogue = "Jitterbug stops jittering.";
        descriptionDialogue = "A nervous friend who just can't stop shaking.";
        talkDialogue = new string[3];
        talkDialogue[0] = "You open your mouth to speak, but Jitterbug lets out a loud cricket before you can say anything.";
        talkDialogue[1] = "You ask them how they're doing, and instead of responding they just scream.";
        talkDialogue[2] = "Jitterbug vibrates so loudly you can't even hear yourself think.";

        sitDialogue = new string[2];
        sitDialogue[0] = "You can feel your calmness, it resonates through the both of you. It could just be the jittering though.";
        sitDialogue[1] = "As soon as your butt hits the ground jitterbug stops and stares at you, turning their head to the side.";

        hugDialogue = new string[2];
        hugDialogue[0] = "You're both vibrating now, but it's not entirely unpleasant. But definitly at least a little bit.";
        hugDialogue[1] = "You like bugs, but maybe not that much.";

        actDialogue = new string[1];
        actDialogue[0] = "You offer to get Jitterbug a blanket because they look cold, but they respectfully decline.";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "You tell Jitterbug they'd make a great dancer. They blush.";
        affirmDialogue[1] = "Jitterbug busts a move. You clap.";

        giftDialogue = new string[1];
        giftDialogue[0] = "You give Jitterbug some new dance shoes, but mostly they just look embarrassed.";

        playerTurnIdle = new string[5];
        playerTurnIdle[0] = "Jitterbug taps their feet anxiously.";
        playerTurnIdle[1] = "You and Jitterbug stare intently at each other.";
        playerTurnIdle[2] = "For a brief moment you feel like you can tell Jitterbug is smiling just a little under their cover.";
        playerTurnIdle[3] = "Jitterbug vibrates at mach speeds.";
        playerTurnIdle[4] = "Jitterbug does the worm, despite very clearly not being a worm.";

        enemyID = EnemyIDs.jitterbug;
        enemyName = "Jitterbug";
        fleeable = true;
        enemyHealth = 4;
        enemyHealthMax = 4;
        enemyEmotion = "Anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 2;
        enemyResistances[1] = 1;
        enemyResistances[2] = 0;
        enemyResistances[3] = 0;
        enemyResistances[4] = 1;
        enemyResistances[5] = 1;

        enemyLevel = 1;
        numAttacks = 3;

        rewardMoney = 10;
    }


    public override void ResolveEnemy()
    {
        GameControl.control.EnemyData.jitterbugDefeated[enemyNumber] = true;
        base.ResolveEnemy();
        //GameControl.control.MainRoom.monikaAlive = false;
    }

    public override bool CheckEnemyDead()
    {
        return GameControl.control.EnemyData.jitterbugDefeated[enemyNumber];
    }

    public override IEnumerator SelectAttack()
    {
        numAttacks = 5;
        int rand = Random.Range(0, numAttacks);
        switch (rand)
        {
            case 0:
                rand = Random.Range(3, 7);
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < rand; i++)
                {
                    int randtwo = Random.Range(0, 3);
                    switch (randtwo)
                    {
                        case 0:
                            yield return MoveToSetpoint(SetPoints[1], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, RightPrefab);
                            break;
                        case 1:
                            yield return MoveToSetpoint(SetPoints[12], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, UpPrefab);
                            break;
                        case 2:
                            yield return MoveToSetpoint(SetPoints[11], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, LeftPrefab);
                            break;

                    }
                    yield return new WaitForSeconds(1f);
                }
                break;
            case 1:
                yield return MoveToSetpoint(SetPoints[12], .1f);
                yield return MoveAroundRight(3, 12, .3f);
                SpawnSpecialNormalProjectile(2f, RightPrefab);
                yield return MoveAroundRight(6, 3, .3f);
                SpawnSpecialNormalProjectile(2f, DownPrefab);
                yield return MoveAroundRight(9, 6, .3f);
                SpawnSpecialNormalProjectile(2f, LeftPrefab);
                yield return MoveAroundRight(12, 9, .3f);
                SpawnSpecialNormalProjectile(2f, UpPrefab);
                break;
            case 2:
                rand = Random.Range(3, 7);
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < rand; i++)
                {
                    int randtwo = Random.Range(0, 3);
                    switch (randtwo)
                    {
                        case 0:
                            yield return MoveToSetpoint(SetPoints[3], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, RightPrefab);
                            break;
                        case 1:
                            yield return MoveToSetpoint(SetPoints[2], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, UpPrefab);
                            break;
                        case 2:
                            yield return MoveToSetpoint(SetPoints[4], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, DownPrefab);
                            break;

                    }
                    yield return new WaitForSeconds(1f);
                }
                break;
            case 3:
                rand = Random.Range(3, 7);
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < rand; i++)
                {
                    int randtwo = Random.Range(0, 3);
                    switch (randtwo)
                    {
                        case 0:
                            yield return MoveToSetpoint(SetPoints[6], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, DownPrefab);
                            break;
                        case 1:
                            yield return MoveToSetpoint(SetPoints[5], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, RightPrefab);
                            break;
                        case 2:
                            yield return MoveToSetpoint(SetPoints[7], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, LeftPrefab);
                            break;

                    }
                    yield return new WaitForSeconds(1f);
                }
                break;
            case 4:
                rand = Random.Range(3, 7);
                yield return new WaitForSeconds(1f);
                for (int i = 0; i < rand; i++)
                {
                    int randtwo = Random.Range(0, 3);
                    switch (randtwo)
                    {
                        case 0:
                            yield return MoveToSetpoint(SetPoints[9], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, LeftPrefab);
                            break;
                        case 1:
                            yield return MoveToSetpoint(SetPoints[8], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, DownPrefab);
                            break;
                        case 2:
                            yield return MoveToSetpoint(SetPoints[10], .2f);
                            yield return new WaitForSeconds(.2f);
                            SpawnSpecialNormalProjectile(2.5f, UpPrefab);
                            break;

                    }
                    yield return new WaitForSeconds(1f);
                }
                break;
        }

    }
}
