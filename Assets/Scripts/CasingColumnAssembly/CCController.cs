using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasingColumnAssembly
{
    /// <summary>
    /// Класс управления конструкцией колонны
    /// </summary>
    public class CCController : MonoBehaviour
    {
        private ColumnBuilder m_columnBuilder;
        public ColumnBuilder GetColumnBuilder => m_columnBuilder;

        public void Initialize()
        {
            // ищу компоненты в дочерних обьектах
            m_columnBuilder = GetComponentInChildren<ColumnBuilder>();
            ColumnParent columnParent = GetComponentInChildren<ColumnParent>();

            if (!m_columnBuilder)
                throw new System.Exception($"для {this} отсутствует дочерний обьект с классом ColumnBuilder");

            if (!columnParent)
                throw new System.Exception($"для {this} отсутствует дочерний обьект с классом ColumnParent");

            m_columnBuilder.Initialize(columnParent.GetTransform);
        }

        public void Build(List<ElementParameters> value)
        {
            if (m_columnBuilder)
                m_columnBuilder.Build(value);
        }

        public void Clear() { }
    }
}