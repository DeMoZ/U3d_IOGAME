using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CasingColumnAssembly
{
    [CreateAssetMenu(fileName = "CasingColumnReceivedData", menuName = "Scriptable/CasingColumnReceivedData")]
    public class ReceivedData : ScriptableObject
    {
        public ReceivedParameters[] m_Parameters;
    }

    [System.Serializable]
    public class ReceivedParameters
    {
        [FormerlySerializedAs("name")] public string name;
        [FormerlySerializedAs("id")] public string id;
        [FormerlySerializedAs("top")] public string top;
        [FormerlySerializedAs("bottom")] public string bottom;
        [FormerlySerializedAs("length")] public string length;
        [FormerlySerializedAs("diameter")] public string diameter;
        [FormerlySerializedAs("casingKind")] public string casingKind;
        [FormerlySerializedAs("pipeModel")] public string pipeModel;
    }
}
