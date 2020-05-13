using Mirror;
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

        private Vector2 _direction = Vector2.zero;
        private Vector2 _velocityEx = Vector2.zero;

        private void Update()
        {
            Vector2 velocity = Vector2.Lerp(_velocityEx, _direction, Time.deltaTime * _acceleration);

            Move(new Vector3(velocity.x, 0, velocity.y));
            _velocityEx = velocity;
        }

        /// <summary>
        /// Method striked from outside class. Sets the direction for movement.
        /// </summary>
        /// <param name="direction"></param>
        public void Move(Vector2 direction)
        {
            _direction = direction;
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
        }
    }
}

