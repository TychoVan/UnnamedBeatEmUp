using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Steering;

public class TemporaryEnemyHP : MonoBehaviour, I_Damagable, I_ScoreValue
{
    [field: SerializeField] public int Health { get; private set; }

    public float UIValue => Health;
    public float UIMaxValue => maxHealth;
    public float UIMinValue => 0;

    [SerializeField] private int startHealth = 10;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float timer = 1f;
    [SerializeField] private BossBrain boss;

    public Slider slider;

    public Animator anim;

    [Header("PickUp")]
    [SerializeField] private float          dropChancePercent;
    [SerializeField] private GameObject     healthPickUp;




    private void Start()
    {
        // Set the health.
        Health = startHealth;
    }


    // Change the health variable inside the clamp.
    public void ChangeHealth(int amount)
    {
        if (anim != null)
        {
            anim.SetBool("Hit", true);
            StartCoroutine(hitreset());
        }
        Health += amount;
        Mathf.Clamp(Health, 0, maxHealth);

        // Check if the health is 0.
        if (Health <= 0)
        {
            OnDeath();
        }
        if(slider != null)
        {
            StartCoroutine(slider.LerpSlider());
        }
        
        
    }


    // Called upon death.
    public void OnDeath()
    {
        if(anim == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            anim.SetBool("Death", true);
        }
                
    }
    public IEnumerator hitreset()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Hit", false);
    }
    
    public void animDeath()
    {
        float chance = Random.Range(0, 100);
        Debug.Log(chance);
        if (chance <= dropChancePercent)
        {
            Debug.Log("spawned pickup");
            HealthPickup pickup = Instantiate(healthPickUp, transform.position, Quaternion.identity).GetComponent<HealthPickup>();
            pickup.target = GetComponent<HunterBrain>().target;
        }

        Debug.Log("Triggered");
        gameObject.SetActive(false);
        if(boss != null)
        {
            boss.allowedAttack = false;
        }
    }
}
