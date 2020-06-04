using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorPlayerController : MonoBehaviour
{
    public float mirrorAxis;
    public float hideAxisX;
    public bool useHideAxis;
    public bool lessThanX;

    protected Transform player;
    protected Transform thisTransform;
    protected SpriteRenderer thisSprite;
    protected PlayerController playerController;
    protected Animator thisAnim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player(Clone)").GetComponent<Transform>();
        playerController = GameObject.Find("player(Clone)").GetComponent<PlayerController>();
        thisTransform = GetComponent<Transform>();
        thisSprite = GetComponent<SpriteRenderer>();
        thisAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkVisibility();
        if (playerController.walking)
        {
            thisAnim.SetBool("walking", true);
        }
        else
        {
            thisAnim.SetBool("walking", false);
        }

        if(playerController.direction == 0)
        {
            thisAnim.SetInteger("walkDirection", 2);
        }
        else if(playerController.direction == 1)
        {
            thisAnim.SetInteger("walkDirection", 3);
        }
        else if (playerController.direction == 2)
        {
            thisAnim.SetInteger("walkDirection", 0);
        }
        else if (playerController.direction == 3)
        {
            thisAnim.SetInteger("walkDirection", 1);
        }
        thisTransform.position = new Vector3(player.position.x, -player.position.y + .38f + mirrorAxis, -player.position.y);
    }

    protected void checkVisibility()
    {
        if (useHideAxis)
        {
            if (!lessThanX)
            {
                if (player.position.x > hideAxisX)
                {
                    thisSprite.color = new Color(1f, 1f, 1f, 0f);
                }
                else
                {
                    thisSprite.color = new Color(1f, 1f, 1f, 1f);
                }
            }
            else
            {
                if (player.position.x < hideAxisX)
                {
                    thisSprite.color = new Color(1f, 1f, 1f, 0f);
                }
                else
                {
                    thisSprite.color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
    }
}
