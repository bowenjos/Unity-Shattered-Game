using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainStormController : MonoBehaviour
{
    public SpriteRenderer thisSprite;
    public Sprite[] rainSprite;

    public float speed;
    protected bool changed;

    // Start is called before the first frame update
    void Start()
    {
        changed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!changed && !GameControl.control.paused)
        {
            Debug.Log("Rain");
            StartCoroutine(Change());
        }
    }

    public IEnumerator Change()
    {
        changed = true;
        Debug.Log("Rain1");
        thisSprite.sprite = rainSprite[0];
        yield return new WaitForSeconds(speed);
        Debug.Log("Rain2");
        thisSprite.sprite = rainSprite[1];
        yield return new WaitForSeconds(speed);
        Debug.Log("Rain3");
        thisSprite.sprite = rainSprite[2];
        yield return new WaitForSeconds(speed);
        changed = false;
        Debug.Log("Done Raining");
    }
}
