using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int            healthAmount;

    [Header("Movement")]
    [SerializeField] private bool           floatTowardsTarget;

    [SerializeField] private float          floatSpeedMultiplier;
    [SerializeField] private AnimationCurve speedAcceleration;
    [SerializeField] private float          accelerationTime;
    private float                           timeElapsed;

    [Header("Dont edit")]
    public GameObject target;




    private void Start() {
        timeElapsed = 0;
    }


    void Update() {
        float floatSpeed = speedAcceleration.Evaluate(timeElapsed / accelerationTime)* floatSpeedMultiplier;

        float step =  floatSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        timeElapsed = Mathf.Clamp(timeElapsed + Time.deltaTime, 0, accelerationTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"));
        {
            I_Damagable healthScript = other.GetComponent<I_Damagable>();
            healthScript.ChangeHealth(healthAmount);
            Destroy(this.gameObject);
        }
    }
}
