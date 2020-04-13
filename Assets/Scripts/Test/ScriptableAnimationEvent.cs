using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    [CreateAssetMenu(fileName = "AnimationEventScriptable", menuName = "ScriptableObjects/AnimationEvents")]
    public class ScriptableAnimationEvent : ScriptableObject
    {
        public TheAttack.AttackStates AttackStateValue;
        public int IntValue;
        public float FloatValue;

        public override string ToString()
        {
            string rezult = "";
            rezult+= $"_";
            rezult += $"AttackStateValue ={AttackStateValue} ; ";
            rezult += $"IntValue ={IntValue} ; ";
            rezult += $"FloatValue ={FloatValue} ; ";

            return rezult;
        }
    }
}