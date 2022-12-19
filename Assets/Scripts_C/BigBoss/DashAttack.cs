using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
    public GameObject marker;
    public GameObject[] dashpositions;
    public float speed;
    public float attackTimer = 2.0f;
    public bool AllowDash = false;
    public bool AllowCount = false;
    public int attackCount = 0;
    public float attackReset;

    [Header("Positions")]
    public float X;
    public float Y;

    public void Start()
    {
       
    }
    
    public void Update()
    {
           // sets
        if(attackTimer > 0)
        {
            Dashing();
            attackTimer = attackTimer - 1 * Time.deltaTime;
        }
        else if(attackTimer < 0)
        {            
            attackTimer = attackReset;
            attackCount++;
        }
        //destroys if final checkpoint reached
        if(attackCount == 3)
        {
            Destroy(dashpositions[0]);
            Destroy(dashpositions[1]);
            Destroy(dashpositions[2]);
        }
    }
    public void Dashing()
    {
        //code for dashing
        if (AllowDash)
        {
            transform.position = Vector3.Lerp(transform.position, dashpositions[attackCount].transform.position, speed);
        }       
    }
    public void spawnPoints()
    {
        attackCount = 0;
        for(int i = 0; i < dashpositions.Length; i++)
        {
            //random position between - and + x and y coordinates
            X = Random.Range(-8, 8);
            Y = Random.Range(-4, 4);

            // sets the spawn pos, and places checkpoint in their right slot
            Vector3 spawnpos = new Vector3(X, Y, 0);
            dashpositions[i] = Instantiate(marker, spawnpos, transform.rotation);
            
            // if i is 2 (max checkpoints) set allow dash to true
            if (i == 2)
            {
                AllowDash = true;            
            }          
        }              
    }
}