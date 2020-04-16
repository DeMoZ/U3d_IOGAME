using UnityEngine;

namespace CasingColumnAssembly
{
/// <summary>
/// класс вешмется на родительский обьект для конструкции КНБК
/// </summary>
    public class ColumnParent : MonoBehaviour
    {
        private Transform m_transform;
        public Transform GetTransform
        {
            get
            {
                if (!m_transform)
                    m_transform = transform;
                return m_transform;
            }
        }
    }
}
