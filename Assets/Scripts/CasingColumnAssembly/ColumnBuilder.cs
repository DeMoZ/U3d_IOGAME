using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasingColumnAssembly
{
    /// <summary>
    /// Сборка / Очистка модели КНБК на сцене
    /// </summary>
    public class ColumnBuilder : MonoBehaviour
    {
        [Tooltip("Префаб элемента конструкции. Оболочка и джоинты.")]
        public GameObject m_ElementPrefab;

        //[Tooltip("Родительский объект модели колонны")]
        private Transform m_columnParent;
        public Transform GetColumnParent => m_columnParent;

        /// <summary>
        /// список элементов на сцене
        /// </summary>
        private List<ColumnElement> m_elements;

        /// <summary>
        /// номер слоя, на котором расположены все обьекты
        /// </summary>
        private int m_layerNumber;

        private int m_parametersCount;

        public void Initialize(Transform columnParent)
        {
            m_columnParent = columnParent;

            CheckFields();
        }

        private void CheckFields()
        {
            if (!m_columnParent)
                throw new System.Exception($"На сцене не указан родительский Transform для модели КНБК для {this}");
        }

        public void Clear()
        {
            for (int i = m_columnParent.childCount - 1; i > -1; i--)
            {
                GameObject child = m_columnParent.GetChild(i).gameObject;
#if UNITY_EDITOR
                if (Application.isPlaying)
                    Destroy(child);
                else
                    DestroyImmediate(child);
#else
                Destroy(child);
#endif
            }

            m_elements?.Clear();

            Debug.Log($"Очищено {this}.");
        }

        /// <summary>
        /// Собрать модель КНБК на сцене.
        /// </summary>
        /// <param name="columnParameters"></param>
        public void Build(List<ElementParameters> columnParameters)
        {
            // Debug.Log($"получено задание на строительство числа элементов {columnParameters.Count}");
            m_parametersCount = columnParameters.Count;

            for (var i = 0; i < columnParameters.Count; i++)
            {
                AddElement(i, columnParameters[i]);
            }
        }

        /// <summary>
        /// Временно.
        /// </summary>
        /// <param name="parameters"></param>
        public void AddElement(int number, ElementParameters parameters)
        {
            if (m_elements == null)
                m_elements = new List<ColumnElement>();

            GameObject prefab = m_ElementPrefab;
            Transform elementT = Instantiate(prefab, m_columnParent, false).transform;
            ColumnElement newElement = elementT.GetComponent<ColumnElement>();

            m_elements.Add(newElement); //
                                        // обязательный порядок
            ArrangeElement(newElement, parameters); //
        }

        /// <summary>
        /// располагает элемент относительно топ джоинта предыдущего элемента, или в нулевых координатах, 
        /// <para>если он первый в спиские </para>
        /// </summary>
        /// <param name="element"></param>
        private void ArrangeElement(ColumnElement element, ElementParameters parameters)
        {
            // TODO: -----------------------------
            // временные размеры элементов по умолчанию. Должны быть расчитаны исходя из колличества элементов и их длинны.
            float defaultElementRadius = 1.2f;
            float defaultElementThiknes = 0.1f;
            //------------------------------------

            // TODO: -----------------------------
            // временная длинна элемента. Привязана к индексу элемента в массиве элементов. должна быть расчетной, по TVD - True Value Deph, чтобы совпадать с крафиком траектории.
            int elementLength = m_elements.IndexOf(element);
            //------------------------------------

            float offset = 0.005f;   // небольшой промежуток между элементами, для наглядности

            if (elementLength > 0)
            {
                ColumnElement prevElement = m_elements[elementLength - 1];
                ProceduralParameters prevElementParameters = prevElement.GetPrceduralParameters;
                ProceduralParameters newElementParameters = new ProceduralParameters(
                    radius: prevElementParameters.Radius - prevElementParameters.Thiknes - offset,
                    thicknes: defaultElementThiknes,
                    length: elementLength + 1
                    );
                element.Build(newElementParameters);
            }
            else
            {
                // строить конструкцию изходя их колличества элементов в последовательности
                ProceduralParameters newElementParameters = new ProceduralParameters(
                    radius: defaultElementRadius,
                    thicknes: defaultElementThiknes,
                    length: elementLength + 1
                    );
                element.Build(newElementParameters);
            }

            // расстановка джоинтов после смены скейла по Y
            element.ArrangeJoints();
        }
    }
}