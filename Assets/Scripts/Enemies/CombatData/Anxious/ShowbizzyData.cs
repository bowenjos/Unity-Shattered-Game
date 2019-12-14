using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowbizzyData : EnemyCombatController
{
    public GameObject hookPrefab;
    public GameObject ropePrefab;

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

        playerTurnIdle = new string[4];
        playerTurnIdle[0] = "Showbizzy spins their pulley wildly.";
        playerTurnIdle[1] = "Frayed rope pieces line the floor.";
        playerTurnIdle[2] = "";
        playerTurnIdle[3] = "";


        enemyName = "Showbizzy";
        fleeable = true;
        enemyHealth = 8;
        enemyHealthMax = 8;
        enemyEmotion = "Anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 0;
        enemyResistances[2] = 1;
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
        //Pick a side
        int randPos = 1;  //Random.Range(0, 4);
        int randAttack = 0; // Random.Range(0, 4);
        switch (randPos)
        {
            case 0:
                yield return MoveToSetpoint(SetPoints[12], .2f);
                break;
            case 1:
                yield return MoveToSetpoint(SetPoints[3], .2f);
                break;
            case 2:
                yield return MoveToSetpoint(SetPoints[6], .2f);
                break;
            case 3:
                yield return MoveToSetpoint(SetPoints[9], .2f);
                break;
        }

        //Spawn the hook and rope
        GameObject hook = SpawnSpecialNormalProjectile(1f, hookPrefab);
        HookAttack hookAttack = hook.GetComponent<HookAttack>();
        Instantiate(ropePrefab);
        RotateAroundOnCommand hookRotater = hook.GetComponent<RotateAroundOnCommand>();
        hookRotater._centre = GameObject.Find("EnemyAttackPhaseSprite").GetComponent<Transform>().position;
        yield return StartCoroutine(hookAttack.MoveHook(.2f, 1f));


        //pick an attack


        //perform that attack
        if (randPos == 0)
        {
            switch (randAttack)
            {
                case 0:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 4, 0f));
                    hookAttack.LaunchHook(new Vector3(.45f, -4.5f, 0f));
                    break;
                case 1:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 6, -.1f));
                    hookAttack.LaunchHook(new Vector3(1.5f, -4.5f, 0f));
                    break;
                case 2:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 6, -.1f));
                    hookAttack.LaunchHook(new Vector3(-1.5f, -4.5f, 0f));
                    break;
                case 3:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 4, 0f));
                    hookAttack.LaunchHook(new Vector3(-.45f, -4.5f, 0f));
                    break;
            }
        }
        else if(randPos == 1)
        {
            switch (randAttack)
            {
                case 0:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 5, .95f));
                    hookAttack.LaunchHook(new Vector3(-4.5f, .45f, 0f));
                    break;
                case 1:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 6, 0.9f));
                    hookAttack.LaunchHook(new Vector3(-4.5f, -1.5f, 0f));
                    break;
                case 2:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 6, -0.9f));
                    hookAttack.LaunchHook(new Vector3(-4.5f, 1.5f, 0f));
                    break;
                case 3:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 4, -.95f));
                    hookAttack.LaunchHook(new Vector3(-4.5f, -.45f, 0f));
                    break;
            }
        }
        else if(randPos == 2)
        {
            switch (randAttack)
            {
                case 0:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 5, 0f));
                    hookAttack.LaunchHook(new Vector3(-.45f, 4.5f, 0f));
                    break;
                case 1:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 7, .1f));
                    hookAttack.LaunchHook(new Vector3(-1.5f, 4.5f, 0f));
                    break;
                case 2:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 7, .1f));
                    hookAttack.LaunchHook(new Vector3(1.5f, 4.5f, 0f));
                    break;
                case 3:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 5, 0f));
                    hookAttack.LaunchHook(new Vector3(.45f, 4.5f, 0f));
                    break;
            }
        }
        else if(randPos == 3)
        {
            
        }
        


        

    }
}
