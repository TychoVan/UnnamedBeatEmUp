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
        spawnPoints();
    }
    

    public void Update()
    {
           
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
    }
    public void Dashing()
    {
        if (AllowDash)
        {
            transform.position = Vector3.Lerp(transform.position, dashpositions[attackCount].transform.position, speed);
        }
        
    }
    public void spawnPoints()
    {        
        for(int i = 0; i < dashpositions.Length; i++)
        {
            
            X = Random.Range(-8, 8);
            Y = Random.Range(-4, 4);

            Vector3 spawnpos = new Vector3(X, Y, 0);
            dashpositions[i] = Instantiate(marker, spawnpos, transform.rotation);
            
            
            if (i == 2)
            {
                AllowDash = true;
               
            }
           
        }              
    }
}
