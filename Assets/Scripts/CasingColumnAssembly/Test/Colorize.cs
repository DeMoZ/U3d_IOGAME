using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasingColumnAssembly.Test
{
    /// <summary>
    /// красит обьект в рандомный цвет
    /// </summary>
    [RequireComponent(typeof(Material))]
    public class Colorize : MonoBehaviour
    {
        private Renderer m_renderer;

        // private Color[] m_colors = { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.gray, Color.white, Color.black };
        // Start is called before the first frame update
        void Start()
        {
            m_renderer = GetComponent<Renderer>();
            Color color = new Color(
               Random.Range(0f, 1f),
               Random.Range(0f, 1f),
               Random.Range(0f, 1f)
   );

            m_renderer.material.color = color;
        }
    }
}