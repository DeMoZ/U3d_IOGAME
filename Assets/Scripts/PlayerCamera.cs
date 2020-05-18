using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class PlayerCamera : MonoBehaviour
{
    private Transform _transform;
    public Transform GetTransform => _transform;

    private CinemachineVirtualCamera _cmCamera;
    public CinemachineVirtualCamera GetCmCamera => _cmCamera;
    private void Awake()
    {
        _transform = transform;
        _cmCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void Initialize(Transform follow,Transform lookAt)
    {
        GetCmCamera.Follow = follow;
        GetCmCamera.LookAt = lookAt;
    }
}
