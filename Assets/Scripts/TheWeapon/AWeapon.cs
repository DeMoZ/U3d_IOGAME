using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWeapon
{
    public class AWeapon : MonoBehaviour//, IWeapon
    {
        [SerializeField] private Vector3 _colliderBounds = Vector3.one;
        public Vector3 GetColliderBounds => _colliderBounds;

        [SerializeField] private float _attack = 1;
        public float GetAttack => _attack;       

        [Tooltip("Object points the place where weapon ends")]
        [SerializeField] private Transform _pike;
        public Transform GetPike => _pike;

        [Tooltip("Object points the place where weapon is nagdled by")]
        [SerializeField] private Transform _handle;
        public Transform GetHandle => _handle;

        private void Awake()
        {
            if (!_handle)
                throw new System.Exception($"the weapon {name} not properly set");

            if (!_pike)
                throw new System.Exception($"the weapon {name} not properly set");
        }
    }
}
