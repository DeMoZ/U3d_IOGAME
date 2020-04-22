using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGlobal
{
    public class GlobalEnums
    {
        /// <summary>
        /// unic names or ids for animation. will be received by script to be affected by name/id  only 
        /// </summary>
        public enum AnimationNamesIDs
        {
            Simple,
        }

        /// <summary>
        /// Layers of animator controller. will be blended by scripts
        /// </summary>
        public enum AnimatorLayers
        {
            Normal,
            Attack,
        }

        /// <summary>
        /// all posible attac states
        /// </summary>
        public enum AttackStates
        {
            None = 0,       // state with no activity

            PreAttack,      // animation started and if required some pre effect/code before damage trigger appear
            Attack,    // damage trigger to be appeared
            Hold,      // damage trigger to dissappear, waiting to continue attack queue
            Back,     // animation end and if required some pos effect/code required
            End,

            BrokeAttack,    // if attack has been broke
        };

        /// <summary>
        /// all posible attac states
        /// </summary>
        public enum QueueStates
        {
            NotReady = 0,       // state with no activity or state wasnt parsed
            Attacking,          // performing attack
            Ready               // Ready to listen button for attack
          
        };

        /// <summary>
        /// Triggers for animator
        /// </summary>
        public enum AnimatorTriggers
        {
            Attack,
        }
    }
}
