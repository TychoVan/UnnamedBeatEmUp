using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class EditorAttack
    {
        [field: SerializeField] public string       InputButton   { get; private set; }
        [field: SerializeField] public AttackData   AttackData    { get; private set; }
        [field: SerializeField] public GameObject[] AttackHitboxes { get; private set; }

        [Header("DO NOT EDIT")]
        public List<IAttack> AttackScripts;
    }
}
