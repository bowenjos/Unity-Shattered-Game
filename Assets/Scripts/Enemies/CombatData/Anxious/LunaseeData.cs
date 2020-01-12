using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaseeData : EnemyCombatController
{

    public GameObject FullPrefab;
    public GameObject WanePrefab;
    public GameObject HalfPrefab;
    public GameObject CrescentPrefab;

    int moonPosition;

    // Start is called before the first frame update
    void Awake()
    {
        moonPosition = 12;

        introDialogue = "Lunasee swings in.";
        outroDialogue = "A spirit leaves the prop causing it to fall to the ground softly.";
        descriptionDialogue = "Just your average stage prop turned possessed object.";

        talkDialogue = new string[2];
        talkDialogue[0] = "You talk to Lunasee. It's a good listener, but doesn't have much to say.";
        talkDialogue[1] = "Expressing your interests is hard, but theres something about the way that Lunasee looks at you that makes it easy.";

        hugDialogue = new string[1];
        hugDialogue[0] = "You hug the moon. Your arms don't quite reach, but everyone knows it's the thought that counts.";

        affirmDialogue = new string[2];
        affirmDialogue[0] = "You share a glance, and nothing needs to be said. A single tear roles down Lunasee's wooden face.";
        affirmDialogue[1] = "You give Lunasee a thumbs up, and they swing joyously.";

        sitDialogue = new string[1];
        sitDialogue[0] = "You attempt to sit in Lunasee's crest, it does not go over well.";

        actDialogue = new string[2];
        actDialogue[0] = "You give Lunasee a fresh coat of paint.";
        actDialogue[1] = "You tighten the rope that fastens Lunasee to the ceiling.";

        giftDialogue = new string[1];
        giftDialogue[0] = "You struggle to think of something a cutout of a moon could possibly need.";

        playerTurnIdle = new string[4];
        playerTurnIdle[0] = "Lunasee swings from side to side.";
        playerTurnIdle[1] = "You feel like maybe something is watching you...";
        playerTurnIdle[2] = "It stares straight through you.";
        playerTurnIdle[3] = "I see you.";

        enemyName = "Lunasee";
        fleeable = true;
        enemyHealth = 6;
        enemyHealthMax = 6;
        enemyEmotion = "Anxiety";

        enemyResistances = new int[6];
        enemyResistances[0] = 1;
        enemyResistances[1] = 1;
        enemyResistances[2] = 0;
        enemyResistances[3] = 2;
        enemyResistances[4] = 1;
        enemyResistances[5] = 2;

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

    public override bool CheckEnemyDead()
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
                int rand2 = Random.Range(1, 13);
                int coinflip = Random.Range(0, 2);
                yield return MoveToSetpoint(SetPoints[moonPosition], 0f);
                if (coinflip == 0)
                {
                    
                    yield return MoveAroundLeft(rand2, moonPosition, .3f);
                    moonPosition = rand2;
                }
                else
                {
                    yield return MoveAroundRight(rand2, moonPosition, .3f);
                    moonPosition = rand2;
                }
                
                yield return new WaitForSeconds(1.5f);
                SpawnSpecialNormalProjectile(1f, CrescentPrefab);
                yield return new WaitForSeconds(.4f);
                SpawnSpecialNormalProjectile(1f, HalfPrefab);
                yield return new WaitForSeconds(.4f);
                SpawnSpecialNormalProjectile(1f, WanePrefab);
                yield return new WaitForSeconds(.4f);
                SpawnSpecialNormalProjectile(1f, FullPrefab);
                yield return new WaitForSeconds(.4f);
                attack = SpawnSpecialNormalProjectile(1f, WanePrefab);
                attack.GetComponent<SpriteRenderer>().flipX = true;
                yield return new WaitForSeconds(.4f);
                attack = SpawnSpecialNormalProjectile(1f, HalfPrefab);
                attack.GetComponent<SpriteRenderer>().flipX = true;
                yield return new WaitForSeconds(.4f);
                attack = SpawnSpecialNormalProjectile(1f, CrescentPrefab);
                attack.GetComponent<SpriteRenderer>().flipX = true;
                yield return new WaitForSeconds(.4f);
                attack = SpawnSpecialNormalProjectile(1f, FullPrefab);
                attack.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f, 1f);
                break;
        }

    }
}
