﻿using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMove
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(ActionStateMachine))]
    public class ChaContMoveRootMotionIAction : NetworkBehaviour, IMove, IAction
    {
        [Tooltip("Hero move speed")]
        [SerializeField] private float _speed = 2;
        [Tooltip("Field name in animator controller to move Right / Left")]
        [SerializeField] private string _animatorMoveRight = "MoveRight";
        [Tooltip("Field name in animator controller to move Forward / Backward")]
        [SerializeField] private string _animatorMoveForward = "MoveForward";

        private ActionStateMachine _actionStateMachine;
        private CharacterController _characterController;
        private Animator _animator;
        private Transform _transform;

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

        void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _actionStateMachine = GetComponent<ActionStateMachine>();
            _transform = transform;

            // subscribe to move events
            GetPlayerControllerInputSystem.SubscribeMeOnMoveEvent(Move);
        }

        /// <summary>
        /// intermediate method to translate v2 to v3
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Vector2 direction)
        {
            Move(new Vector3(direction.x, 0, direction.y));
        }

        public void Move(Vector3 direction)
        {
            if (!_actionStateMachine.AllowAction(this)) return;

            Debug.Log($"Going to move, direction {direction}");
            // _characterController.Move(direction * _speed * Time.deltaTime);

            // Vector3 inverseTransform = _transform.InverseTransformDirection(_characterController.velocity);

            Vector3 inverseTransform = _transform.InverseTransformDirection(direction * Time.deltaTime * _speed);

            CmdMoveAnimation(inverseTransform);
        }

        [Command]
        private void CmdMoveAnimation(Vector3 direction)
        {
            RpcMoveAnimation(direction);
        }

        [ClientRpc]
        private void RpcMoveAnimation(Vector3 direction)
        {
            _animator.SetFloat(_animatorMoveForward, direction.z);
            _animator.SetFloat(_animatorMoveRight, direction.x);
        }

        public bool IsInAction()
        {
            // returns true if some kind of jumping of something like that
            // but for now it always false

            return false;
        }



        //void IMove.Move(Vector2 direction)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}

