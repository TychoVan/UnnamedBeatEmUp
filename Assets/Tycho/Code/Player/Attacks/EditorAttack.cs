using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EditorAttack
{
    [field: SerializeField] public string       InputButton   { get; private set; }
    [field: SerializeField] public AttackData   AttackData    { get; private set; }
    [field: SerializeField] public GameObject[] AttackHitboxes { get; private set; }
}
