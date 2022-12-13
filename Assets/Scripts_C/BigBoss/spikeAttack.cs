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
    

    public float spikeSpeed;
    public float movespeed;
    
    public void CallSpike()
    {
        spawnedSpike = Instantiate(bigSpike, transform.position, transform.rotation);
        spawned = true;
        playerpos = player.transform.position;
    }
    public void Update()
    {
        if(spawned == true)
        {
            Moving();            
        }
    }
    public void Moving()
    {
        spawnedSpike.transform.position = Vector3.Lerp(spawnedSpike.transform.position, playerpos, spikeSpeed);

    }
   
}
