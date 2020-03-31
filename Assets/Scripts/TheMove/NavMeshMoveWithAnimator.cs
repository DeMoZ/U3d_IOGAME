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
        [SerializeField] private float _speed = 2;

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
            //_animator.SetFloat("MoveForward", _navMeshAgent.velocity.z);
            //_animator.SetFloat("MoveRight", _navMeshAgent.velocity.z);

            Vector3 directionRelatedBody = _transform.TransformDirection(_navMeshAgent.velocity);

            Debug.DrawLine(_transform.position, _transform.position + directionRelatedBody);
             
            RpcMoveAnimation(directionRelatedBody);
        }

        [Command]
        private void CmdMoveAnimation(Vector3 direction)
        {
            RpcMoveAnimation(direction);
        }

        [ClientRpc]
        private void RpcMoveAnimation(Vector3 direction)
        {
            _animator.SetFloat("MoveForward", direction.z);
            _animator.SetFloat("MoveRight", direction.z);
        }
    }
}
