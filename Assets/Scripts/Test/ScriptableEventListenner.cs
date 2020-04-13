using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class ScriptableEventListenner : MonoBehaviour
    {
        public void ScriptableAttackTest(ScriptableAnimationEvent scriptableValues)
        {
            Debug.Log(scriptableValues.ToString());
        }
    }
}
