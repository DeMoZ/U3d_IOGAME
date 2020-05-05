using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheAttack
{
    /// <summary>
    /// Script for weapon collider. Applayed on the object with Weapon Controller
    /// </summary>
    public class AttackCollider : MonoBehaviour
    {
        /// <summary>
        /// collider height - minimal height
        /// </summary>
        private float _colliderHeight;
        public float GetColliderHeight
        {
            get { return _colliderHeight; }
            set { _colliderHeight = value; }
        }

        /// <summary>
        /// collider width - constant
        /// </summary>
        private float _colliderWidth;
        public float GetColliderWidth
        {
            get { return _colliderWidth; }
            set { _colliderWidth = value; }
        }

        // Event when weapon model contacted with something
        public delegate void HitTriggered(Collider other);
        public event HitTriggered OnHitTriggered;

        /// <summary>
        /// weapon collider game object with box dynamic box collider will be generated
        /// </summary>
        private Transform _transform;
        private Transform GetTransform
        {
            get
            {
                if (!_transform)
                {
                    _transform = transform;
                }

                return _transform;
            }
        }

        private BoxCollider _boxCollider;
        public BoxCollider GetBoxCollider
        {
            get
            {
                if (!_boxCollider)
                {
                    _boxCollider = gameObject.AddComponent<BoxCollider>();
                    _boxCollider.isTrigger = true;
                }

                return _boxCollider;
            }
        }

        /// <summary>
        /// character game object name. To awoid colliding  with its self
        /// </summary>
        private string _myName;
        public string GetMyName
        {
            set { _myName = value; }
            get { return _myName; }
        }

        public void Initialize(float colliderHeight, float colliderWidth, string name)
        {
            GetColliderHeight = colliderHeight;
            GetColliderWidth = colliderWidth;
            GetMyName = name;
        }

        public void SubscribeMeOnHitCollider(HitTriggered callback)
        {
            OnHitTriggered += callback;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (GetMyName == other.name) return;

            Debug.Log($"!!! {this} OnTriggerEnter other= {other.name}");

            OnHitTriggered?.Invoke(other);
        }


        private void OnDestroy()
        {
            OnHitTriggered = null;
        }

        private void Awake()
        {
        }

        /// <summary>
        /// move and rotate collider on scene acording anamated weapon
        /// </summary>
        public void SetCollider(Transform parent, Transform closePoint, Transform farPoint)
        {
            Vector3 handlePosition, pikePosition;
            Vector3 size = new Vector3(_colliderWidth, _colliderHeight, 1);
            Quaternion rotation = new Quaternion();

            // collider position
            handlePosition = closePoint.position;
            //handlePosition = _transform.position + _transform.InverseTransformPoint(_currentRightWeapon.GetHandle.position);
            handlePosition.y = parent.position.y;

            pikePosition = farPoint.position;
            //pikePosition = _transform.position + _transform.InverseTransformPoint(_currentRightWeapon.GetPike.position);
            pikePosition.y = parent.position.y;

            // collider size 
            size.y = farPoint.position.y - parent.position.y;
            size.y = size.y > _colliderHeight ? size.y : _colliderHeight;

            size.z = Vector3.Distance(pikePosition, handlePosition);

            // collider rotation
            rotation = Quaternion.LookRotation(pikePosition - handlePosition);

            // apply
            GetBoxCollider.size = size;
            GetBoxCollider.center = size / 2;
            GetTransform.position = handlePosition;
            GetTransform.rotation = rotation;
        }

        public void SetReset()
        {
            GetBoxCollider.enabled = false;
            GetBoxCollider.size = Vector3.zero;
        }
    }
}
