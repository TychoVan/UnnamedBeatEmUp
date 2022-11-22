using UnityEngine;


namespace Player
{
    public class PlayerHealth : MonoBehaviour, I_Damagable
    {
        [field: SerializeField] public int  Health { get; private set; }

        [SerializeField] private int        maxHealth           = 10;




        public void ChangeHealth(int amount)
        {
            Health += amount;
            Mathf.Clamp(Health, 0, maxHealth);

            if (Health <= 0)
            {
                OnDeath();
            }
        }


        public void OnDeath()
        {
            gameObject.SetActive(false);
        }
    }
}
