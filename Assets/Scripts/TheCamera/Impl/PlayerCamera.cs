using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheCamera
{
    /// <summary>
    /// Camera Setup Script based on Cinemachine Free Look. The component CinemachineVirtualCamera is required on the camera
    /// </summary>
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class PlayerCamera : MonoBehaviour, IPlayerCamera
    {
        [Tooltip("Recalculate rigs heights if checked")]
        [SerializeField] bool _calculateRigsHeight = false;

        [Tooltip("Recalculate rigs radius if checked")]
        [SerializeField] bool _calculateRigsRadius = false;

        [Tooltip("Recalculate target follow offset height if checked")]
        [SerializeField] bool _calculateFollowHeight = false;
        
        private Transform _transform;
        public Transform GetTransform => _transform;

        private CinemachineFreeLook _cmCamera;
        public CinemachineFreeLook GetCmCamera => _cmCamera;

        private CinemachineComposer _topComposer;
        private CinemachineComposer _middleComposer;
        private CinemachineComposer _bottomComposer;

        private void Awake()
        {
            _transform = transform;
            _cmCamera = GetComponent<CinemachineFreeLook>();
            _topComposer = _cmCamera.GetRig(0).GetCinemachineComponent<CinemachineComposer>();
            _middleComposer = _cmCamera.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
            _bottomComposer = _cmCamera.GetRig(2).GetCinemachineComponent<CinemachineComposer>();
        }

        public void Init(Transform follow, Transform lookAt)
        {
            Debug.Log($"Camera Created, follow {follow} ; lookAt {lookAt}");

            GetCmCamera.Follow = follow;
            GetCmCamera.LookAt = lookAt;

            // set rigs
            float heigh = GetHeigh(lookAt);

            if (_calculateRigsHeight)
            {
                // Heights and radiused of rings
                GetCmCamera.m_Orbits[0].m_Height = heigh + 0.2f;
                GetCmCamera.m_Orbits[1].m_Height = heigh / 2;
                GetCmCamera.m_Orbits[2].m_Height = heigh / 6;
            }

            // TODO: need formula to calculate Rigs radius
            if (_calculateRigsRadius) { 
                // GetCmCamera.m_Orbits[0].m_Radius=
                // GetCmCamera.m_Orbits[1].m_Radius =
                // GetCmCamera.m_Orbits[2].m_Radius =
            }

            if (_calculateFollowHeight)
            {
                Vector3 followOffset = new Vector3(0, heigh, 0);

                _topComposer.m_TrackedObjectOffset = followOffset;
                _middleComposer.m_TrackedObjectOffset = followOffset;
                _bottomComposer.m_TrackedObjectOffset = followOffset;
            }           
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
            GetCmCamera.m_YAxis.Value += vector.y * Time.deltaTime;// * _speed.y;
            GetCmCamera.m_XAxis.Value += vector.x * Time.deltaTime;// * _speed.x;
        }
    }
}
