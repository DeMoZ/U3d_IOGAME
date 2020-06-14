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
                GetCmCamera.m_Orbits[0].m_Height = heigh * 2f;
                GetCmCamera.m_Orbits[1].m_Height = heigh / 2;
                GetCmCamera.m_Orbits[2].m_Height = heigh / 6;
            }

            // TODO: need formula to calculate Rigs radius
            if (_calculateRigsRadius)
            {
                GetCmCamera.m_Orbits[0].m_Radius = heigh / 2;
                GetCmCamera.m_Orbits[1].m_Radius = heigh * 2;
                GetCmCamera.m_Orbits[2].m_Radius = heigh / 2;
            }

            if (_calculateFollowHeight)
            {
                _topComposer.m_TrackedObjectOffset = new Vector3(_topComposer.m_TrackedObjectOffset.x, heigh, _topComposer.m_TrackedObjectOffset.z);
                _middleComposer.m_TrackedObjectOffset = new Vector3(_middleComposer.m_TrackedObjectOffset.x, heigh, _middleComposer.m_TrackedObjectOffset.z);
                _bottomComposer.m_TrackedObjectOffset = new Vector3(_bottomComposer.m_TrackedObjectOffset.x, heigh, _bottomComposer.m_TrackedObjectOffset.z);
            }

            // set camera at the back of character
            float angle = Vector3.SignedAngle(GetTransform.forward, follow.forward, Vector3.up);
            // GetCmCamera.m_XAxis.Value -= angle;
            Debug.Log($"angle = {angle}, \n cam forw = {GetTransform.forward}, obj forw = {follow.forward}");

        }

        public Vector3 CamForward;
        private void Update()
        {
            CamForward = GetTransform.forward;
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
            // rotate around Y axis
            //GetCmCamera.m_XAxis.Value += vector.x * Time.deltaTime;// * _speed.x;
            GetCmCamera.m_XAxis.m_InputAxisValue = vector.x * Time.deltaTime;

            // rotate around X axis
            //GetCmCamera.m_YAxis.Value += vector.y * Time.deltaTime;// * _speed.y;
            GetCmCamera.m_YAxis.m_InputAxisValue = vector.y * Time.deltaTime;
        }
    }
}
