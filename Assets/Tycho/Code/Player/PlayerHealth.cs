using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UI;


namespace Player
{
    public class PlayerHealth : MonoBehaviour, I_Damagable, I_ScoreValue
    {
        [field: SerializeField] public int      Health { get; private set; }

        [SerializeField] private int            startHealth         = 10;
        [SerializeField] private int            maxHealth           = 10;

        [SerializeField] private UnityEvent     OnTakeDamage;
        [SerializeField] private float          invincibilityTime;
        [SerializeField] private float          backwardsMovement;
        [SerializeField] private Color          damageColor;

        // Private
        private bool                            takingDamage        = false;
        private Animator                        animator;
        private SpriteRenderer                  spriteRenderer;
        private PlayerMovement                  playerMovement;


        public float                            UIValue             => Health;
        public float                            UIMaxValue          => maxHealth;
        public float                            UIMinValue          => 0;

        private void Awake()
        {
            // Set the health.
            Health         = startHealth;
            animator       = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        // Change the health variable inside the clamp.
        public void ChangeHealth(int amount)
        {
            if (amount < 0 && !takingDamage) StartCoroutine(HandleTakeDamage(amount));

            // Check if the health is 0.
            if (Health <= 0)
            {
                OnDeath();
            }
        }
        

        private IEnumerator HandleTakeDamage(int amount)
        {
            takingDamage           = true;
            playerMovement.canMove = false;

            spriteRenderer.color = damageColor;
            Health = Mathf.Clamp(Health += amount, 0, maxHealth);
            OnTakeDamage.Invoke();
            animator?.SetTrigger("Take Damage");

            yield return new WaitForSeconds(invincibilityTime);
            takingDamage           = false;
        }

        public void HandleDamageTaken() {
            playerMovement.canMove = true;
            spriteRenderer.color = Color.white;
        }

        private void Update() {
            if (!playerMovement.canMove && takingDamage) transform.Translate(Vector2.right * -playerMovement.LookDirection * backwardsMovement * Time.deltaTime);
        }

        // Called upon death.
        public void OnDeath()
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Playtest1");
        }
    }
}
