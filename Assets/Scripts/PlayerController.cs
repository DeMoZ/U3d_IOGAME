using System;
using System.Collections;
using System.Collections.Generic;
using TheAttack;
using TheMove;
using UnityEngine;

/// <summary>
/// General script that manages communication between classes
/// </summary>
[RequireComponent(typeof(AttackQueueAction))]
[RequireComponent(typeof(PlayerInputSystem))]
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

    private ChaContMoveRootMotionIAction _iMove;
    private ChaContMoveRootMotionIAction GetIMove
    {
        get
        {
            if (_iMove == null)
                _iMove = GetComponent<ChaContMoveRootMotionIAction>();

            return _iMove;
        }
    }

    private void Awake()
    {
        GetPlayerControllerInputSystem.SubscribeMeOnMoveEvent(GetIMove.Move);

        GetPlayerControllerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackUp, GetAttackQueueAction.AttackUp);
        GetPlayerControllerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackDn, GetAttackQueueAction.AttackDn);
        GetPlayerControllerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackLt, GetAttackQueueAction.AttackLt);
        GetPlayerControllerInputSystem.SubscribeMeOnNoParamEvents(PlayerInputSystem.NoParamEvents.AttackRt, GetAttackQueueAction.AttackRt);
    }
}
