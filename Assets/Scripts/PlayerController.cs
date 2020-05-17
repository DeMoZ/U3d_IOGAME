using System;
using System.Collections;
using System.Collections.Generic;
using TheAttack;
using TheMove;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// General script that manages communication between classes
/// </summary>
[DisallowMultipleComponent]

[RequireComponent(typeof(AttackQueueAction))]
[RequireComponent(typeof(PlayerInputSystem))]
//[RequireComponent(typeof(IMoveController))]
public class PlayerController : MonoBehaviour
{
    private PlayerInputSystem _playerControllerInputSystem;
    private PlayerInputSystem GetPlayerControllerInputSystem
    {
        get
        {
            if (!_playerControllerInputSystem)
                _playerControllerInputSystem = GetComponent<PlayerInputSystem>();

            return _playerControllerInputSystem;
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

        public void Move(Vector2 vector2)
        {
            _iMove.Move(vector2);
        }

        public void Turn(Vector2 vector2)
        {
            _iMove.Turn(vector2);
        }
    }

    private void Awake()
    {
        MoveHelper moveHelper = new MoveHelper(GetMove);

        GetPlayerControllerInputSystem.SubscribeMeOnMoveEvent(moveHelper.Move);

        GetPlayerControllerInputSystem.SubscribeMeOnCameraTurnEvent(moveHelper.Turn);

        GetPlayerControllerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackUp, GetAttackQueueAction.AttackUp);
        GetPlayerControllerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackDn, GetAttackQueueAction.AttackDn);
        GetPlayerControllerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackLt, GetAttackQueueAction.AttackLt);
        GetPlayerControllerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackRt, GetAttackQueueAction.AttackRt);
    }
}
