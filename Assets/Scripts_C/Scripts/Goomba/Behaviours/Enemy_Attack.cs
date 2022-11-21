using UnityEngine;

namespace Steering
{
    public class Enemy_Attack : MonoBehaviour
    {
        
        public GameObject PH_Attack;
        public float timeNumber;

        public float timeVisible = 1f;     
        public bool canTime = false;

        public bool AllowedAttack = false;
        public HunterBrain hBrain;

        public float timeBetweenAttacks = 3;

        
        public void Awake()
        {
            timeVisible = timeNumber;
            PH_Attack.SetActive(false);
        }

        public void Update()
        {
            if (AllowedAttack)
            {
                if (timeBetweenAttacks < 0)
                {
                    PH_Attack.SetActive(true);

                    if (timeVisible > 0f)
                    {
                        canTime = true;
                    }
                }
            }
            

            //is een timer foor de cooldown van attacking
            if (timeBetweenAttacks > 0)
            {
                timeBetweenAttacks = timeBetweenAttacks - 1 * Time.deltaTime;
            }

            
            
                // als je in range bent en de attack al niet bezig is dan voert hij de if uit
                if (canTime == true && timeVisible > 0)
                {
                    timeVisible = timeVisible - 1 * Time.deltaTime;
                }
                else if (timeVisible <= 0)
                {
                    TurnOff();
                    
                }
            
            
        }

        public void Attack()
        {
            
            
            
        }

        public void TurnOff()
        {
            Debug.Log("Dit doet het");
            canTime = false;
            PH_Attack.SetActive(false);
            timeVisible = timeNumber;
            hBrain.state = HunterBrain.HunterState.pursue;
            timeBetweenAttacks = 3f;
        }
    }
}