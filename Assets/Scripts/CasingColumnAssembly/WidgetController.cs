using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasingColumnAssembly
{
    /// <summary>
    /// Класс создает и инициализирует виджет конструкции КНБК и внедряет все соответствующие зависимости
    /// </summary>
    public class WidgetController : MonoBehaviour
    {
        [Tooltip("Объект сцены, с классом сборщиком КНБК")]
        [SerializeField] CCController m_ccController;
        public CCController GetCCController => m_ccController;

        /// <summary>
        /// полученные данные коснтрукции
        /// </summary>
        private List<ElementParameters> m_parameters = new List<ElementParameters>();

        void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Устанавливает зависимости, подписывает на события, заполняет поля, скрывает элементы
        /// </summary>
        public void Initialize()
        {
            m_ccController.Initialize();

            // подписываю панель информации на эвенты 
            // ... some code
            // очистка - нужна на случаей, если будет запущеа повторная инициализация
            Clear();
        }

        // инициализирует
        public void PopulateParameters(List<ElementParameters> value)
        {
            m_parameters = value;
        }

        /// <summary>
        /// Удалить сгенерированные элементы UI и КНБК со сцены, остановить рендеринг камеры, очистить текстуру виджета
        /// </summary>
        public void Clear()
        {
            m_ccController.Clear();

            m_parameters.Clear();
        }

        /// <summary>
        /// построить КНБК на сцене, заполнить UI виджета
        /// </summary>
        public void Build()
        {
            if (m_parameters.Count > 0)
            {
                m_ccController.Build(m_parameters);
            }
        }
    }
}
