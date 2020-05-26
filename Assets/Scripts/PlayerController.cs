#define INSTALL_CAMERA

using System;
using System.Collections;
using System.Collections.Generic;
using TheAttack;
using TheMove;
using UnityEngine;
using TheCamera;
using Mirror;

/// <summary>
/// General script that manages communication between classes, generates camera.
/// </summary>
[DisallowMultipleComponent]

[RequireComponent(typeof(AttackQueueAction))]
[RequireComponent(typeof(PlayerInputSystem))]
//[RequireComponent(typeof(IMoveController))]
public class PlayerController : NetworkBehaviour
{
#if INSTALL_CAMERA
    [SerializeField] GameObject _playerCameraPrefab;
#endif
    IPlayerCamera _playerCamera;
    private IPlayerCamera GetPlayerCamera
    {
        get
        {
            if (_playerCamera == null)
                InitCamera();

            return _playerCamera;
        }
    }

    private PlayerInputSystem _playerInputSystem;
    private PlayerInputSystem GetPlayerInputSystem
    {
        get
        {
            if (!_playerInputSystem)
                _playerInputSystem = GetComponent<PlayerInputSystem>();

            return _playerInputSystem;
        }
    }

    private AttackQueueAction _attackQueueAction;
    private AttackQueueAction GetAttackQueueAction
    {
        get
        {
            if (!_attackQueueAction)
                _attackQueueAction = GetComponent<AttackQueueAction>();

            return _attackQueueAction;
        }
    }

    private IMove _iMove;
    private IMove GetMove
    {
        get
        {
            if (_iMove == null)
                _iMove = GetComponent<IMove>();

            return _iMove;
        }
    }

    /// <summary>
    /// Helper class to subscribe iMove inhereited class to move buttons events
    /// </summary>
    public class MoveHelper
    {
        IMove _iMove;
        public MoveHelper(IMove move)
        {
            _iMove = move;
        }

        public void Move(Vector2 vector)
        {
            _iMove.Move(vector);
        }

        public void Turn(Vector3 vector)
        {
            _iMove.Turn(vector);
        }
    }

    /// <summary>
    /// Helper class to subscribe iPlayerCamera inhereited class to move buttons events
    /// </summary>
    public class LookHelper
    {
        IPlayerCamera _iCamera;
        public LookHelper(IPlayerCamera camera)
        {
            _iCamera = camera;
        }

        public void Rotate(Vector2 vector)
        {
            _iCamera.Rotate(vector);
        }
    }

    private void Start()
    {


        //if (isLocalPlayer)
        if (!hasAuthority) return;

        Init();
    }

    private void Init()
    {
#if INSTALL_CAMERA

        InitCamera();
#else
        _cameraT = Camera.main.transform;
#endif

        // if (isLocalPlayer)
        GetPlayerInputSystem.Init(GetPlayerCamera);

        //SubscribeToEvents();
    }

    private void InitCamera()
    {
        if (!_playerCameraPrefab)
            throw new System.Exception($"_ Camera prefab not set for {this}");

        // instantiate player camera
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.LookRotation(transform.position - position, Vector3.up);
        _playerCamera = Instantiate(_playerCameraPrefab, position, rotation).GetComponent<IPlayerCamera>();

        if (_playerCamera == null)
            throw new System.Exception($"PlayerCameraPrefab doesnt have component PlayerCamera");

        _playerCamera.Init(transform, transform);


    }

    private void SubscribeToEvents()
    {
        MoveHelper moveHelper = new MoveHelper(GetMove);

        GetPlayerInputSystem.SubscribeMeOnMoveEvent(moveHelper.Move);

        GetPlayerInputSystem.SubscribeMeOnCameraTurnEvent(moveHelper.Turn);

        GetPlayerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackUp, GetAttackQueueAction.AttackUp);
        GetPlayerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackDn, GetAttackQueueAction.AttackDn);
        GetPlayerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackLt, GetAttackQueueAction.AttackLt);
        GetPlayerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackRt, GetAttackQueueAction.AttackRt);

        // subscribe camera on look attempt event
        GetPlayerInputSystem.SubscribeMeOnLookEvent(GetPlayerCamera.Rotate); 
    }

    private void UnsubscribeFromEvents()
    {
        MoveHelper moveHelper = new MoveHelper(GetMove);

        GetPlayerInputSystem.UnsubscribeMeFromMoveEvent(moveHelper.Move);

        GetPlayerInputSystem.UnsubscribeMeFromCameraTurnEvent(moveHelper.Turn);

        GetPlayerInputSystem.UnsubscribeMeFromNoParamEvents(PlayerInputSystem.NoParamEvents.AttackUp, GetAttackQueueAction.AttackUp);
        GetPlayerInputSystem.UnsubscribeMeFromNoParamEvents(PlayerInputSystem.NoParamEvents.AttackDn, GetAttackQueueAction.AttackDn);
        GetPlayerInputSystem.UnsubscribeMeFromNoParamEvents(PlayerInputSystem.NoParamEvents.AttackLt, GetAttackQueueAction.AttackLt);
        GetPlayerInputSystem.UnsubscribeMeFromNoParamEvents(PlayerInputSystem.NoParamEvents.AttackRt, GetAttackQueueAction.AttackRt);

        // unsubscribe camera on look attempt event
        GetPlayerInputSystem.UnsubscribeMeFromLookEvent(GetPlayerCamera.Rotate);
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void OnDestroy()
    {
        if (_playerCamera != null)
            _playerCamera.Destroy();
    }
}
