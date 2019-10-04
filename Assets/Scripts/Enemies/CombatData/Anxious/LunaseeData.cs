using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaseeData : EnemyCombatController
{

    public GameObject FullPrefab;
    public GameObject WanePrefab;
    public GameObject HalfPrefab;
    public GameObject CrescentPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        introDialogue = "I See You.";
        outroDialogue = ".";
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
        playerTurnIdle[0] = "";

        enemyHealth = 6;
        enemyHealthMax = 6;
        enemyEmotion = "anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 1;
        enemyResistances[2] = 1;
        enemyResistances[3] = 1;
        enemyResistances[4] = 1;
        enemyResistances[5] = 1;

        enemyLevel = 2;
        numAttacks = 1;

        rewardMoney = 15;
    }

    public override void ResolveEnemy()
    {
        GameControl.control.EnemyData.lunaseeDefeated[enemyNumber] = true;
        base.ResolveEnemy();
        //GameControl.control.MainRoom.monikaAlive = false;
    }

    public override bool CheckEnemy()
    {
        return GameControl.control.EnemyData.lunaseeDefeated[enemyNumber];
    }

    // Update is called once per frame
    public override IEnumerator SelectAttack()
    {
        GameObject attack;
        numAttacks = 1;
        
        int rand = Random.Range(0, numAttacks);
        switch (rand)
        {
            case 0:
                yield return new WaitForSeconds(.1f);
                SpawnSpecialNormalProjectile(1f, CrescentPrefab);
                yield return new WaitForSeconds(.2f);
                SpawnSpecialNormalProjectile(1f, HalfPrefab);
                yield return new WaitForSeconds(.2f);
                SpawnSpecialNormalProjectile(1f, WanePrefab);
                yield return new WaitForSeconds(.2f);
                SpawnSpecialNormalProjectile(1f, FullPrefab);
                yield return new WaitForSeconds(.2f);
                attack = SpawnSpecialNormalProjectile(1f, WanePrefab);
                attack.GetComponent<SpriteRenderer>().flipX = true;
                yield return new WaitForSeconds(.2f);
                attack = SpawnSpecialNormalProjectile(1f, HalfPrefab);
                attack.GetComponent<SpriteRenderer>().flipX = true;
                yield return new WaitForSeconds(.2f);
                attack = SpawnSpecialNormalProjectile(1f, CrescentPrefab);
                attack.GetComponent<SpriteRenderer>().flipX = true;
                yield return new WaitForSeconds(.2f);
                attack = SpawnSpecialNormalProjectile(1f, FullPrefab);
                attack.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f, 1f);
                break;
        }

    }
}
