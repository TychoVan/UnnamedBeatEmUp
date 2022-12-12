using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UI;


namespace Player
{
    public class PlayerHealth : MonoBehaviour, I_Damagable, I_ScoreValue
    {
        [field: SerializeField] public int  Health { get; private set; }

        [SerializeField] private int            startHealth         = 10;
        [SerializeField] private int            maxHealth           = 10;

        [SerializeField] private UnityEvent     OnTakeDamage;

        public float                            UIValue             => Health;
        public float                            UIMaxValue          => maxHealth;
        public float                            UIMinValue          => 0;

        private void Start()
        {
            // Set the health.
            Health = startHealth;
        }

        // Change the health variable inside the clamp.
        public void ChangeHealth(int amount)
        {
            Health = Mathf.Clamp(Health += amount, 0, maxHealth);

            OnTakeDamage.Invoke();

            // Check if the health is 0.
            if (Health <= 0)
            {
                OnDeath();
            }
        }


        // Called upon death.
        public void OnDeath()
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("Playtest1");
        }
    }
}
