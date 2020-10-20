using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleLensInteraction : LensInteractionController
{
    public GameObject targetObject;
    protected SpriteRenderer targetSprite;
    protected BoxCollider2D targetCollider;

    protected bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        lensColor = 6;
        targetSprite = targetObject.GetComponent<SpriteRenderer>();
        targetCollider = targetObject.GetComponent<BoxCollider2D>();

        targetCollider.enabled = false;
        targetSprite.color = new Color(1f, 1f, 1f, 0.02f);
    }

    public override IEnumerator StartInteraction()
    {
        if (!started)
        {
            started = true;
            targetCollider.enabled = true;
            for (float i = 0.02f; i < 1f; i += 0.02f)
            {
                targetSprite.color = new Color(1f, 1f, 1f, i);
                yield return new WaitForSeconds(0.05f);
            }
            this.GetComponent<AudioSource>().Play();
            Destroy(this);
        }
    }
}
