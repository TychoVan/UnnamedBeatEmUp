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
        private Animator                        animator;
        [SerializeField] private float          Mana;
        [SerializeField] private float          MaxMana;
        [SerializeField] private float          MinMana;
        [SerializeField] private UnityEvent     OnAttack;
        private PlayerAnimations Animator;


        public float UIValue    => Mana;

        public float UIMaxValue => MaxMana;

        public float UIMinValue => MinMana;

        private void Start()
        {
            animator = this.gameObject.GetComponent<Animator>();

            for (int i = 0; i < Attacks.Length; i++) {
                for (int j = 0; j < Attacks[i].AttackHitboxes.Length; j++) {
                    Attacks[i].AttackScripts.Add(Attacks[i].AttackHitboxes[j].GetComponent<IAttack>());
                }
            }
        }


        private void Update()
        {
            for (int i = 0; i < Attacks.Length; i++)
            {
                if (Input.GetButtonDown(Attacks[i].InputButton))
                {
                    Attack(Attacks[i], Attacks[i].AttackData);
                }
            }
        }

       
        private void Attack(EditorAttack attack, AttackData attackData)
        {
            if (Mana - attackData.ManaCost >= MinMana)
            {
                Mana = Mathf.Clamp(Mana - attackData.ManaCost, MinMana, MaxMana);

                OnAttack.Invoke();
                animator.SetInteger("AnimationState", attackData.AnimationTag);

                // Activate the attack objects.
                for (int j = 0; j < attack.AttackHitboxes.Length; j++){
                    attack.AttackHitboxes[j].SetActive(true);
                    attack.AttackScripts[j].Attack(attack.AttackData.HittableTags);
                }
            }

        }
    }
}
