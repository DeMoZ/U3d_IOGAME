//#define INSTALL_CAMERA
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TheInput;
using UnityEngine.Events;
using TheCamera;

/// <summary>
///  Input based on Imput system
/// </summary>
public class PlayerInputSystem : MonoBehaviour// NetworkBehaviour
{
    public delegate void Vector2Event(Vector2 vector2);
    public delegate void Vector3Event(Vector3 vector3);
    /// <summary>
    /// Event invoked on Move Arrows pressed
    /// </summary>
    public Vector2Event OnMoveEvent;

    /// <summary>
    ///  event invoked on attempt to look (Rotate Camera)
    /// </summary>
    public Vector2Event OnLookEvent;

    /// <summary>
    /// Event invoked on Camera rotate
    /// </summary>
    public Vector3Event OnCameraRotateEvent;

    /// <summary>
    /// All no parameters events 
    /// </summary>
    public enum NoParamEvents
    {
        AttackUp,
        AttackDn,
        AttackLt,
        AttackRt,
    }

    /// <summary>
    /// Dictionary for events with no params related to NoParamEvents enum
    /// </summary>
    public Dictionary<NoParamEvents, UnityEvent> _events = new Dictionary<NoParamEvents, UnityEvent>();

    /// <summary>
    /// Player Camera script on the camera that follows player body
    /// </summary>
    private IPlayerCamera _playerCamera;

    /// <summary>
    /// the input action file with all actions presets
    /// </summary>
    private TheInputActions _inputActions;

    private Vector3 _direction = Vector3.zero;

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

        _inputActions.PlayerControls.Move.performed += cntx => InvokeMoveEvent(cntx.ReadValue<Vector2>());
        _inputActions.PlayerControls.MoveTouch.performed += cntx => InvokeMoveTouchEvent(cntx.ReadValue<Vector2>());

        //_inputActions.PlayerControls.MouseMove.performed += cntx => InvokeMouseMoveEvent(cntx.ReadValue<Vector2>());

        _inputActions.PlayerControls.AttackUp.performed += ctrl => InvokeNoParamEvents(NoParamEvents.AttackUp);
        _inputActions.PlayerControls.AttackDn.performed += ctrl => InvokeNoParamEvents(NoParamEvents.AttackDn);
        _inputActions.PlayerControls.AttackLt.performed += ctrl => InvokeNoParamEvents(NoParamEvents.AttackLt);
        _inputActions.PlayerControls.AttackRt.performed += ctrl => InvokeNoParamEvents(NoParamEvents.AttackRt);

        // Look
        _inputActions.PlayerControls.LookMouse.performed += cntx => InvokeLookEvent(cntx.ReadValue<Vector2>());

    }

    /// <summary>
    /// Initialize 
    /// </summary>
    public void Init(IPlayerCamera playerCamera)
    {
        _playerCamera = playerCamera;
    }


    #region Move event
    private void InvokeMoveEvent(Vector2 vector2)
    {
        if (_playerCamera == null) return;

        vector2 = DirectionCameraRelated(vector2);
        OnMoveEvent?.Invoke(vector2);
    }

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
        OnMoveEvent?.Invoke(vector2);
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

    #region Look
    private void InvokeLookEvent(Vector2 vector2)
    {
        //        Debug.Log($"InvokeLookEvent! value {vector2}");
        OnLookEvent?.Invoke(vector2);
    }

    #endregion

    #region Subscribe
    /// <summary>
    /// Subscribe a method to move event (Vector2)
    /// </summary>
    /// <param name="callback"></param>
    public void SubscribeMeOnMoveEvent(Vector2Event callback)
    {
        OnMoveEvent += callback;
    }

    /// <summary>
    /// Subscribe a method to Look event (Vector2)
    /// </summary>
    /// <param name="callback"></param>
    public void SubscribeMeOnLookEvent(Vector2Event callback)
    {
        OnLookEvent += callback;
    }

    /// <summary>
    /// Subscribe a method to Turn event (Vector2)
    /// </summary>
    /// <param name="callback"></param>
    public void SubscribeMeOnCameraTurnEvent(Vector3Event callback)
    {
        OnCameraRotateEvent += callback;
    }

    #endregion


    #region Unsubscribe
    public void UnsubscribeMeFromMoveEvent(Vector2Event callback)
    {
        OnMoveEvent -= callback;
    }

    public void UnsubscribeMeFromLookEvent(Vector2Event callback)
    {
        OnLookEvent -= callback;
    }

    public void UnsubscribeMeFromCameraTurnEvent(Vector3Event callback)
    {
        OnCameraRotateEvent -= callback;
    }
    #endregion








    #region UnityEvents with no Params
    /// <summary>
    /// Adds events into dictionary. Key is enum variables
    /// </summary>
    private void FillUpDictionaryNoPatamEvents()
    {
        foreach (var e in (NoParamEvents.GetValues(typeof(NoParamEvents))))
        {
            _events.Add((NoParamEvents)e, new UnityEvent());
        }
        // Debug.Log("Event dictionary filled");
    }

    /// <summary>
    /// Subscribe to the event from events enum (no parameters)
    /// </summary>
    /// <param name="noParamEvents"></param>
    /// <param name="callback"></param>
    public void SubscribeMeOnNoParamEvents(NoParamEvents noParamEvents, UnityAction callback)
    {
        if (_events.Count == 0)
            FillUpDictionaryNoPatamEvents();

        UnityEvent even;
        _events.TryGetValue(noParamEvents, out even);

        even?.AddListener(callback);
    }

    public void UnsubscribeMeFromNoParamEvents(NoParamEvents noParamEvents, UnityAction callback)
    {
        UnityEvent even;
        _events.TryGetValue(noParamEvents, out even);

        even?.RemoveListener(callback);
    }

    /// <summary>
    /// Invoke event by enum name
    /// </summary>
    /// <param name="noParamEvents"></param>
    public void InvokeNoParamEvents(NoParamEvents noParamEvents)
    {
        UnityEvent even;
        _events.TryGetValue(noParamEvents, out even);

        even?.Invoke();
    }

    #endregion


    private void OnEnable()
    {
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

    /// <summary>
    /// Triggers event with difference in degrees bitween camera forward and body forward
    /// </summary>
    private void TurnBodyToCameraForward()
    {

    }

    //----------------in Progress

    /// <summary>
    /// All Vector2 Events
    /// </summary>
    public enum Vector2Events
    {
        Look,
        Move,
        Turn,
    }
    /// <summary>
    /// Dictionary for vector2 events related to Vector2Events enum
    /// </summary>
    public Dictionary<Vector2Events, Vector2Event> _eventsVector2 = new Dictionary<Vector2Events, Vector2Event>();
    public void SubscribeMeOnVector2Event(Vector2Events vector2Events, Vector2Event callback)
    {
        if (_eventsVector2.Count == 0)
            FillUpDictionaryVector2Events();

        Vector2Event even;
        _eventsVector2.TryGetValue(vector2Events, out even);

        even += callback;
    }
    public void UnsubscribeMeFromVector2Event(Vector2Events vector2Events, Vector2Event callback)
    {
        Vector2Event even;
        _eventsVector2.TryGetValue(vector2Events, out even);

        even -= callback;
    }

    public delegate void theDelegate();
    public Dictionary<NoParamEvents, theDelegate> _es = new Dictionary<NoParamEvents, theDelegate>();
    private void FillUpDictionaryVector2Events()
    {
        //foreach (var e in (NoParamEvents.GetValues(typeof(NoParamEvents))))
        //{            
        //    _es.Add((Vector2Events)e, new theDelegate());
        //}
    }
}