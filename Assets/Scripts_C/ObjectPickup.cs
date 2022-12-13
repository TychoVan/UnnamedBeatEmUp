using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class ObjectPickup : MonoBehaviour
    {
        public GameObject player;
        public float speed;       
        public Rigidbody2D rb;
        public Vector3 forward;
        public float declineRate;
        public float velY;
       // public float timer = 0f;

        public PlayerHealth pHealth;


        public void Awake()
        {
            pHealth = FindObjectOfType<PlayerHealth>();
           
        }
        public void Start()
        {
            velY = rb.velocity.y;
        }

        public void Update()
        {                       
                transform.position = Vector3.Lerp(transform.position, player.transform.position, speed);
                Debug.Log("Wtf");                                    
        }
      
        public void OnTriggerEnter2D(Collider2D collision)
        {                                
                Destroy(gameObject);
                pHealth.ChangeHealth(+1);                      
        }

    }
}