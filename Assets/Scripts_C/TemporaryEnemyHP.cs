using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

public class TemporaryEnemyHP : MonoBehaviour, I_Damagable, I_ScoreValue
{
    [field: SerializeField] public int Health { get; private set; }

    public float UIValue => Health;
    public float UIMaxValue => maxHealth;
    public float UIMinValue => 0;

    [SerializeField] private int startHealth = 10;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float timer = 1f;

    public Slider slider;

    public Animator anim;




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
        Debug.Log("Triggered");
        gameObject.SetActive(false);
    }
}
