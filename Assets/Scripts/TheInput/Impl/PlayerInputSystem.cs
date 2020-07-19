using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using TheCamera;

namespace TheInput
{
    /// <summary>
    ///  Input based on Imput system
    /// </summary>
    public class PlayerInputSystem : MonoBehaviour, IInputSystem// NetworkBehaviour
    {
        /// <summary>
        /// Event invoked on Camera rotate
        /// </summary>
        public InputGlobals.EventV3 OnCameraRotateEvent;

        /// <summary>
        /// Dictionary for events with no params related to NoParamEvents enum
        /// </summary>
        public Dictionary<InputGlobals.EventsNoParamEnum, UnityEvent> _events = new Dictionary<InputGlobals.EventsNoParamEnum, UnityEvent>();

        /// <summary>
        /// Dictionary for vector2 events related to Vector2Events enum
        /// </summary>
        public Dictionary<InputGlobals.EventsV2Enum, InputGlobals.EventV2> _eventsVector2 = new Dictionary<InputGlobals.EventsV2Enum, InputGlobals.EventV2>();

        /// <summary>
        /// Player Camera script on the camera that follows player body
        /// </summary>
        private IPlayerCamera _playerCamera;

        /// <summary>
        /// the input action file with all actions presets
        /// </summary>
        private TheInputActions _inputActions;

        /// <summary>
        /// Input vector2.magnitude limit from touch move control (separates move and run)
        /// </summary>
        private float _walkRunLimitFromTouch = 0.5f;

        /// <summary>
        /// keeps rotation from previous frame
        /// </summary>
        private Quaternion _rotationEx = new Quaternion();

        private void Awake()
        {
            _inputActions = new TheInputActions();

            _inputActions.PlayerControls.Move.performed += cntx => InvokeEventV2(InputGlobals.EventsV2Enum.Move, cntx.ReadValue<Vector2>());
            _inputActions.PlayerControls.MoveTouch.performed += cntx => InvokeMoveTouchEvent(cntx.ReadValue<Vector2>());

            _inputActions.PlayerControls.AttackUp.performed += ctrl => InvokeUnityEventNoParam(InputGlobals.EventsNoParamEnum.AttackUp);
            _inputActions.PlayerControls.AttackDn.performed += ctrl => InvokeUnityEventNoParam(InputGlobals.EventsNoParamEnum.AttackDn);
            _inputActions.PlayerControls.AttackLt.performed += ctrl => InvokeUnityEventNoParam(InputGlobals.EventsNoParamEnum.AttackLt);
            _inputActions.PlayerControls.AttackRt.performed += ctrl => InvokeUnityEventNoParam(InputGlobals.EventsNoParamEnum.AttackRt);

            _inputActions.PlayerControls.LookMouse.performed += cntx => InvokeEventV2(InputGlobals.EventsV2Enum.Look, cntx.ReadValue<Vector2>());
        }

        /// <summary>
        /// Initialize 
        /// </summary>
        public void Init(IPlayerCamera playerCamera)
        {

            Debug.Log($"Camera initialized on {this}");
            _playerCamera = playerCamera;


        }


        #region Move event

        /// <summary>
        /// THe metod under conctruction
        /// </summary>
        /// <param name="vector2"></param>
        private void InvokeMoveTouchEvent(Vector2 vector2)
        {
            if (_playerCamera == null) return;

            // walk or run from touch
            // Vector normalized if vector.magnitude > 0.5
            // Otherwise half nomalized 

            vector2 = (vector2.sqrMagnitude > _walkRunLimitFromTouch * _walkRunLimitFromTouch) ?
                vector2.normalized :
                vector2.normalized * _walkRunLimitFromTouch;

            vector2 = DirectionCameraRelated(vector2);
            //OnMoveEvent?.Invoke(vector2);

        }

        //public Vector2 _Input;
        //public float _Magnitude;
        //public float _SqrMagnitude;
        //public bool _magMore;
        //public bool _scrMore;
        //private void Update()
        //{
        //    Vector2 vector2 = _Input;

        //    _Magnitude = vector2.magnitude;
        //    _SqrMagnitude = vector2.sqrMagnitude;
        //    _magMore = _Magnitude > 0.5f;
        //    _scrMore = _SqrMagnitude > 0.5f * 0.5f;
        //}


        private Vector2 DirectionCameraRelated(Vector2 input)
        {
            Vector3 velocity = Vector3.zero;
            velocity.x = input.x;
            velocity.z = input.y;

            // camera forward and right vectors:
            Vector3 forward = _playerCamera.GetTransform.forward;
            Vector3 right = _playerCamera.GetTransform.right;

            //project forward and right vectors on the horizontal plane (y = 0)
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            //this is the direction in the world space we want to move:
            velocity = forward * velocity.z + right * velocity.x;

            // velocity magnitude not more than 1
            velocity = Vector3.ClampMagnitude(velocity, 1);
            return new Vector2(velocity.x, velocity.z);
        }

        #endregion

        #region Turn

        /// <summary>
        /// Subscribe a method to Turn event (Vector2)
        /// </summary>
        /// <param name="callback"></param>
        public void SubscribeMeOnCameraTurnEvent(InputGlobals.EventV3 callback)
        {
            OnCameraRotateEvent += callback;
        }

        public void UnsubscribeMeFromCameraTurnEvent(InputGlobals.EventV3 callback)
        {
            OnCameraRotateEvent -= callback;
        }
        #endregion

        #region UnityEvents with no Params
        /// <summary>
        /// Adds events into dictionary. Key is enum variables
        /// </summary>
        private void FillUpDictionaryUnityEventsNoPatam()
        {
            foreach (var e in (InputGlobals.EventsNoParamEnum.GetValues(typeof(InputGlobals.EventsNoParamEnum))))
            {
                _events.Add((InputGlobals.EventsNoParamEnum)e, new UnityEvent());
            }
            // Debug.Log("Event dictionary filled");
        }

        /// <summary>
        /// Subscribe to the event from events enum (no parameters)
        /// </summary>
        /// <param name="noParamEvents"></param>
        /// <param name="callback"></param>
        public void SubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum noParamEvents, UnityAction callback)
        {
            if (_events.Count == 0)
                FillUpDictionaryUnityEventsNoPatam();

            UnityEvent even;
            _events.TryGetValue(noParamEvents, out even);

            even?.AddListener(callback);
        }

        public void UnsubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum noParamEvents, UnityAction callback)
        {
            UnityEvent even;
            _events.TryGetValue(noParamEvents, out even);

            even?.RemoveListener(callback);
        }

        /// <summary>
        /// unsubscribes all callbacks from event by enum name
        /// </summary>
        /// <param name="noParamEvents"></param>
        private void UnsubscribeUnityEventsNoParam(InputGlobals.EventsNoParamEnum noParamEvents)
        {
            UnityEvent even;
            _events.TryGetValue(noParamEvents, out even);

            even?.RemoveAllListeners();
        }
        /// <summary>
        /// Invoke event by enum name
        /// </summary>
        /// <param name="noParamEvents"></param>
        public void InvokeUnityEventNoParam(InputGlobals.EventsNoParamEnum noParamEvents)
        {
            UnityEvent even;
            _events.TryGetValue(noParamEvents, out even);

            even?.Invoke();
        }

        #endregion

        #region Events with Vector2
        private void FillUpDictionaryEventsV2()
        {
            foreach (var e in (InputGlobals.EventsV2Enum.GetValues(typeof(InputGlobals.EventsV2Enum))))
            {
                _eventsVector2.Add((InputGlobals.EventsV2Enum)e, (Vector2) => { });
            }
        }

        public void SubscribeVector2Event(InputGlobals.EventsV2Enum vector2enum, InputGlobals.EventV2 callback)
        {
            if (_eventsVector2.Count == 0)
                FillUpDictionaryEventsV2();

            string str = "";
            foreach (var k in _eventsVector2)
                str += k.Key.ToString() + " ; ";

            _eventsVector2[vector2enum] += callback;
        }
        public void UnsubscribeVector2Event(InputGlobals.EventsV2Enum vector2enum, InputGlobals.EventV2 callback)
        {
            InputGlobals.EventV2 even;
            _eventsVector2.TryGetValue(vector2enum, out even);

            if (even != null)
                even -= callback;
        }

        /// <summary>
        /// unsubscribes all callbacks from event by enum name
        /// </summary>
        /// <param name="vector2enum"></param>
        private void UnsubscribeVector2Event(InputGlobals.EventsV2Enum vector2enum)
        {
            InputGlobals.EventV2 even;
            _eventsVector2.TryGetValue(vector2enum, out even);

            if (even != null)
                even = null;
        }

        private void InvokeEventV2(InputGlobals.EventsV2Enum enu, Vector2 vector)
        {
            InputGlobals.EventV2 even;
            _eventsVector2.TryGetValue(enu, out even);
            even?.Invoke(vector);
        }

        #endregion

        public void UnsubscribeAll()
        {
            foreach (var e in (InputGlobals.EventsV2Enum.GetValues(typeof(InputGlobals.EventsV2Enum))))
                UnsubscribeVector2Event((InputGlobals.EventsV2Enum)e);

            foreach (var e in (InputGlobals.EventsNoParamEnum.GetValues(typeof(InputGlobals.EventsNoParamEnum))))
                UnsubscribeUnityEventsNoParam((InputGlobals.EventsNoParamEnum)e);
        }

        private void OnEnable()
        {
            Debug.Log($"OnEnagle on {this}");
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        private void Update()// возможно бег в стороны надо вынести на класс движения и стороны должны зависить от поворота игрока, в случае с рут анимацией
        {
            if (_playerCamera == null) return;

            // check if camera changed rotation. If true, triggers the event for who may concern
            if (_rotationEx != _playerCamera.GetTransform.rotation)
            {
                Vector3 vector = _playerCamera.GetTransform.forward;
                vector.y = 0;
                OnCameraRotateEvent?.Invoke(vector);

                _rotationEx = _playerCamera.GetTransform.rotation;
            }
        }

        //----------------in Progress





    }
}