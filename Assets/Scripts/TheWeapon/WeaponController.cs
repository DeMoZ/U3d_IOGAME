using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheAttack;

namespace TheWeapon
{
    /// <summary>
    /// keeps information about current weapon. Has link to weapon joint transrorm in the body.
    /// Knows weapon Collider, joints, etc.
    /// </summary>
    public class WeaponController : MonoBehaviour, IAttack
    {
        [Tooltip("collider height - minimal height")]
        [SerializeField] private float _colliderHeight = 1.8f;
        [Tooltip("collider width - constant")]
        [SerializeField] private float _colliderWidth = 0.2f;

        [Tooltip("palm right joint in the character body")]
        [SerializeField] private Transform _palmRightJoint;

        /// <summary>
        /// attack collider script on attack collider object
        /// </summary>
        private AttackCollider _attackCollider;
        private AttackCollider GetAttackCollider
        {
            get
            {
                if (!_attackCollider)
                    CreateWeaponCollider();

                return _attackCollider;
            }
        }

        /// <summary>
        /// Attack collider is active during routine
        /// </summary>
        private Coroutine _colliderRoutine;
        private bool _triggerActive = false;

        private AWeapon _currentRightWeapon;
        private AWeapon GetCurrentRightWeapon
        {
            get
            {
                if (!_currentRightWeapon)
                    _currentRightWeapon = _palmRightJoint.GetComponentInChildren<AWeapon>();

                return _currentRightWeapon;
            }
        }

        private Transform _transform;
        private void Awake()
        {
            _transform = transform;

            CreateWeaponCollider();

            if (!_palmRightJoint)
                throw new System.Exception($"PalmRightJoint not set in WeaponController for{gameObject}");            
        }
        private void CreateWeaponCollider()
        {
            GameObject go = new GameObject();
            go.name = "WeaponCollider";
            go.transform.parent = transform;

            _attackCollider = go.AddComponent<AttackCollider>();
            _attackCollider.Initialize(_colliderHeight, _colliderWidth);
            _attackCollider.SubscribeMeOnHitCollider(TestOnWeaponHit);
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
            WeaponCollider();
        }

        /// <summary>
        /// Damage grigger should be removed here
        /// </summary>
        protected void EndAttack()
        {
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
              
        private void WeaponCollider()
        {
            if (_colliderRoutine != null)
            {
                StopCoroutine(_colliderRoutine);
                GetAttackCollider.SetReset();
            }

            _colliderRoutine = StartCoroutine(WeaponColliderPositioning());
        }
       
        private IEnumerator WeaponColliderPositioning()
        {
            _triggerActive = true;

            GetAttackCollider.SetCollider(_transform, GetCurrentRightWeapon.GetHandle, GetCurrentRightWeapon.GetPike);

            GetAttackCollider.GetBoxCollider.enabled = true;

            while (_triggerActive)
            {
                GetAttackCollider.SetCollider(_transform, GetCurrentRightWeapon.GetHandle, GetCurrentRightWeapon.GetPike);

                yield return null;
            }

            yield return null;

            GetAttackCollider.SetReset();
        }
               
        //private void Update()
        //{
        //    if (_triggerActive)
        //    {
        //        ColliderPositionSizeRotation();
        //    }
        //}

        //private void TriggerEnable(bool active)
        //{
        //    ColliderPositionSizeRotation();

        //    _triggerActive = active;
        //    GetBoxCollider.enabled = active;
        //}

        private void TestOnWeaponHit(Collider other)
        {
            Debug.Log($"{gameObject.name} hit {other.name}");
        }
    }
}
