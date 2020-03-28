using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWeapon
{
    /// <summary>
    /// script is required to be on weapon model to callback on collsion while hit
    /// </summary>
    public class WeaponModel : MonoBehaviour
    {
        private bool _allowTrigger;
        public bool GetAllowTrigger => _allowTrigger;

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
    }
}
