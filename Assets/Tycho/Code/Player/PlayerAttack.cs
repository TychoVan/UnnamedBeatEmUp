using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using UnityEngine.Events;


namespace Player
{
    public class PlayerAttack : MonoBehaviour, I_ScoreValue
    {
        [SerializeField] private EditorAttack[] Attacks;
<<<<<<< Updated upstream
        private Animator                        animator;
=======
>>>>>>> Stashed changes
        [SerializeField] private float          Mana;
        [SerializeField] private float          MaxMana;
        [SerializeField] private float          MinMana;
        [SerializeField] private UnityEvent     OnAttack;
<<<<<<< Updated upstream
        private PlayerAnimations Animator;
=======
        private PlayerAnimations animator;
>>>>>>> Stashed changes


        public float UIValue    => Mana;

        public float UIMaxValue => MaxMana;

        public float UIMinValue => MinMana;

        private void Start()
        {
<<<<<<< Updated upstream
            animator = this.gameObject.GetComponent<Animator>();
=======
            animator = this.gameObject.GetComponent<PlayerAnimations>();
>>>>>>> Stashed changes

            for (int i = 0; i < Attacks.Length; i++) {
                for (int j = 0; j < Attacks[i].AttackHitboxes.Length; j++) {
                    Attacks[i].AttackScripts.Add(Attacks[i].AttackHitboxes[j].GetComponent<IAttack>());
<<<<<<< Updated upstream
=======
                    Attacks[i].CanAttack = true;
>>>>>>> Stashed changes
                }
            }
        }


        private void Update()
        {
<<<<<<< Updated upstream
            for (int i = 0; i < Attacks.Length; i++)
            {
                if (Input.GetButtonDown(Attacks[i].InputButton))
                {
                    Attack(Attacks[i], Attacks[i].AttackData);
=======
            for (int i = 0; i < Attacks.Length; i++) {
                if (Attacks[i].CanAttack) {
                    if (Input.GetButtonDown(Attacks[i].InputButton)) {
                        Attack(Attacks[i], Attacks[i].AttackData);
                    }
>>>>>>> Stashed changes
                }
            }
        }

       
        private void Attack(EditorAttack attack, AttackData attackData)
        {
<<<<<<< Updated upstream
            if (Mana - attackData.ManaCost >= MinMana)
            {
                Mana = Mathf.Clamp(Mana - attackData.ManaCost, MinMana, MaxMana);

                OnAttack.Invoke();
                animator.SetInteger("AnimationState", attackData.AnimationTag);
=======
            attack.CanAttack = false;

            if (Mana - attackData.ManaCost >= MinMana) {
                Mana = Mathf.Clamp(Mana - attackData.ManaCost, MinMana, MaxMana);

                OnAttack.Invoke();
                animator.PlayAnimation("AnimationState", attackData.AnimationTag, 1);
>>>>>>> Stashed changes

                // Activate the attack objects.
                for (int j = 0; j < attack.AttackHitboxes.Length; j++){
                    attack.AttackHitboxes[j].SetActive(true);
                    attack.AttackScripts[j].Attack(attack.AttackData.HittableTags);
                }
<<<<<<< Updated upstream
            }

        }
=======

                StartCoroutine(CooldownTimer(attack));
            }

        }


        private IEnumerator CooldownTimer(EditorAttack attack)
        {
            attack.CurrentCooldown = attack.AttackData.AttackCooldown;
            while (attack.CurrentCooldown > 0) {
                attack.CurrentCooldown = Mathf.Clamp(attack.CurrentCooldown, 0, attack.AttackData.AttackCooldown);
                yield return new WaitForEndOfFrame();
            }
            attack.CanAttack = true;
        }
>>>>>>> Stashed changes
    }
}
