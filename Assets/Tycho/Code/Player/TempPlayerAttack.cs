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
        private SpriteRenderer          tempOtherObject;




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
                    StartCoroutine(Attack(tempOtherObject));
                }
            }   
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            otherHealthScript = other.gameObject.GetComponent<I_Damagable>();
            tempOtherObject = other.gameObject.GetComponent<SpriteRenderer>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (otherHealthScript == other.gameObject.GetComponent<I_Damagable>())
            {
                otherHealthScript = null;
                tempOtherObject = null;
            }
        }


        private IEnumerator Attack(SpriteRenderer sprite)
        {
            canAttack = false;
            otherHealthScript.ChangeHealth(-damageAmount);
            sprite.color = Color.green;

            yield return new WaitForSeconds(attackCooldown);

            sprite.color = Color.red;
            canAttack = true;
        }
    }
}
