using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpot : MonoBehaviour
{
    private Transform _transform;

    /// <summary>
    /// Returns spawnPoint transform.position
    /// </summary>
    public Vector3 GetPosition
    {
        get
        {
            if (!_transform)
                _transform = transform;

            return _transform.position;
        }
    }

    /// <summary>
    /// Returns spawnPoint transform
    /// </summary>
    public Transform GetTransform
    {
        get
        {
            if (!_transform)
                _transform = transform;

            return _transform;
        }
    }

    public void Awake()
    {
        Debug.Log("Should be registered");
        NetworkManager.RegisterStartPosition(transform);
        Debug.Log($"Start positions amount {NetworkManager.startPositions.Count}");
    }

    public void OnDestroy()
    {
        NetworkManager.UnRegisterStartPosition(transform);
    }
}
