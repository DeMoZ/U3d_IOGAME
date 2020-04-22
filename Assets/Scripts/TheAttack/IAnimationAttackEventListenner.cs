using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGlobal;

namespace TheAttack
{
    /// <summary>
    /// An interface for animation events listenners
    /// </summary>
    public interface IAnimationAttackEventListenner
    {
        void OnAnimationAttack(AnimationAttackEvent value);
    }
}
