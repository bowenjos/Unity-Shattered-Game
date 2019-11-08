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

    public Sprite mirror1;
    public Sprite mirror2;
    public Sprite mirror3;
    public Sprite mirror4;
    public Sprite mirror5;


    public Sprite[] dieSprites;
    private AudioSource takeDamage;
    private bool dying;
    
    private SpriteRenderer mirrorSprite;

    // Use this for initialization
    void Start () {
        //contactFilter.useTriggers = false;
        //contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        takeDamage = this.GetComponent<AudioSource>();
        dying = false;
        mirrorSprite = this.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        if(GameControl.control.health >= GameControl.control.maxHealth * 0.8f)
        {
            mirrorSprite.sprite = mirror1;
        }
        else if(GameControl.control.health >= GameControl.control.maxHealth * 0.6f)
        {
            mirrorSprite.sprite = mirror2;
        }
        else if (GameControl.control.health >= GameControl.control.maxHealth * 0.4f)
        {
            mirrorSprite.sprite = mirror3;
        }
        else if (GameControl.control.health >= GameControl.control.maxHealth * 0.2f)
        {
            mirrorSprite.sprite = mirror4;
        }
        else if (GameControl.control.health >= 0f && !dying)
        {
            mirrorSprite.sprite = mirror5;
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
        //while(GameObject.Find("Attack(Clone)") != null)
        //{
        //    Destroy(GameObject.Find("Attack(Clone)"));
        //}
        GameObject.Find("BattleJukeBox").GetComponent<AudioSource>().Stop();
        GameObject.Find("mirrorText").GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);
        GameObject.Find("mirrorBorderText").GetComponent<Text>().color = new Color(1f, 1f, 1f, 0f);
        //Animate
        mirrorSprite.sprite = dieSprites[0];
        takeDamage.Play();
        yield return new WaitForSeconds(1.5f);
        for (int i = 1; i < 4; i++)
        {
            mirrorSprite.sprite = dieSprites[i];
            takeDamage.Play();
            yield return new WaitForSeconds(.5f);
        }
        
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
        Destroy(col.gameObject);
    }

    
}
