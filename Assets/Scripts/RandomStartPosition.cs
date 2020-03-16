using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomStartPosition : MonoBehaviour
{
    private Transform _transform;
    void Start()
    {
        _transform = transform;
        _transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)); 
    }

    
}
