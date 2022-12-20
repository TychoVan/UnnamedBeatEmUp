using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player 
{ 
    public interface IAttack 
    {

        void Init(PlayerAttack Player);

        void Attack(List<string> targetTags);
    }
}
