using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWeapon
{
    public class AWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private float _attack = 1;
        public float GetAttack => _attack;
       
        private bool _allowTrigger=true;
        public bool GetAllowTrigger => _allowTrigger;

        private MeshCollider _meshCollider;
        public MeshCollider GetMeshCollider
        {
            get
            {
                if (!_meshCollider)
                    _meshCollider = GetComponent<MeshCollider>();

                return _meshCollider;
            }
        }

        // Event when weapon model contacted with something
        public delegate void HitTriggered(Collider other);
        public event HitTriggered OnHitTriggered;
        public void SubscribeMeOnHitCollider(HitTriggered callback)
        {
            OnHitTriggered += callback;
        }


        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{this} OnTriggerEnter other= {other.name}");
            // if condition
            if (_allowTrigger)
                OnHitTriggered.Invoke(other);
        }

        //private void OnCollisionEnter(Collision other)
        //{
        //    Debug.Log($"{this} OnCollisionEnter other= {other.gameObject.name}");

        //}

        private void OnDestroy()
        {
            OnHitTriggered = null;
        }

        public void Atack()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Activate / Disactivate weapon collider
        /// </summary>
        /// <param name="value"></param>
        public void ActivateCollider(bool value)
        {
            GetMeshCollider.enabled = value;
        }

        private void Awake()
        {
            Initialize();
        }

        protected void Initialize()
        {          
                _meshCollider = GetComponent<MeshCollider>();

            if (!_meshCollider) throw new System.Exception($"There is no MeshCollider on {gameObject}");

                ActivateCollider(false);
        }
    }
}
