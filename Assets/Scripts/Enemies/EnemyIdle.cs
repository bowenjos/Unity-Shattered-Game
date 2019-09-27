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

    public bool aggro;
    public bool moving;

    protected Coroutine co;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Encounter")
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
