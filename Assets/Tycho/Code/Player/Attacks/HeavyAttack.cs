using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player
{
    public class HeavyAttack : MonoBehaviour, IAttack
    {
        [field: SerializeField] public float attackCooldown { get; private set; }

        private bool    canAttack;
        private int     damageAmount = 3;


        private I_Damagable otherHealthScript;
        private SpriteRenderer tempOtherObject;

        public void Attack(List<string> targetTags)
        {
            throw new System.NotImplementedException();
        }
        public void Init(PlayerAttack Player)
        {
            throw new System.NotImplementedException();
        }

        public List<string> TargetTags;


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
            canAttack = true;
        }
    }
}
