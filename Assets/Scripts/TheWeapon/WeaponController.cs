﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheAttack;

namespace TheWeapon
{
    /// <summary>
    /// keeps information about current weapon. Has link to weapon joint transrorm in the body. Knows weapon Collider, joints, etc.
    /// </summary>
    public class WeaponController : MonoBehaviour, IAttack
    {
        [Tooltip("palm right joint in the character body")]
        [SerializeField] private Transform _palmRightJoint;

        [Tooltip("Collider prefab for attack")]
        [SerializeField] private GameObject _attackCollider;
        private Transform _attackColliderT;
        private Transform GetAttackCollider
        {
            get
            {
                if (!_attackColliderT)
                {
                    if (!_attackCollider)
                        throw new System.Exception($"No Attack Collider on {this}");

                    _attackColliderT = Instantiate(_attackCollider).transform;
                }
                return _attackColliderT;
            }
        }

        private Coroutine _colliderRoutine;
        private bool _triggerActive = false;

        private AttackStates _attackStates;

        private AWeapon _currentRightWeapon;
        private void Awake()
        {
            SelfCheck();
            GetCurrentWeapon();

            _currentRightWeapon.SubscribeMeOnHitCollider(TestOnWeaponHit);
        }

        private void SelfCheck()
        {
            if (!_palmRightJoint)
                throw new System.Exception($"PalmRightJoint not set in WeaponController for{gameObject}");
        }

        /// <summary>
        /// Test: Find current armed weapon in right palm joint
        /// in case that the weapon was already armed
        /// </summary>
        private void GetCurrentWeapon()
        {
            if (!_currentRightWeapon)
                _currentRightWeapon = _palmRightJoint.GetComponentInChildren<AWeapon>();
        }


        public void ActivateCollider(bool value)
        {
            if (_currentRightWeapon)
                _currentRightWeapon.ActivateCollider(value);
        }

        public void Attack(string attackStates)
        {

            string[] parsed = attackStates.Split(':');
            if (parsed.Length < 2)
                throw new System.Exception($"Wrong string value from animation event on {gameObject} hould be ' id:PreStart ' ");

            AttackStates state = AttackStates.None;

            System.Enum.TryParse(parsed[1], true, out state);

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
        protected void PreAttack()
        {


        }

        /// <summary>
        /// Damage trigger should be placed here
        /// </summary>
        protected void StartAttack()
        {
            _currentRightWeapon.ActivateCollider(true);
            WeaponCollider();
        }

        /// <summary>
        /// Damage grigger should be removed here
        /// </summary>
        protected void EndAttack()
        {
            _currentRightWeapon.ActivateCollider(false);
            _triggerActive = false;
        }

        /// <summary>
        /// event for movement after attack
        /// </summary>
        protected void PostAttack() { }

        /// <summary>
        /// wrong method name is set in animation event
        /// </summary>
        protected void None() { }

        private void TestOnWeaponHit(Collider other)
        {
            Debug.Log($"Weapon hit someone {other.name}");
        }

        private void WeaponCollider()
        {
            if (_colliderRoutine != null)
            {
                StopCoroutine(_colliderRoutine);
                ResetWeaponCollider();
            }

            _colliderRoutine = StartCoroutine(WeaponColliderPositioning());
        }

        /// <summary>
        /// reset collider properties to inactive status
        /// </summary>
        private void ResetWeaponCollider()
        {
            GetAttackCollider.gameObject.SetActive(false);
        }

        private IEnumerator WeaponColliderPositioning()
        {
            _triggerActive = true;

            GetAttackCollider.position = _palmRightJoint.position;
            GetAttackCollider.rotation = _palmRightJoint.rotation;

            GetAttackCollider.gameObject.SetActive(true);

            while (_triggerActive)
            {
                GetAttackCollider.position = _palmRightJoint.position;
                GetAttackCollider.rotation = _palmRightJoint.rotation;

                yield return null;
            }

            yield return null;

            ResetWeaponCollider();
        }
    }
}
