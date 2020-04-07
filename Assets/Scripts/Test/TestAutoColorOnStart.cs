using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestAutoColorOnStart : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Renderer renderer = GetComponent<Renderer>();

            //Color color = Random.ColorHSV();
            Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            renderer.material.color = color;
        }

 
    }
}