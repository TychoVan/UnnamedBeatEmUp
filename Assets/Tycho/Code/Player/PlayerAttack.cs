using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using UnityEngine.Events;


namespace Player
{
    public class PlayerAttack : MonoBehaviour, I_ScoreValue
    {
        [SerializeField] private AttackData lightAttackData;
        [SerializeField] private AttackData heavyAttackData;
        [SerializeField] private LayerMask  attackLayer;

        [Header("Mana")]
        [SerializeField] private int        maxMana;
        [SerializeField] private int        minMana;
        [Tooltip("The amount of time between getting more mana in S")]
        [SerializeField] private float      manaGainInterval;
        [SerializeField] private int        manaGainAmount;

        [Header("Debug")]
        [SerializeField] private bool       lightAttackGizmos;
        [SerializeField] private bool       heavyAttackGizmos;
        [SerializeField] private int        mana;

        public float UIValue    => mana;
        public float UIMaxValue => maxMana;
        public float UIMinValue => minMana;

        // Private
        private Animator                    animator;
        private PlayerMovement              playerMovement;

        private bool                        isInAttack;
        private bool                        inHitPhase;
        private AttackData                  currentAttackData;
        private List<I_Damagable>           hitTargets = new List<I_Damagable>();




        private void Awake() {
            mana           = maxMana;

            playerMovement = GetComponent<PlayerMovement>();
            animator       = GetComponent<Animator>();

            StartCoroutine(GainMana());
        }


        public void Update() {
            #region input
            if (Input.GetButtonDown(lightAttackData.InputButtonName)      && !isInAttack) {
                if (mana - lightAttackData.ManaCost >= minMana) {
                    mana -= lightAttackData.ManaCost;

                    currentAttackData = lightAttackData;
                    isInAttack        = true;
                    animator.SetTrigger("Light Attack");
                }
            }
            else if (Input.GetButtonDown(heavyAttackData.InputButtonName) && !isInAttack) {
                if (mana - heavyAttackData.ManaCost >= minMana) {
                    mana -= heavyAttackData.ManaCost;

                    currentAttackData = heavyAttackData;
                    isInAttack = true;
                    animator.SetTrigger("Heavy Attack");
                }
            }
            #endregion

            // Damaging
            if (inHitPhase) {
                // Check for hittable targets
                Vector2 modifiedOffset = new Vector2(currentAttackData.HitOffset.x * playerMovement.LookDirection, currentAttackData.HitOffset.y);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(((Vector2)transform.position + currentAttackData.HitOffset) * modifiedOffset, 
                                                                 currentAttackData.HitSize, 
                                                                 0f, 
                                                                 attackLayer);

                // Do damage to all hittable targets
                for (int i = 0; i < colliders.Length; i++) {
                    I_Damagable damageable = colliders[i].GetComponent<I_Damagable>();
                    if (damageable != null && !hitTargets.Contains(damageable)){

                        damageable.ChangeHealth(-currentAttackData.Damage);
                            hitTargets.Add(damageable);
                    }
                }
            }

            // Move while attacking
            if (isInAttack) transform.Translate(Vector2.right * playerMovement.LookDirection * currentAttackData.ForwardMovement * Time.deltaTime);
        }


        #region animation events
        public void HandleStartHit() {
            inHitPhase = true;
        }


        public void HandleEndHit() {
            inHitPhase = false;
        }


        public void HandleEndAttackAnimationEvent() {
            // Reset all temporary attack data
            isInAttack        = false;
            currentAttackData = null;
            hitTargets.Clear();
        }
        #endregion


        private IEnumerator GainMana() {
            // Gain mana on interval
            while (manaGainAmount > 0 && manaGainInterval > 0) {
                yield return new WaitForSeconds(manaGainInterval);
                mana = Mathf.Clamp(mana + manaGainAmount, minMana, maxMana);
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (playerMovement == null) return;

            // Light attack
            if (lightAttackGizmos) {
                if (inHitPhase && currentAttackData == lightAttackData) Gizmos.color = lightAttackData.GizmoHitboxHitColor;
                else                                                    Gizmos.color = lightAttackData.GizmoHitboxColor;

                Gizmos.DrawWireCube(transform.position + new Vector3(lightAttackData.HitOffset.x * playerMovement.LookDirection, lightAttackData.HitOffset.y, 0f), 
                                    lightAttackData.HitSize);
            }

            //Heavy attack
            if (heavyAttackGizmos) {
                if (inHitPhase && currentAttackData == heavyAttackData) Gizmos.color = heavyAttackData.GizmoHitboxHitColor;
                else Gizmos.color = heavyAttackData.GizmoHitboxColor;

                Gizmos.DrawWireCube(transform.position + new Vector3(heavyAttackData.HitOffset.x * playerMovement.LookDirection, heavyAttackData.HitOffset.y, 0f),
                                    heavyAttackData.HitSize);
            }
        }
#endif
    }
}