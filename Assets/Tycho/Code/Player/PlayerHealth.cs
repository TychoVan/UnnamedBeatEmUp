using UnityEngine;


namespace Player
{
    public class PlayerHealth : MonoBehaviour, I_Damagable
    {
        [field: SerializeField] public int  Health { get; private set; }

        [SerializeField] private int        startHealth         = 10;
        [SerializeField] private int        maxHealth           = 10;



        private void Start()
        {
            // Set the health.
            Health = startHealth;
        }

        // Change the health variable inside the clamp.
        public void ChangeHealth(int amount)
        {
            Health = Mathf.Clamp(Health += amount, 0, maxHealth);

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
        }
    }
}
