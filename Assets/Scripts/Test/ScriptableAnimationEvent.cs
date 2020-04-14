using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGlobal;

namespace Test
{
    [CreateAssetMenu(fileName = "TestAnimationEventScriptable", menuName = "ScriptableObjects/TestAnimationEvents")]
    public class ScriptableAnimationEvent : ScriptableObject
    {
        public GlobalEnums.AttackStates AttackStateValue;
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