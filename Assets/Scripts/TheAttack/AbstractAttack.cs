using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheAttack
{
    /// <summary>
    /// General state machine for attack animation end behaviour
    /// </summary>
    abstract public class AbstractAttack : MonoBehaviour, IAttack
    {
        [Tooltip("The unic name for the animation. Can be an animation name")]
        [SerializeField] protected string _idName;

        [SerializeField] protected string _animatorAttackLayer = "Attack";

        private LayerMask _attackLayer;

        public string GetIdName => _idName;

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
            _attackLayer = GetAnimator.GetLayerIndex(_animatorAttackLayer);
        }

        protected void SetLayerWeight(int layer, float weight)
        {
            GetAnimator.SetLayerWeight(layer, weight);
        }

        /// <summary>
        /// Class should receive events from animations with string names and parce it
        /// </summary>
        /// <param name="attackStates"></param>
        public void Attack(string attackStates)
        {
            string[] parsed = attackStates.Split(':');
            if (parsed.Length < 2)
                throw new System.Exception($"Wrong string value from animation event on {gameObject} hould be ' id:PreStart ' ");

            // if first parth of string value
            if (_idName != parsed[0]) return;

            AttackStates state = AttackStates.None;

            System.Enum.TryParse(parsed[1], true, out state);

            // Debug.Log($"enum = {state}");
            Attack(state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void Attack(AttackStates state)
        {
            switch (state)
            {
                case AttackStates.PreAttack:
                    PreAttack();
                    break;

                case AttackStates.StartAttack:
                    StartAttack();
                    break;

                case AttackStates.EndAttack:
                    EndAttack();
                    break;

                case AttackStates.PostAttack:
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
            SetLayerWeight(_attackLayer, 1);
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
            SetLayerWeight(_attackLayer, 0.001f);

        }

        /// <summary>
        /// wrong method name is set in animation event
        /// </summary>
        protected virtual void None() { }
    }
}
