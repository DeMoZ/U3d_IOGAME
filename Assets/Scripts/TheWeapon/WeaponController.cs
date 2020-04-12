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

                    _attackColliderT.parent = transform;

                    _attackColliderT.localScale = GetAttackColliderBounds;

                    ResetWeaponCollider();
                }

                return _attackColliderT;
            }
        }

        private Vector3 _attackColliderBounds = new Vector3(0.1f, 0.1f, 0.1f);
        private Vector3 GetAttackColliderBounds
        {
            get
            {
                if (GetCurrentRightWeapon)
                {
                    if (_attackColliderBounds != _currentRightWeapon.GetColliderBounds)
                        _attackColliderBounds = _currentRightWeapon.GetColliderBounds;
                }
                else
                {
                    Debug.LogError($"gameObject {this.name} has no weapon so fist sized bounds are returned");
                    _attackColliderBounds = new Vector3(0.1f, 0.1f, 0.1f);
                }

                return _attackColliderBounds;
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

        private Transform _transform;
        private void Awake()
        {
            _transform = transform;

            SelfCheck();

            _weaponCollider = GetAttackCollider.GetComponent<WeaponCollider>();
            _weaponCollider.SubscribeMeOnHitCollider(TestOnWeaponHit);

            if (!_currentRightWeapon)
                _currentRightWeapon = _palmRightJoint.GetComponentInChildren<AWeapon>();
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
        public void ActivateCollider(bool value)
        {
            _weaponCollider.ActivateCollider(value);
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
            _weaponCollider.ActivateCollider(true);
            WeaponCollider();
        }

        /// <summary>
        /// Damage grigger should be removed here
        /// </summary>
        protected void EndAttack()
        {
            _weaponCollider.ActivateCollider(false);
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

            // world position of palm invers in body trnasfom local position
            ColliderPositioning();

            //GetAttackCollider.rotation = _palmRightJoint.rotation;

            GetAttackCollider.gameObject.SetActive(true);

            while (_triggerActive)
            {
                ColliderPositionSizeRotation();

                yield return null;
            }

            yield return null;

            ResetWeaponCollider();
        }

        /// <summary>
        /// world position of palm invers in body trnasfom local position
        /// </summary>
        private void ColliderPositioning()
        {
            Vector3 position;

            position = _transform.position + _transform.InverseTransformPoint(_palmRightJoint.position);
            position.y = _transform.position.y;
            GetAttackCollider.position = position;
        }

        private void ColliderPositionSizeRotation()
        {
            Vector3 handlePosition, pikePosition;
            Vector3 size = new Vector3(_colliderWidth, _colliderHeight, 1);
            Quaternion rotation;

            // collider position
            handlePosition = _transform.position + _transform.InverseTransformPoint(_currentRightWeapon.GetHandle.position);
            handlePosition.y = _transform.position.y;

            pikePosition = _transform.position + _transform.InverseTransformPoint(_currentRightWeapon.GetPike.position);
            pikePosition.y = _transform.position.y;

            // collider size
            size.y = (_currentRightWeapon.GetPike.position - _transform.position).y;
            size.y = size.y > _colliderHeight ? size.y : _colliderHeight;
            
            size.z = Vector3.Distance(pikePosition, handlePosition);

            // collider rotation
            rotation = Quaternion.LookRotation(pikePosition - handlePosition);

            // apply
            GetAttackCollider.position = handlePosition;
            GetAttackCollider.localScale = size;
            GetAttackCollider.rotation = rotation;
        }

        //Vector3? _gismopos;
        //private void OnDrawGizmos()
        //{
        //    if (_gismopos != null)
        //    {
        //        Color c = Color.red;
        //        Gizmos.color = c;
        //        Gizmos.DrawSphere((Vector3)_gismopos, 10f);
        //    }
        //}
    }
}
