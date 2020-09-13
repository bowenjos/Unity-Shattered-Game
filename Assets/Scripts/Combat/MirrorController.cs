using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MirrorController : MonoBehaviour {

    public float damageValue;

    public GameObject bf;

    protected ContactFilter2D contactFilter;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected Collider2D mirrorCollider;

    public Animator mirrorAnimator;
    public int Affliction;
    public bool Afflicted;

    public Sprite[] dieSprites;
    private AudioSource takeDamage;
    private bool dying;

    // Use this for initialization
    void Start () {
        takeDamage = this.GetComponent<AudioSource>();
        dying = false;
        Affliction = 0;
        mirrorAnimator.SetBool("Dying", false);
    }
	
	// Update is called once per frame
	void Update () {
        Affliction = BattleController.BC.playerAffliction;

        mirrorAnimator.SetInteger("Affliction", Affliction);

        if(GameControl.control.health >= GameControl.control.maxHealth * 0.8f)
        {
            mirrorAnimator.SetInteger("DamageStatus", 0);
        }
        else if(GameControl.control.health >= GameControl.control.maxHealth * 0.6f)
        {
            mirrorAnimator.SetInteger("DamageStatus", 1);
        }
        else if (GameControl.control.health >= GameControl.control.maxHealth * 0.4f)
        {
            mirrorAnimator.SetInteger("DamageStatus", 2);
        }
        else if (GameControl.control.health >= GameControl.control.maxHealth * 0.2f)
        {
            mirrorAnimator.SetInteger("DamageStatus", 3);
        }
        else if (GameControl.control.health >= 0f && !dying)
        {
            mirrorAnimator.SetInteger("DamageStatus", 4);
        }
        if (GameControl.control.health <= 0 && !dying)
        {
            dying = true;

            StopAllCoroutines();
            StartCoroutine(Die());
        }
	}

    IEnumerator Die()
    {
        BattleController.BC.DestroyPlayer();

        mirrorAnimator.SetInteger("DamageStatus", 4);
        yield return new WaitForEndOfFrame();
        mirrorAnimator.SetBool("Dying", true);
        GameObject.Find("BattleJukeBox").GetComponent<AudioSource>().Stop();
        GameObject.Find("mirrorText").GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);
        GameObject.Find("mirrorBorderText").GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);
        takeDamage.Play();
        yield return new WaitForSeconds(0.5f);
        takeDamage.Play();
        yield return new WaitForSeconds(0.5f);
        takeDamage.Play();
        yield return new WaitForSeconds(0.5f);
        takeDamage.Play();
        yield return new WaitForSeconds(0.5f);

        GameObject.Find("TransitionControl(Clone)").GetComponent<TransitionController>().transitionNow();
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        Debug.Log("You have Shattered");
        yield return null;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Something entered");
        takeDamage.Play();
        GameControl.control.health -= col.gameObject.GetComponent<DefaultAttack>().damageValue;
        this.GetComponent<Shake>().StartShake(.05f);
        BattleController.BC.healingTouched = false;

        //Check to see if this attack has an affliction associated, if so, attempt to afflict.
        if (col.gameObject.GetComponent<AfflictionMod>() != null)
        {
            col.gameObject.GetComponent<AfflictionMod>().Afflict();
        }

        Destroy(col.gameObject);
    }

    
}
