using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWeapon
{
    public class WeaponCollider : MonoBehaviour, IWeapon
    {
        private bool _allowTrigger = true;
        public bool GetAllowTrigger => _allowTrigger;

        //private MeshCollider _meshCollider;
        //public MeshCollider GetMeshCollider
        private BoxCollider _meshCollider;
        public BoxCollider GetMeshCollider
        {
            get
            {
                if (!_meshCollider)
                    _meshCollider = GetComponent<BoxCollider>();
                //_meshCollider = GetComponent<MeshCollider>();

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
                OnHitTriggered?.Invoke(other);
        }


        private void OnDestroy()
        {
            OnHitTriggered = null;
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
            _meshCollider = GetComponent<BoxCollider>();
            //_meshCollider = GetComponent<MeshCollider>();

            if (!_meshCollider) throw new System.Exception($"There is no MeshCollider on {gameObject}");

            ActivateCollider(false);
        }

        public void Atack()
        {
            throw new System.NotImplementedException();
        }


    }
}
