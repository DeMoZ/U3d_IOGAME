using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMove
{
    /// <summary>
    /// very simple move component
    /// </summary>
    public class SimpleMove : MonoBehaviour, IMove
    {
        [SerializeField] private float _speed = 2;

        private Transform _transform;
        public void Move(Vector3 direction)
        {
            _transform.Translate(direction * _speed * Time.deltaTime);
        }

        void Start()
        {
            _transform = transform;
        }
    }
}