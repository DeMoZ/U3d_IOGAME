using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMove : MonoBehaviour,IMove
{
    [SerializeField] private float speed=2;
    private Transform _transform;
    public void Move(Vector3 direction)
    {
        _transform.Translate(direction * speed * Time.deltaTime);
    }

    void Start()
    {
        _transform = transform;
    }
}
