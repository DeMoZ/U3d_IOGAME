using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasingColumnAssembly
{
    /// <summary>
    /// Скрипт имитирует получение данных снаружи. 
    /// </summary>
    [RequireComponent(typeof(ColumnsManager))]
    public class ColumnReceiver : MonoBehaviour
    {
        public ReceivedData m_ReceivedData;

        private ColumnsManager m_columnsManager;

        private void Start()
        {
            m_columnsManager = GetComponent<ColumnsManager>();

            //Invoke("Receive", 0.1f);
            Receive();
        }

        [ContextMenu("Receive Fake Parameters (from Scriptable object ReceivedData)")]
        public void Receive()
        {
            List<ElementParameters> parameters = new List<ElementParameters>();

            for (int i = 0; i < m_ReceivedData.m_Parameters.Length; i++)
            {
                ElementParameters elementParameters =
                    new ElementParameters(
                        name: m_ReceivedData.m_Parameters[i].name,
                        top: m_ReceivedData.m_Parameters[i].top,
                        bottom: m_ReceivedData.m_Parameters[i].bottom,
                        length: m_ReceivedData.m_Parameters[i].length,
                        diameter: m_ReceivedData.m_Parameters[i].diameter,
                        casingKind: m_ReceivedData.m_Parameters[i].casingKind
                    );

                parameters.Add(elementParameters);

                //Debug.Log($" Receive - name {parameters[i].name} top {parameters[i].top} bottom {parameters[i].bottom} length {parameters[i].length} diameter {parameters[i].diameter} casingKind {parameters[i].casingKind}");
            }

            m_columnsManager.SetWidgetParameters(parameters);
        }
    }
}
