using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/Player/Attack", order = 1)]
    public class AttackData : ScriptableObject
    {
        [field: SerializeField] public string       InputButtonName         { get; private set; }

        [field: SerializeField] public int          Damage                  { get; private set; }
        [field: SerializeField] public int          ManaCost                { get; private set; }


        [field: SerializeField] public Vector2      HitOffset               { get; private set; }
        [field: SerializeField] public Vector2      HitSize                 { get; private set; }
        [field: SerializeField] public float        ForwardMovement         { get; private set; }

        [field: SerializeField] public Color        GizmoHitboxColor        { get; private set; }
        [field: SerializeField] public Color        GizmoHitboxHitColor     { get; private set; }
    }
}
