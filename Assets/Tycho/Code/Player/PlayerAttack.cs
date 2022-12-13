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
        }


        private void Update()
        {
            for (int i = 0; i < Attacks.Length; i++)
            {
                if (Input.GetButtonDown(Attacks[i].InputButton))
                {
                    if (Mana - Attacks[i].AttackData.ManaCost >= MinMana)
                    {
                        Mana = Mathf.Clamp(Mana - Attacks[i].AttackData.ManaCost, MinMana, MaxMana);
                        OnAttack.Invoke();
                        animator.SetInteger("AnimationState", Attacks[i].AttackData.AnimationTag);
                        for (int j = 0; j < Attacks[i].AttackHitboxes.Length; j++)
                        {
                            Attacks[i].AttackHitboxes[j].SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
