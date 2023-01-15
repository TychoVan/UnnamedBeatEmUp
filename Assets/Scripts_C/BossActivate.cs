using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivate : MonoBehaviour
{

    public BossBrain brain;
    public GameObject bossUI;
    public SpriteRenderer rend;
    public GameObject[] plein;
    public Animator anim;
    public GameObject Map;

   
    public void OnTriggerEnter2D(Collider2D collision)
    {
      
        if (collision.tag == "Player")
        {
            anim.SetBool("Door", true);

        }
    }

    public void ActiveerBoss()
    {
        Map.SetActive(false);
        bossUI.SetActive(true);
        brain.allowedAttack = true;
        plein[0].SetActive(true);
        plein[1].SetActive(true);
    }
        

    
}
