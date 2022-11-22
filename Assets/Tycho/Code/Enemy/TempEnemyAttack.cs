using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemyAttack : MonoBehaviour
{
    private int damageAmount = 3;

    private I_Damagable otherHealthScript;
    private SpriteRenderer tempOtherObject;



    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            otherHealthScript = other.gameObject.GetComponent<I_Damagable>();
            tempOtherObject = other.gameObject.GetComponent<SpriteRenderer>();
        }
    }


    private IEnumerator Attack(SpriteRenderer sprite)
    {
        sprite.color = Color.blue;

        yield return new WaitForSeconds(0.3f);

        sprite.color = Color.white;
    }
    public void Attack()
    {
        Debug.Log("test");
        if (otherHealthScript != null)
        {
            StartCoroutine(Attack(tempOtherObject));
            otherHealthScript.ChangeHealth(-damageAmount);

            otherHealthScript = null;
            tempOtherObject = null;
        }
    }
}
