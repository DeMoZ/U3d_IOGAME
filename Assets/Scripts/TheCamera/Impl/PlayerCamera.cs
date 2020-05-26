using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCamera
{
    /// <summary>
    /// Camera Setup Script based on Cinemachine virtual camera. The component CinemachineVirtualCamera is required on the camera
    /// </summary>
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class PlayerCamera : MonoBehaviour, IPlayerCamera
    {
        [Tooltip("Dumpling of following offset. w - for YAW (y rotation)")]
        [SerializeField] UnityEngine.Vector3 _followingOffset = new UnityEngine.Vector3(0, 3.54f, -4);

        [Tooltip("Dumpling of following offset. w - for YAW (y rotation)")]
        [SerializeField] UnityEngine.Vector4 _followingOffsetDumpling = new UnityEngine.Vector4(1, 5, 3, 0);

        private Transform _transform;
        public Transform GetTransform => _transform;

        private CinemachineVirtualCamera _cmCamera;
        public CinemachineVirtualCamera GetCmCamera => _cmCamera;

        private CinemachineTransposer _transposer;
        private CinemachineComposer _composer;

        private void Awake()
        {
            _transform = transform;
            _cmCamera = GetComponent<CinemachineVirtualCamera>();
        }

        public void Init(Transform follow, Transform lookAt)
        {
            Debug.Log("Camera Created");

            GetCmCamera.Follow = follow;
            GetCmCamera.LookAt = lookAt;

            float heigh = GetHeigh(lookAt);

            // cashing
            _transposer = GetCmCamera.GetCinemachineComponent<CinemachineTransposer>();
            _composer = GetCmCamera.GetCinemachineComponent<CinemachineComposer>();

            // setting offsets
            _transposer.m_FollowOffset = _followingOffset;

            _transposer.m_XDamping = _followingOffsetDumpling.x;
            _transposer.m_YDamping = _followingOffsetDumpling.y;
            _transposer.m_ZDamping = _followingOffsetDumpling.z;
            _transposer.m_YawDamping = _followingOffsetDumpling.w;

            _composer.m_TrackedObjectOffset = new UnityEngine.Vector3(_composer.m_TrackedObjectOffset.x,
                                                            heigh,
                                                            _composer.m_TrackedObjectOffset.z);
        }

        private float GetHeigh(Transform other)
        {
            float rezult = 1.8f;
            Collider collider = other.GetComponent<Collider>();

            switch (collider)
            {
                case CharacterController gotCollider:
                    // Debug.Log("CharacterController ");
                    rezult = gotCollider.height;
                    break;
                case SphereCollider gotCollider:
                    rezult = gotCollider.radius;
                    break;
                case BoxCollider gotCollider:
                    rezult = gotCollider.size.y;
                    break;
                case MeshCollider gotCollider:
                    rezult = gotCollider.bounds.extents.y;
                    break;
                case CapsuleCollider gotCollider:
                    rezult = gotCollider.height;
                    break;
            }

            return rezult;
        }

        public void Destroy()
        {
            if (gameObject)
                Destroy(gameObject);
        }

        public void Rotate(Vector2 vector)
        {
            _transposer.m_FollowOffset.y += vector.y*Time.deltaTime;

           // Debug.Log($"Rotate . _transposer{_transposer.m_YDamping } ; vector {vector}");
        }
    }
}
