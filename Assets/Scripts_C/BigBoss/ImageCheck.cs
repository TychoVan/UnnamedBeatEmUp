using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCheck : MonoBehaviour
{
    public SpriteRenderer rend;
    public Transform playerpos;

    public void Update()
    {
        if(playerpos.position.x < transform.position.x)
        {
            rend.flipX = false;
        }
        if (playerpos.position.x > transform.position.x)
        {
            rend.flipX = true;
            
        }
    }
}
