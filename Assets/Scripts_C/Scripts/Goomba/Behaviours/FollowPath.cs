using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Steering
{
    public class FollowPath : Behaviour
    {
        private GameObject[] waypointList;
        private string waypointTag = "Waypoint";
        public int waypointIndex = 0;
        private float waypointRadius = 2.5f;

        public FollowPath(GameObject[] waypoints)
        {
            waypointList = waypoints;
        }
        public override void start(BehaviorContext context)
        {
            base.start(context);
            //initialize things for behavior here
            
        }

        override public Vector3 CalculateSteeringForce(float dt, BehaviorContext context)
        {
           

            // calculate distance to waypoint
            float _distanceToWaypoint = (waypointList[waypointIndex].transform.position - context.m_position).magnitude;

            // update waypoint index if object is within radius
            Debug.Log("_distanceToWaypoint: " + _distanceToWaypoint + "  waypointRadius: " + waypointRadius);
            if (_distanceToWaypoint < waypointRadius)
            {
               
                if (waypointIndex < waypointList.Length - 1)
                {
                    ++waypointIndex;
                    
                }
                //else
                //{
                //    waypointIndex = 0;
                //}
            }
            Debug.Log(waypointIndex);
            positionTarget = waypointList[waypointIndex].transform.position;
            //update target position plus desired velocity, and returning steering force

            velocityDesired = (positionTarget - context.m_position).normalized * context.m_settings.maxDesiredVelocity;
            return velocityDesired - context.m_velocity;
        }

#if UNITY_EDITOR
        public override void OnDrawGizmos(BehaviorContext context)
        {
            base.OnDrawGizmos(context);
            // draw things for behavior
            // draw lines between waypoints
            GameObject _previousWP = null;
            foreach (var _currentWP in waypointList)
            {
                // draw circle
                Support.DrawWireDisc(_currentWP.transform.position, waypointRadius, Color.black);

                // draw line between waypoints
                if (_previousWP != null)
                    Debug.DrawLine(_previousWP.transform.position, _currentWP.transform.position, Color.black);

                // update previous waypoint with the current one
                _previousWP = _currentWP;
            }
            //met grote hulp van milos

        }
#endif
    }
}