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
        /// weapon collider game object with box dynamic box collider will be generated
        /// </summary>
        private Transform _weaponColliderT;
        private Transform GetWeaponColliderT
        {
            get
            {
                if (!_weaponColliderT)
                {
                    _weaponColliderT = new GameObject().transform;
                    _weaponColliderT.name = "WeaponCollider";
                    _weaponColliderT.parent = transform;
                }

                return _weaponColliderT;
            }
        }

        private readonly object _boxlock = new object();

        private BoxCollider _boxCollider;
        private BoxCollider GetBoxCollider
        {
            get
            {
                if (!_boxCollider)
                {
                    _boxCollider = GetWeaponColliderT.gameObject.AddComponent<BoxCollider>();
                    _boxCollider.isTrigger = true;
                }

                return _boxCollider;
            }
        }

        private Coroutine _colliderRoutine;
        private bool _triggerActive = false;

        private AttackStates _attackStates;

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

        private WeaponCollider _weaponCollider;
        private WeaponCollider GetWeaponCollider
        {
            get
            {
                if (!_weaponCollider)
                {
                    _weaponCollider = GetWeaponColliderT.gameObject.AddComponent<WeaponCollider>();
                }

                return _weaponCollider;
            }
        }

        private Transform _transform;
        private void Awake()
        {
            _transform = transform;

            GetWeaponCollider.SubscribeMeOnHitCollider(OnWeaponTriggerEnter);

            SelfCheck();

            if (!_currentRightWeapon)
                _currentRightWeapon = _palmRightJoint.GetComponentInChildren<AWeapon>();
        }

        private void SelfCheck()
        {
            if (!_palmRightJoint)
                throw new System.Exception($"PalmRightJoint not set in WeaponController for{gameObject}");
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
            GetBoxCollider.enabled = false;
        }

        private IEnumerator WeaponColliderPositioning()
        {
            _triggerActive = true;

            ColliderPositionSizeRotation();

            GetBoxCollider.enabled = true;

            while (_triggerActive)
            {
                ColliderPositionSizeRotation();

                yield return null;
            }

            yield return null;

            ResetWeaponCollider();
        }

        /// <summary>
        /// move and rotate collider on scene acording anamated weapon
        /// </summary>
        private void ColliderPositionSizeRotation()
        {
            Vector3 handlePosition, pikePosition;
            Vector3 size = new Vector3(_colliderWidth, _colliderHeight, 1);
            Quaternion rotation = new Quaternion();

            // collider position
            handlePosition = _currentRightWeapon.GetHandle.position;
            //handlePosition = _transform.position + _transform.InverseTransformPoint(_currentRightWeapon.GetHandle.position);
            handlePosition.y = _transform.position.y;

            pikePosition = _currentRightWeapon.GetPike.position;
            //pikePosition = _transform.position + _transform.InverseTransformPoint(_currentRightWeapon.GetPike.position);
            pikePosition.y = _transform.position.y;

            // collider size 
            size.y = _currentRightWeapon.GetPike.position.y - _transform.position.y;
            size.y = size.y > _colliderHeight ? size.y : _colliderHeight;

            size.z = Vector3.Distance(pikePosition, handlePosition);

            // collider rotation
            rotation = Quaternion.LookRotation(pikePosition - handlePosition);

            // apply
            GetBoxCollider.size = size;
            GetBoxCollider.center = size / 2;
            GetWeaponColliderT.position = handlePosition;
            GetWeaponColliderT.rotation = rotation;
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

        private void OnWeaponTriggerEnter(Collider other)
        {
            Debug.Log($"{gameObject.name} hit {other.name}");
        }
    }
}
