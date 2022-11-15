using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{


    public class BehaviorContext : MonoBehaviour
    {
        public Vector3 m_position; // the current position
        public Vector3 m_velocity; // the currten velocity
        public GenericSteering m_settings; // all the steering settings
        public BehaviorContext(Vector2 position, Vector2 velocity, GenericSteering settings)
        {
            m_position = position;
            m_velocity = velocity;
            m_settings = settings;
        }
    }
}