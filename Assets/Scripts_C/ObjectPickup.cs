using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;
    public float speed;
    public bool allowPickup = false;
    public Rigidbody2D rb;
    public Vector3 forward;
    public float pulseForce;
    public float declineRate;
    public float velY;

    public void Start()
    {
        velY = rb.velocity.y;
    }

    public void Update()
    {
        if (allowPickup)
        {            
           transform.position = Vector3.Lerp(transform.position, player.transform.position, speed);            
        }
        
    }
    
    
        
}
