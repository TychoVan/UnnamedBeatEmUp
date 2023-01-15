using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivate : MonoBehaviour
{

    public BossBrain brain;
    public GameObject bossUI;
    public SpriteRenderer rend;
    public GameObject plein;
    public Animator anim;

   
    public void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.tag == "Player")
        {
            anim.SetBool("Door", true);

        }
    }

    public void ActiveerBoss()
    {
        bossUI.SetActive(true);
        brain.allowedAttack = true;
        plein.SetActive(true);
    }
        

    
}
