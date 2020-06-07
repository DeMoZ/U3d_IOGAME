using Mirror.Examples.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using TheCamera;
using UnityEngine;

[RequireComponent(typeof(PlayerInputSystem))]
/// <summary>
/// on Player assigment, sets all the dependendies on it
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject _playerCameraPrefab;

    IPlayerCamera _playerCamera;
    IPlayerCamera GetPlayerCamera
    {
        get
        {
            if (!_playerCameraPrefab)
                throw new System.Exception($"Player camera prefab not set in {this}");

            if (_playerCamera == null)
                _playerCamera = Instantiate(_playerCameraPrefab).GetComponent<IPlayerCamera>();

            return _playerCamera;
        }
    }

    PlayerInputSystem _playerInputSystem;

    PlayerInputSystem GetPlayerInputSystem
    {
        get
        {
            if (_playerInputSystem == null)
                _playerInputSystem = GetComponent<PlayerInputSystem>();

            return _playerInputSystem;
        }
    }

    private IControllable _controllable;

    public void SetControllable(IControllable controllable)
    {
        Debug.Log($"{this} SetControllable");

        _controllable = controllable;

        GetPlayerInputSystem.UnsubscribeAll();

        _controllable.Init(GetPlayerInputSystem, GetPlayerCamera);
    }
}
