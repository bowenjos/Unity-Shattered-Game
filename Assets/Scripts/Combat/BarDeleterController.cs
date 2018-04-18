using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarDeleterController : MonoBehaviour {

    protected ContactFilter2D contactFilter;
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);
    protected Collider2D barCollider;

    public GameObject BattleControl;

    

    // Use this for initialization
    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "SingleBarPF(Clone)" || col.gameObject.name == "DoubleBarPF(Clone)")
        {
            if(col.gameObject.name == "DoubleBarPF(Clone)")
            {
                BattleControl.GetComponent<BattleFlow>().EndTurnEnemy();
            }
            Destroy(col.gameObject);
        }
    }
}
