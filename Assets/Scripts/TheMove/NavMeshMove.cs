using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TheMove
{
    /// <summary>
    /// Moving using navmesh agent
    /// </summary>    
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMove : MonoBehaviour, IMove
    {
        [SerializeField] private float _speed = 2;

        private NavMeshAgent _navMeshAgent;
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _speed;

        }

        public void Move(Vector3 direction)
        {
            _navMeshAgent.velocity = direction;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void Move(Vector2 direction)
        {
            throw new System.NotImplementedException();
        }
    }
}
