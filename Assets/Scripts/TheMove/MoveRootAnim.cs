using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace TheMove
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ActionStateMachine))]
    public class MoveRootAnim : NetworkBehaviour, IMove, IAction
    {
        [Tooltip("Character move speed acceleration")]
        [SerializeField] float _acceleration = 3;

        [Tooltip("Field name in animator controller to move Right / Left")]
        [SerializeField] private string _animatorMoveRight = "MoveRight";
        [Tooltip("Field name in animator controller to move Forward / Backward")]
        [SerializeField] private string _animatorMoveForward = "MoveForward";
        [Tooltip("Field name in animator controller to Turn")]
        [SerializeField] private string _animatorTurn = "Turn";

        private ActionStateMachine _actionStateMachine;
        private ActionStateMachine GetActionStateMachine
        {
            get
            {
                if (!_actionStateMachine)
                    _actionStateMachine = GetComponent<ActionStateMachine>();

                return _actionStateMachine;
            }
        }

        private Animator _animator;
        private Animator GetAnimator
        {
            get
            {
                if (!_animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }

        private Transform _transform;
        private Transform GetTransform
        {
            get
            {
                if (!_transform)
                    _transform = transform;

                return _transform;
            }
        }
        /// <summary>
        /// received direction related to camera. Not related to character direction.
        /// </summary>
        private Vector3 _direction = Vector2.zero;
        private Vector3 _velocityEx = Vector3.zero;

        /// <summary>
        /// Method striked from outside class. Sets the direction for movement.
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Vector2 direction)
        {
            _direction = new Vector3(direction.x, 0, direction.y);
        }

        public float angle;
        public Vector3 transformForward;
        private void Update()
        {
            Vector3 velocity = GetTransform.InverseTransformDirection(_direction);
            velocity = Vector3.Lerp(_velocityEx, velocity, Time.deltaTime * _acceleration);

            Move(velocity);
            _velocityEx = velocity;
        }

        /// <summary>
        /// Mthod 
        /// </summary>
        /// <param name="direction"></param>
        private void Move(Vector3 direction)
        {
            CmdMoveAnimation(direction);
        }

        [Command]
        private void CmdMoveAnimation(Vector3 direction)
        {
            RpcMoveAnimation(direction);
        }

        [ClientRpc]
        private void RpcMoveAnimation(Vector3 direction)
        {
            GetAnimator.SetFloat(_animatorMoveForward, direction.z);
            GetAnimator.SetFloat(_animatorMoveRight, direction.x);
        }

        /// <summary>
        /// inheraited from IAction interface
        /// </summary>
        /// <returns></returns>
        public bool IsInAction()
        {
            // returns true if some kind of jumping of something like that
            // but for now it always false

            return false;
        }

        public Vector2 _TurnCheck = new Vector2();
        public void Turn(Vector2 rotation)
        {
            _TurnCheck = rotation;

            CmdTurnAnimation(rotation.y);
        }

        [Command]
        private void CmdTurnAnimation(float value)
        {
            RpcTurnAnimation(value);
        }

        [ClientRpc]
        private void RpcTurnAnimation(float value)
        {
            //   GetAnimator.SetFloat(_animatorTurn, value);
        }
    }
}

