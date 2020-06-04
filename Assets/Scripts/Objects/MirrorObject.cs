using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorObject : MonoBehaviour
{
    public float mirrorAxis;

    public Transform objectToBeMirrored;
    public Transform thisObject;
    protected SpriteRenderer thatSprite;
    protected SpriteRenderer thisSprite;

    
    // Start is called before the first frame update
    void Start()
    {
        thisSprite = GetComponent<SpriteRenderer>();
        thatSprite = objectToBeMirrored.gameObject.GetComponent<SpriteRenderer>();
        //thisSprite.flipY = true;
    }

    // Update is called once per frame
    void Update()
    {
        thisSprite.sprite = thatSprite.sprite;
        thisObject.position = new Vector3(objectToBeMirrored.position.x, -objectToBeMirrored.position.y + mirrorAxis, -objectToBeMirrored.position.y + mirrorAxis);
    }

    public void UpdateObjectToBeMirrored(GameObject newObject)
    {
        objectToBeMirrored = newObject.transform;
        thatSprite = newObject.GetComponent<SpriteRenderer>();
    }
}
