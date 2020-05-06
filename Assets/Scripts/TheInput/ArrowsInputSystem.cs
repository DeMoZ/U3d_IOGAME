using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TheInput
{
    public class ArrowsInputSystem : MonoBehaviour, IInput
    {
        private Vector3 _direction = Vector3.zero;
        private PlayerInput _pInput;
        public Vector3 GetInput()
        {
            return _direction;
        }

        // Update is called once per frame
        void Update()
        {


            //direction.x = Input.GetAxis("Horizontal");
            //direction.y = Input.GetAxis("Vertical");

            

            _direction = Vector3.ClampMagnitude(_direction, 1);
        }
    }
}