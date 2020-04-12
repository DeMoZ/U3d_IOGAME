using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWeapon
{
    /// <summary>
    /// Script for weapon collider. Applayed on the object with Weapon Controller
    /// </summary>
    public class WeaponCollider : MonoBehaviour, IWeapon
    {
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
            OnHitTriggered?.Invoke(other);
        }


        private void OnDestroy()
        {
            OnHitTriggered = null;
        }

        private void Awake()
        {
        }


        public void Atack()
        {
            throw new System.NotImplementedException();
        }


    }
}
