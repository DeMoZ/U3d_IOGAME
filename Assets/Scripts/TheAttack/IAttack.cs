using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGlobal;

namespace TheAttack
{
    public interface IAttack
    {
        /// <summary>
        ///  invoke this method with state name
        /// </summary>
        /// <param name="attackStates"> </param>
        void Attack(GlobalEnums.AttackStates attackState);
    }
}
