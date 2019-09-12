using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDemoMonika : EnemyCombatControl {



    // Use this for initialization
    void Start()
    {
        CheckThis();
        dialogueRespondTalk = new string[2][];
        dialogueRespondTalk[0] = new string[] { "Just Monika Just Monika Just Monika", "...", "I can't turn back now" };
        dialogueRespondTalk[1] = new string[] { "I made my choice, have you?", "Every day, I dream a dream where I can be with you", "Please just accept this" };

        dialogueIdle = new string[2][];
        dialogueIdle[0] = new string[] { "Monika looks tired", "Thunder rolls through the house", "This ends here" };
        dialogueIdle[1] = new string[] { "Monika is just getting started", "You feel uneasy", "Monika seems determined", "Just Monika" };

        dialoguePlayerTalk = new string[2][];
        dialoguePlayerTalk[0] = new string[] { "You tell Monika to give it up", "You tell Monika to D I E" };
        dialoguePlayerTalk[1] = new string[] { "You explain you would romance Monika if she was an option", "You try to tell Monika that she should just program her own route", "You try to calm Monika" };

        dialogueStateChange = new string[] { "You're right. This was foolish. I should accept this and move on.", "You might be right..." };
    }

    public override IEnumerator Attack()
    {
        hasAttacked = true;
        int random = Random.Range(0, numberOfAttacks);
        int randomD = Random.Range(0, dialogueRespondTalk[enemyLevelState].Length);

        //yield return StartCoroutine(Talk.GetComponent<TalkControl>().Dialogue(dialogueRespondTalk[bf.enemyLevelState][randomD])); 

        switch (random)
        {
            case 0:
                yield return StartCoroutine(attack0());
                break;
            case 1:
                yield return StartCoroutine(attack1());
                break;
            default:
                break;
        }

        yield return null;
    }

    IEnumerator attack0()
    {
        eHC.SetSpeed(150F);
        eHC.BeginAttack();
        yield return StartCoroutine(eHC.Left(0.2f));
        //yield return new WaitForSeconds(0.2f);
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Left(0.2f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Left(0.2f));
        eHC.PauseAttack();
        yield return StartCoroutine(eHC.Right(0.2f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Right(0.2f));
        eHC.ShootNote();
        yield return new WaitForSeconds(0.2f);
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Right(0.2f));
        eHC.ShootNote();
        yield return new WaitForSeconds(0.2f);
        eHC.EndAttack();
        yield return null;
    }

    IEnumerator attack1()
    {
        eHC.SetSpeed(250F);
        eHC.BeginAttack();
        yield return StartCoroutine(eHC.Left(0.1f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Right(0.2f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Left(0.2f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Right(0.2f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Left(0.2f));
        eHC.PauseAttack();
        yield return StartCoroutine(eHC.Right(0.2f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Left(0.2f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Right(0.2f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Left(0.2f));
        eHC.ShootNote();
        yield return StartCoroutine(eHC.Right(0.1f));
        eHC.EndAttack();
        yield return null;
    }

    public override void CheckThis()
    {
        if (GameControl.control.frozen)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public override void DestroyThis()
    {
        //GameControl.control.MainRoom.monikaAlive = false;
        GameObject.Destroy(this.gameObject);
    }


}

