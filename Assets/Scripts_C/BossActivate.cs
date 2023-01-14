using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivate : MonoBehaviour
{

    public BossBrain brain;
    public GameObject bossUI;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bossUI.SetActive(true);
            brain.allowedAttack = true;
        }
    }
}
