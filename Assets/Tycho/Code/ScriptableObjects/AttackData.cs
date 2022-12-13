using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "ScriptableObjects/Player/Attack", order = 1)]
public class AttackData : ScriptableObject
{
    [field: SerializeField] public List<string>     HittableTags   { get; private set; }
    [field: SerializeField] public float            ManaCost       { get; private set; }
    [field: SerializeField] public float            AttackCooldown { get; private set; }
    [field: SerializeField] public float            DamageAmount   { get; private set; }
    [field: SerializeField] public int              AnimationTag   { get; private set; }
}
