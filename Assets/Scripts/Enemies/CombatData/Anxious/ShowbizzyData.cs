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
        talkDialogue[0] = "You chat about stagetech work, Showbizzy is clearly passionate about the work.";

        sitDialogue = new string[1];
        sitDialogue[0] = "You sit with Showbizzy, but they're not interested in saying anything.";

        hugDialogue = new string[1];
        hugDialogue[0] = "They whisper in your ear \"Thank you\".";
        hugDialogue[1] = "You pat them on the back and they let out a few tears.";

        actDialogue = new string[2];
        actDialogue[0] = "You attempt to help Showbizzy with the prework, but they insist you get out of their way.";
        actDialogue[1] = "You reach for a rope, but Showbizzy slaps your hand away frantically.";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "You tell Showbizzy the show is going to be great, they yell back that you don't know that.";
        affirmDialogue[1] = "You assure Showbizzy they're doing a good job, they assure you everythign is a disaster.";

        giftDialogue = new string[1];
        giftDialogue[0] = "You present a new pulley to Showbizzy, they are impressed, but fancy pulleys didn't make a show good.";

        playerTurnIdle = new string[4];
        playerTurnIdle[0] = "Showbizzy spins their pulley wildly.";
        playerTurnIdle[1] = "Frayed rope pieces line the floor.";
        playerTurnIdle[2] = "The din of pre-show prepwork fills the room.";
        playerTurnIdle[3] = "The shadow of openning night looms over you.";


        enemyName = "Showbizzy";
        fleeable = true;
        enemyHealth = 8;
        enemyHealthMax = 8;
        enemyEmotion = "Anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 1; //Talking
        enemyResistances[1] = 0; //Physical Touch
        enemyResistances[2] = 2; //Affirmation
        enemyResistances[3] = 1; //Quality Time
        enemyResistances[4] = 2; //Acts of Charity
        enemyResistances[5] = 1; // Gifts

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
        int randPos = Random.Range(0, 3);
        int randAttack = Random.Range(0, 4);
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
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 3, 0f));
                    hookAttack.LaunchHook(new Vector3(.45f, -4.5f, 0f));
                    break;
                case 1:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 5, -.1f));
                    hookAttack.LaunchHook(new Vector3(1.5f, -4.5f, 0f));
                    break;
                case 2:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 5, -.1f));
                    hookAttack.LaunchHook(new Vector3(-1.5f, -4.5f, 0f));
                    break;
                case 3:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 3, 0f));
                    hookAttack.LaunchHook(new Vector3(-.45f, -4.5f, 0f));
                    break;
            }
        }
        else if(randPos == 1)
        {
            switch (randAttack)
            {
                case 0:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 4, -.95f));
                    hookAttack.LaunchHook(new Vector3(-4.5f, .45f, 0f));
                    break;
                case 1:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 6, -0.9f));
                    hookAttack.LaunchHook(new Vector3(-4.5f, 1.5f, 0f));
                    break;
                case 2:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 5, 0.9f));
                    hookAttack.LaunchHook(new Vector3(-4.5f, -1.5f, 0f));
                    break;
                case 3:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 3, .9f));
                    hookAttack.LaunchHook(new Vector3(-4.5f, -.45f, 0f));
                    break;
            }
        }
        else if(randPos == 2)
        {
            switch (randAttack)
            {
                case 0:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 4, 0f));
                    hookAttack.LaunchHook(new Vector3(-.45f, 4.5f, 0f));
                    break;
                case 1:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 6, .1f));
                    hookAttack.LaunchHook(new Vector3(-1.5f, 4.5f, 0f));
                    break;
                case 2:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 6, .1f));
                    hookAttack.LaunchHook(new Vector3(1.5f, 4.5f, 0f));
                    break;
                case 3:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 4, 0f));
                    hookAttack.LaunchHook(new Vector3(.45f, 4.5f, 0f));
                    break;
            }
        }
        else if(randPos == 3)
        {
            switch (randAttack)
            {
                case 0:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 4, -.95f));
                    hookAttack.LaunchHook(new Vector3(4.5f, .45f, 0f));
                    break;
                case 1:
                    yield return StartCoroutine(hookAttack.Rotate(true, 1f, 6, -0.9f));
                    hookAttack.LaunchHook(new Vector3(4.5f, 1.5f, 0f));
                    break;
                case 2:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 5, 0.9f));
                    hookAttack.LaunchHook(new Vector3(4.5f, -1.5f, 0f));
                    break;
                case 3:
                    yield return StartCoroutine(hookAttack.Rotate(false, 1f, 3, .9f));
                    hookAttack.LaunchHook(new Vector3(4.5f, -.45f, 0f));
                    break;
            }
        }
        


        

    }
}
