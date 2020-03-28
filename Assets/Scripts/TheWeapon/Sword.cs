using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheWeapon {
    [RequireComponent(typeof(Animator))]
    public class Sword : MonoBehaviour,IWeapon
    {        
        [SerializeField] private float _attack = 1;
        // component on Weapon model  (child)
        private WeaponModel _weaponModel;


        public float GetAttack => _attack;

        public void Atack()
        {
         //   _weaponModel.GetAllowTrigger = true;
        }

        //public void Position()
        //{
        //    _weaponModel.GetAllowTrigger = false;
        //}

        void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _weaponModel = GetComponentInChildren<WeaponModel>();
            if (!_weaponModel)
                throw new System.Exception($"Ther are no child model with WeaponModel script for {this}");

            _weaponModel.SubscribeMeOnHitCollider(RigisterModelBeingHit);
        }

        private void RigisterModelBeingHit(Collider other)
        {
            Debug.Log($" sword hit {other.name}");
        }



        
    }
}
