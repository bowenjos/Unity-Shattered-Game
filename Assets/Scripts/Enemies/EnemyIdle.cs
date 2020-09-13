using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyIdle : MonoBehaviour
{
    public float speed;
    public float idleTime;

    public SpriteRenderer sprite;
    public Transform enemyTransform;
    public Animator anim;
    public Rigidbody2D rb2d;
    protected BoxCollider2D hitbox;

    public bool aggro;
    public bool moving;

    protected bool stunStart;

    protected Coroutine co;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            //Destroy(this);
        }
        
        hitbox = this.GetComponent<BoxCollider2D>();
        if (GameControl.control.stunned)
        {
            StartCoroutine(StunEnemy());
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            //Destroy(this);
        }
        */
        try {
            if (GameControl.control.stunned)
            {
                StartCoroutine(StunEnemy());
            }
        }
        catch
        {
            Debug.Log("Enemy Destroyed Already");
        }
    }

    protected IEnumerator StunEnemy()
    {
        Debug.Log(this.name + " Stunned!");
        hitbox.enabled = false;
        bool spriteOn = true;
        for (int i = 0; i < 20; i++)
        {
            if (spriteOn)
            {
                sprite.color = new Color(1f, 1f, 1f, 0f);
            }
            else
            {
                sprite.color = new Color(1f, 1f, 1f, 1f);
            }
            spriteOn = !spriteOn;
            yield return new WaitForSeconds(0.2f);
        }
        sprite.color = new Color(1f, 1f, 1f, 1f);
        GameControl.control.stunned = false;
        hitbox.enabled = true;
    }

}
