using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAttack : DefaultAttack
{
    Transform attackTransform;
    Transform enemyTokenTransform;
    Transform thisTransform;
    SpriteRenderer thisSprite;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        thisSprite = GetComponent<SpriteRenderer>();
        attackTransform = GameObject.Find("Attack").GetComponent<Transform>();
        enemyTokenTransform = GameObject.Find("EnemyAttackPhaseSprite").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            thisSprite.size = new Vector2(Mathf.Sqrt(Mathf.Pow((attackTransform.position.x - enemyTokenTransform.position.x), 2f) + Mathf.Pow((attackTransform.position.y - enemyTokenTransform.position.y), 2f)) / 5f, 0.02f);
            thisTransform.position = new Vector3(enemyTokenTransform.position.x + (attackTransform.position.x - enemyTokenTransform.position.x) / 2, enemyTokenTransform.position.y + (attackTransform.position.y - enemyTokenTransform.position.y) / 2, 0);
            double value = Mathf.Atan((enemyTokenTransform.position.y - attackTransform.position.y) / (enemyTokenTransform.position.x - attackTransform.position.x)) * (180 / Mathf.PI); //* (Mathf.PI/2);
            Vector3 newAngle = new Vector3(0f, 0f, (float)value);
            Quaternion from = thisTransform.rotation;
            Quaternion to = Quaternion.Euler(newAngle);
            thisTransform.localRotation = Quaternion.Lerp(from, to, 1);
        }
        catch { Destroy(this.gameObject); }

   
    }

    public override IEnumerator Attack(float speed)
    {
        yield return null;
    }
}
