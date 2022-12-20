using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    [RequireComponent(typeof(Steering))]
    public class HunterBrain : MonoBehaviour
    {
        public enum HunterState { Idle, Approach, pursue, evade, FollowPath, Attack }

        [Header("Target and state")]
        public GameObject target;
        public HunterState state;
        public float pursueRadius = 1f;
        public float approachRadius = 1f;
        public float attackRadius = 1f;

        [Header("Steering settings")]
        public GenericSteering idlesettings;
        public GenericSteering approachSettings;
        public GenericSteering PursueSettings;

        [Header("Private")]
        private Steering m_steering;
        

        [Header("Array")]
        public GameObject[] waypoints;

        [Header("Attack")]
        public Enemy_Attack attackScript;

        private void Awake()
        {
            
        }
        private void Start()
        {
            //get steering
            m_steering = GetComponent<Steering>();

            //start idle
            ToIdle();

            

        }

        private void FixedUpdate()
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            //the finite state machine, to update animation and enum state
            switch (state)
            {
                case HunterState.Idle:
                    if (distanceToTarget < approachRadius)
                    {
                        if (distanceToTarget > attackRadius)
                            ToApproach();
                        
                        PursueSettings.maxSpeed = 2;
                    }                   
                    break;
                case HunterState.Approach:
                    if (distanceToTarget > approachRadius)
                    {
                        ToIdle();
                        
                        PursueSettings.maxSpeed = 2;
                    }                      
                    else if (distanceToTarget < pursueRadius)
                    {
                        ToPursue();
                        
                        PursueSettings.maxSpeed = 4;
                        attackScript.AllowedAttack = false;
                    }                       
                    break;
                case HunterState.pursue:
                    if (distanceToTarget < attackRadius)
                    {
                        ToIdle();
                        Debug.Log("Dit werkt");
                        
                        attackScript.AllowedAttack = true;
                    }
                    else if (distanceToTarget > pursueRadius)
                    {
                        attackScript.AllowedAttack = false;
                        ToApproach();
                        
                        PursueSettings.maxSpeed = 2;
                    }
                    
                    break;
                
            }
        }
        //idle state
        private void ToIdle()
        {
            state = HunterState.Idle;
            m_steering.settings = idlesettings;

            List<IBehavior> behaviors = new List<IBehavior>();
            behaviors.Add(new Idle());
            m_steering.SetBehaviors(behaviors, "Idle");
        }
        //approach state
        private void ToApproach()
        {
            state = HunterState.Approach;
            m_steering.settings = approachSettings;

            List<IBehavior> behaviors = new List<IBehavior>();
            behaviors.Add(new Seek(target));
            m_steering.SetBehaviors(behaviors, "Approach");

        }
        //pursue state
        private void ToPursue()
        {
            state = HunterState.pursue;
            m_steering.settings = PursueSettings;

            List<IBehavior> behaviors = new List<IBehavior>();
            behaviors.Add(new Pursue(target));
            m_steering.SetBehaviors(behaviors, "Pursue");
        }
        //followPath state
        private void FollowPath()
        {
            state = HunterState.FollowPath;
            List<IBehavior> behaviors = new List<IBehavior>();
            behaviors.Add(new FollowPath(waypoints));
            m_steering.SetBehaviors(behaviors, "Followpath");
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Support.DrawWireDisc(transform.position, approachRadius, Color.cyan);
            Support.DrawWireDisc(transform.position, pursueRadius, Color.cyan);
            Support.DrawWireDisc(transform.position, attackRadius, Color.red);
        }
#endif
    }
  
}