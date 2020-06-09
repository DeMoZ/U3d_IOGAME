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
[RequireComponent(typeof(IMove))]
public class PlayerController : NetworkBehaviour, IControllable
{
    private Transform _transform;
    public Transform GetTransform
    {
        get
        {
            if (!_transform)
                _transform = transform;

            return transform;
        }
    }

    IPlayerCamera _playerCamera;
    private IPlayerCamera GetPlayerCamera => _playerCamera;
   
    private PlayerInputSystem _playerInputSystem;
    private PlayerInputSystem GetPlayerInputSystem => _playerInputSystem;
   
    private AttackQueueAction _attackQueueAction;
    private AttackQueueAction GetAttackQueueAction
    {
        get
        {
            if (_attackQueueAction==null)
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

    /*
    MoveHelper _moveHelper;
    MoveHelper GetMoveHelper
    {
        get
        {
            if (_moveHelper == null)
                _moveHelper = new MoveHelper(GetMove);

            return _moveHelper;
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
    */
    private void Start()
    {
        if (!hasAuthority) return;

        PlayerManager pm = FindObjectOfType<PlayerManager>();
        pm.SetControllable(this);
    }

    private void SubscribeToEvents()
    {
        if (GetPlayerInputSystem == null) return;


        //GetPlayerInputSystem.SubscribeVector2Event(PlayerInputSystem.EventsV2Enum.Move, GetMoveHelper.Move);
        GetPlayerInputSystem.SubscribeVector2Event(PlayerInputSystem.EventsV2Enum.Move, GetMove.Move);

        //GetPlayerInputSystem.SubscribeMeOnCameraTurnEvent(GetMoveHelper.Turn);
        GetPlayerInputSystem.SubscribeMeOnCameraTurnEvent(GetMove.Turn);

        GetPlayerInputSystem.SubscribeUnityEventsNoParam(PlayerInputSystem.EventsNoParamEnum.AttackUp, GetAttackQueueAction.AttackUp);
        GetPlayerInputSystem.SubscribeUnityEventsNoParam(PlayerInputSystem.EventsNoParamEnum.AttackDn, GetAttackQueueAction.AttackDn);
        GetPlayerInputSystem.SubscribeUnityEventsNoParam(PlayerInputSystem.EventsNoParamEnum.AttackLt, GetAttackQueueAction.AttackLt);
        GetPlayerInputSystem.SubscribeUnityEventsNoParam(PlayerInputSystem.EventsNoParamEnum.AttackRt, GetAttackQueueAction.AttackRt);

        GetPlayerInputSystem.SubscribeVector2Event(PlayerInputSystem.EventsV2Enum.Look, GetPlayerCamera.Rotate);
    }

    private void UnsubscribeFromEvents()
    {
        if (GetPlayerInputSystem == null) return;

        //GetPlayerInputSystem.UnsubscribeVector2Event(PlayerInputSystem.EventsV2Enum.Move, GetMoveHelper.Move);
        GetPlayerInputSystem.UnsubscribeVector2Event(PlayerInputSystem.EventsV2Enum.Move, GetMove.Move);

        //GetPlayerInputSystem.UnsubscribeMeFromCameraTurnEvent(GetMoveHelper.Turn);
        GetPlayerInputSystem.UnsubscribeMeFromCameraTurnEvent(GetMove.Turn);

        GetPlayerInputSystem.UnsubscribeUnityEventsNoParam(PlayerInputSystem.EventsNoParamEnum.AttackUp, GetAttackQueueAction.AttackUp);
        GetPlayerInputSystem.UnsubscribeUnityEventsNoParam(PlayerInputSystem.EventsNoParamEnum.AttackDn, GetAttackQueueAction.AttackDn);
        GetPlayerInputSystem.UnsubscribeUnityEventsNoParam(PlayerInputSystem.EventsNoParamEnum.AttackLt, GetAttackQueueAction.AttackLt);
        GetPlayerInputSystem.UnsubscribeUnityEventsNoParam(PlayerInputSystem.EventsNoParamEnum.AttackRt, GetAttackQueueAction.AttackRt);

        GetPlayerInputSystem.UnsubscribeVector2Event(PlayerInputSystem.EventsV2Enum.Look, GetPlayerCamera.Rotate);
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    
    public void Init(PlayerInputSystem input, IPlayerCamera camera)
    {
        Debug.Log($"{this} Init");
        _playerInputSystem = input;
        _playerCamera = camera;

        SubscribeToEvents();
    }

}
