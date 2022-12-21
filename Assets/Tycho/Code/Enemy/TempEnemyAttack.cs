using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyAttack : MonoBehaviour
{
    private int damageAmount = 1;

    private I_Damagable otherHealthScript;
    private SpriteRenderer tempOtherObject;

    private bool canAttack = true;
    [SerializeField, Tooltip("Time before the player can attack again in S")]
    private float attackCooldown = 0.3f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            otherHealthScript = other.gameObject.GetComponent<I_Damagable>();
            tempOtherObject = other.gameObject.GetComponent<SpriteRenderer>();
            if (canAttack)
            {
                Attack(tempOtherObject);
            }
        }
    }


    private void Attack(SpriteRenderer sprite)
    {
        canAttack = false;
        otherHealthScript.ChangeHealth(-damageAmount);
        //sprite.color = Color.blue;
    }

    private void OnDisable()
    {
        tempOtherObject.color = Color.white;
        canAttack = true;

        otherHealthScript = null;
        tempOtherObject = null;
    }
}
