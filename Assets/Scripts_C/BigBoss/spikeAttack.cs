using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeAttack : MonoBehaviour
{
    public GameObject bigSpike;
    public GameObject spawnedSpike;
    public bool spawned;
    public GameObject player;
    public float perc = 0.0f;

    public Vector3 playerpos;
    
    [Header("Times")]
    public float Reset;
    public float DespawnTime;
    public bool allowtime = false;

    public float spikeSpeed;
    public float movespeed;
    

    [Header("Animation")]
    public Animator anim;
    float timer = 2;
    [Header("Brain")]
    public BossBrain brain;

    public void CallSpike()
    {
        spawnedSpike = Instantiate(bigSpike, transform.position, transform.rotation);
        spawned = true;
        playerpos = player.transform.position;
        anim.SetBool("GSlash", true);
        allowtime = true;
        brain.DamageOn();
        timer = 2f;
    }
    public void Update()
    {
        if(spawned == true)
        {
            Moving();            
        }

       
        if(timer > 0)
        {
            timer = timer - 1 * Time.deltaTime;
        }
        else
        {
            spawned = false;
            Destroy(spawnedSpike);
        }

       
    }
    public void Moving()
    {
        spawnedSpike.transform.position = Vector3.Lerp(spawnedSpike.transform.position, playerpos, spikeSpeed);

    }
    public void SetFalse()
    {
        anim.SetBool("GSlash", false);
        brain.DamageOff();
    }
   
}
