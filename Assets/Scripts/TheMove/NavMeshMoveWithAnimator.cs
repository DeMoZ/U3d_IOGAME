using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

namespace TheMove
{
    /// <summary>
    /// Moving using navmesh agent and animation Controller
    /// </summary>    
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMoveWithAnimator : NetworkBehaviour, IMove
    {
        [Tooltip("Hero move speed")]
        [SerializeField] private float _speed = 4;

        [Tooltip("Field name in animator controller to move Right / Left")]
        [SerializeField] private string _animatorMoveRight = "MoveRight";
        [Tooltip("Field name in animator controller to move Forward / Backward")]
        [SerializeField] private string _animatorMoveForward = "MoveForward";

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Transform _transform;

        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _transform = transform;

            _navMeshAgent.speed = _speed;
        }

        public void Move(Vector3 direction)
        {
            _navMeshAgent.velocity = direction;

            Vector3 inverseTransform = _transform.InverseTransformDirection(_navMeshAgent.velocity);

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

        public void Move(Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        //private Vector3 VelocityRelatedTransform(Transform objectTransform, Vector3 vector)
        //{
        //    Vector3 velocity = vector;

        //    // camera forward and right vectors:
        //    Vector3 forward = objectTransform.forward;
        //    Vector3 right = objectTransform.right;

        //    //project forward and right vectors on the horizontal plane (y = 0)
        //    forward.y = 0f;
        //    right.y = 0f;
        //    forward.Normalize();
        //    right.Normalize();

        //    //this is the direction in the world space we want to move:
        //    velocity = forward * velocity.z + right * velocity.x;

        //    // velocity magnitude not more than 1
        //    velocity = Vector3.ClampMagnitude(velocity, 1);

        //    return velocity;
        //}
    }
}
