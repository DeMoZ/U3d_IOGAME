using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGlobal;

namespace TheAttack
{
    /// <summary>
    /// General state machine for attack animation end behaviour
    /// </summary>
     public class AttackAnimation : MonoBehaviour, IAttack
    {
        [Tooltip("The unic name for the animation. Can be an animation name")]
        [SerializeField] protected GlobalEnums.AnimationNamesIDs _idName;
        public GlobalEnums.AnimationNamesIDs GetIdName => _idName;

        [SerializeField] protected GlobalEnums.AnimatorLayers _animatorAttackLayer = GlobalEnums.AnimatorLayers.Attack;

        private LayerMask _attackLayer;


        protected Animator _animator;
        public Animator GetAnimator
        {
            get
            {
                if (!_animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
            set { _animator = value; }
        }

        private void Start()
        {
            _attackLayer = GetAnimator.GetLayerIndex(_animatorAttackLayer.ToString());
        }

        //protected void SetLayerWeight(int layer, float weight)
        //{
        //    GetAnimator.SetLayerWeight(layer, weight);
        //}

        /// <summary>
        /// Class should receive events from animations with string names and parce it
        /// </summary>
        /// <param name="attackStates"></param>
        public void Attack(AnimationAttackEvent value)
        {           
            GlobalEnums.AttackStates state = value.GetAttackState;

            AttackState(state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void AttackState(GlobalEnums.AttackStates state)
        {
            Debug.Log($"State received {state} at {this}");

            switch (state)
            {
                case GlobalEnums.AttackStates.Warm:
                    PreAttack();
                    break;

                case GlobalEnums.AttackStates.Hit:
                    StartAttack();
                    break;

                case GlobalEnums.AttackStates.Hold:
                    EndAttack();
                    break;

                case GlobalEnums.AttackStates.Cold:
                    PostAttack();
                    break;

                default:        // IAttack.AttackStates.None
                    None();
                    break;
            }
        }

        /// <summary>
        /// event for movement befoure attack
        /// </summary>
        protected virtual void PreAttack()
        {
           // SetLayerWeight(_attackLayer, 1);
        }

        /// <summary>
        /// Damage trigger should be placed here
        /// </summary>
        protected virtual void StartAttack() { }

        /// <summary>
        /// Damage grigger should be removed here
        /// </summary>
        protected virtual void EndAttack() { }

        /// <summary>
        /// event for movement after attack
        /// </summary>
        protected virtual void PostAttack()
        {
           // SetLayerWeight(_attackLayer, 0.001f);

        }

        /// <summary>
        /// wrong method name is set in animation event
        /// </summary>
        protected virtual void None() { }
    }
}
