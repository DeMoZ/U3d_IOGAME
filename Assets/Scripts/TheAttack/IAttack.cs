using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheAttack
{
    public interface IAttack
    {
        /// <summary>
        /// Animation event invoke this method with state name
        /// </summary>
        /// <param name="attackStates"> string should be one of enum AttackStates state, othervise handle as default</param>
        void Attack(string attackStates);
    }

    enum AttackStates
    {
        None = 0,       // state with no activity or state wasnt parsed

        PreAttack,      // animation started and if required some pre effect/code before damage trigger appear
        StartAttack,    // damage trigger to be appeared
        EndAttack,      // damage trigger to be dissappear
        PostAttack,     // animation end and if required some pos effect/code required

        BrokeAttack,    // if attack has been broke
    };
}
