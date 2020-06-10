using System;
using System.Collections;
using System.Collections.Generic;
using TheAttack;
using TheMove;
using UnityEngine;
using TheCamera;
using Mirror;
using TheInput;

namespace TheControllable
{
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

        private IInputSystem _inputSystem;
        private IInputSystem GetInputSystem => _inputSystem;

        private AttackQueueAction _attackQueueAction;
        private AttackQueueAction GetAttackQueueAction
        {
            get
            {
                if (_attackQueueAction == null)
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
       
        private void Start()
        {
            if (!hasAuthority) return;

            PlayerManager pm = FindObjectOfType<PlayerManager>();
            pm.SetControllable(this);
        }

        private void SubscribeToEvents()
        {
            if (GetInputSystem == null) return;

            GetInputSystem.SubscribeVector2Event(InputGlobals.EventsV2Enum.Move, GetMove.Move);          

            GetInputSystem.SubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum.AttackUp, GetAttackQueueAction.AttackUp);
            GetInputSystem.SubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum.AttackDn, GetAttackQueueAction.AttackDn);
            GetInputSystem.SubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum.AttackLt, GetAttackQueueAction.AttackLt);
            GetInputSystem.SubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum.AttackRt, GetAttackQueueAction.AttackRt);
        }

        private void UnsubscribeFromEvents()
        {
            if (GetInputSystem == null) return;

            GetInputSystem.UnsubscribeVector2Event(InputGlobals.EventsV2Enum.Move, GetMove.Move);

            // GetInputSystem.UnsubscribeMeFromCameraTurnEvent(GetMove.Turn);

            GetInputSystem.UnsubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum.AttackUp, GetAttackQueueAction.AttackUp);
            GetInputSystem.UnsubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum.AttackDn, GetAttackQueueAction.AttackDn);
            GetInputSystem.UnsubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum.AttackLt, GetAttackQueueAction.AttackLt);
            GetInputSystem.UnsubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum.AttackRt, GetAttackQueueAction.AttackRt);
        }

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        public void Init(IInputSystem input, IPlayerCamera camera)
        {
            Debug.Log($"{this} Init");
            _inputSystem = input;
            _playerCamera = camera;

            SubscribeToEvents();
        }

    }
}