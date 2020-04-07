using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace TheAttack
{

    [RequireComponent(typeof(Animator))]
    public class Attacks : AbstractAttack
    {
        [SerializeField] private string _idName = "Simple";
        public string GetIdName => _idName;

        [SerializeField] private string _animatorAttackLayer = "Attack";

        private Animator _animator;
        public Animator GetAnimator
        {
            get
            {
                if (!_animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }

        private LayerMask _attackLayer;

        public virtual void Attack(string attackStates)
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
        private void Start()
        {
            _attackLayer = GetAnimator.GetLayerIndex(_animatorAttackLayer);
        }

        void SetLayerWeight(int layer, float weight)
        {
            GetAnimator.SetLayerWeight(layer, weight);
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
            SetLayerWeight(_attackLayer, 0.001f);
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
        public void CheckIfInterfaceStillWorks()
        {
            Debug.Log("for sure it works");
        }
    }
}
