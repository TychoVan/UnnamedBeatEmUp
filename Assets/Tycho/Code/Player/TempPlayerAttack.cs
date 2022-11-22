using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class TempPlayerAttack : MonoBehaviour
    {
        [SerializeField, Tooltip("Amount of damage dealt to the enemy")] 
        private int    damageAmount            = 3;

        [SerializeField, Tooltip("Time before the player can attack again in S")] 
        private float  attackCooldown          = 0.3f;

        private bool                    canAttack;

        private I_Damagable             otherHealthScript;




        private void Start()
        {
            canAttack = true;
        }


        private void Update()
        {
            if (canAttack && Input.GetButtonDown("Fire1"))
            {
                if (otherHealthScript != null)
                {
                    StartCoroutine(Attack());
                }
            }   
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            otherHealthScript = other.gameObject.GetComponent<I_Damagable>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (otherHealthScript == other.gameObject.GetComponent<I_Damagable>())
            {
                otherHealthScript = null;
            }
        }


        private IEnumerator Attack()
        {
            canAttack = false;
            otherHealthScript.ChangeHealth(-damageAmount);

            yield return new WaitForSeconds(attackCooldown);
            canAttack = true;
        }
    }
}
