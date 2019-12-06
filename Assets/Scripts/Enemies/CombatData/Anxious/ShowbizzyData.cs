using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowbizzyData : EnemyCombatController
{
    // Start is called before the first frame update
    void Awake()
    {
        introDialogue = "Showbizzy dashes towards you.";
        outroDialogue = "Showbizzy nods knowingly. The show must go on.";
        descriptionDialogue = "It's very clearly openning night.";
        talkDialogue = new string[1];
        talkDialogue[0] = "";

        sitDialogue = new string[1];
        sitDialogue[0] = "";

        hugDialogue = new string[1];
        hugDialogue[0] = "They whisper in your ear \"Thank you\".";

        actDialogue = new string[1];
        actDialogue[0] = "";

        affirmDialogue = new string[1];
        affirmDialogue[0] = "";

        giftDialogue = new string[1];
        giftDialogue[0] = "";

        playerTurnIdle = new string[1];
        playerTurnIdle[0] = "";


        enemyName = "Showbizzy";
        fleeable = true;
        enemyHealth = 8;
        enemyHealthMax = 8;
        enemyEmotion = "Anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 1;
        enemyResistances[2] = 0;
        enemyResistances[3] = 1;
        enemyResistances[4] = 1;
        enemyResistances[5] = 1;

        enemyLevel = 2;
        numAttacks = 3;

        rewardMoney = 20;
    }

    public override void ResolveEnemy()
    {
        GameControl.control.EnemyData.showbizzyDefeated[enemyNumber] = true;
        base.ResolveEnemy();
        //GameControl.control.MainRoom.monikaAlive = false;
    }

    public override bool CheckEnemyDead()
    {
        return GameControl.control.EnemyData.showbizzyDefeated[enemyNumber];
    }

    public override IEnumerator SelectAttack()
    {
        numAttacks = 1;
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
                            //SpawnSpecialNormalProjectile(2.5f, RightPrefab);
                            break;
                        case 1:
                            yield return MoveToSetpoint(SetPoints[12], .2f);
                            yield return new WaitForSeconds(.2f);
                            //SpawnSpecialNormalProjectile(2.5f, UpPrefab);
                            break;
                        case 2:
                            yield return MoveToSetpoint(SetPoints[11], .2f);
                            yield return new WaitForSeconds(.2f);
                            //SpawnSpecialNormalProjectile(2.5f, LeftPrefab);
                            break;

                    }
                    yield return new WaitForSeconds(1f);
                }
                break;
        }

    }
}
