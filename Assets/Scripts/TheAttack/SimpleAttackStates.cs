﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace TheAttack {

    [RequireComponent(typeof (Animator))]
    public class SimpleAttackStates : Attacks
    {
        [SerializeField] private string _animatorAttackLayer = "Attack";

        private Animator _animator;
        private LayerMask _attackLayer;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            _attackLayer = _animator.GetLayerIndex(_animatorAttackLayer);
        }
        
        void SetLayerWeight(int layer, float weight)
        {
            _animator.SetLayerWeight(layer, weight);
        }

        public override void EndAttack()
        {
           // SetLayerWeight(_attackLayer, 0);
        }

        public override void None()
        {
            throw new System.NotImplementedException();
        }

        public override void PostAttack()
        {
           SetLayerWeight(_attackLayer, 0);
        }

        public override void PreAttack()
        {
           SetLayerWeight(_attackLayer, 1);
        }

        public override void StartAttack()
        {
            //SetLayerWeight(_attackLayer, 1);
        }

       
        /// <summary>
        /// need to check by calling class by the interface iAttack
        /// можно метод задать в интерфейсе и реализовать в абстрактном классе и сделать абстрактным!!!
        /// </summary>
        public void CheckIfInterfaceStillWorks() {
            Debug.Log("for sure it works");
        }
    }
}
