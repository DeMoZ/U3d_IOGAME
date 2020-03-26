using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheInput
{
    public class SimpleKeyboardInput : MonoBehaviour, IInput
    {
        private Vector3 direction = Vector3.zero;
        public Vector3 GetInput()
        {
            return direction;
        }

        // Update is called once per frame
        void Update()
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");

            direction = Vector3.ClampMagnitude(direction, 1);
        }
    }
}
