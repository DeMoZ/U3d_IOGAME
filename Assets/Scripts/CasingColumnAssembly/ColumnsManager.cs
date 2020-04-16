using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasingColumnAssembly
{
    /// <summary>
    /// Скрипт управляет виджетами обсадных колонн
    /// </summary>
    [DisallowMultipleComponent]
    public class ColumnsManager : MonoBehaviour
    {
        [SerializeField] WidgetController m_widgetController;

        // Start is called before the first frame update
        void Awake()
        {
            // initialize? and self check
        }

        public void SetWidgetParameters(List<ElementParameters> elementParameters)
        {
            Debug.Log($"SetWidgetParameters on {this} ; count parameters =  {elementParameters.Count}");

            foreach (var param in elementParameters)
            {
                Debug.Log($" SetWidgetParameters - name {param.name} top {param.top} bottom {param.bottom} length {param.length} diameter {param.diameter} casingKind {param.casingKind}");

            }

            m_widgetController.PopulateParameters(elementParameters);
            m_widgetController.Build();
        }
    }
}
