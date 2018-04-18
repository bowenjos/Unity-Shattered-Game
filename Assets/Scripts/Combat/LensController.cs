using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensController : MonoBehaviour {

    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected Rigidbody2D rb2d;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected Collider2D lensCollider;
    protected GameObject collideObject;

    float speed = 250F;
    int count;

    // Use this for initialization
    void Start () {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));

    }
	
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "TrebleClefPF(Clone)" || col.gameObject.name == "EnemyNotePF(Clone)")
        {
            Destroy(col.gameObject);
        }
    }

    void Walk(int dir, int count)
    {
        if (count == 0)
        {
            if (dir == 1)
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if (dir == 3)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
    }

    // Update is called once per frame
    void Update () {
        if(BattleFlow.playerOrEnemyPhase == 1)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            { // Walk Left
                count = rb2d.Cast(Vector2.left, contactFilter, hitBuffer, 0.01F);
                Walk(3, count);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            { // Walk Right
                count = rb2d.Cast(Vector2.right, contactFilter, hitBuffer, 0.01F);
                Walk(1, count);
            }
        }
    }
}
