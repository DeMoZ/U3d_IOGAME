//#define INSTALL_CAMERA
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;
using TheInput;
using Mirror;
using UnityEngine.Events;

/// <summary>
///  Input based on Imput system
/// </summary>
public class PlayerControllerInputSystem : NetworkBehaviour
{
    public delegate void MoveEvent(Vector2 vector2);
    /// <summary>
    /// Event invoked on Move Arrows pressed
    /// </summary>
    public MoveEvent OnMoveEvent;

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
    public Dictionary<NoParamEvents, UnityEvent> m_events = new Dictionary<NoParamEvents, UnityEvent>();

#if INSTALL_CAMERA
    [SerializeField] GameObject _playerCameraPrefab;
#endif
    Transform _cameraT;

    /// <summary>
    /// the input action file with all actions presets
    /// </summary>
    private TheInputActions _inputActions;

    private Vector3 _direction = Vector3.zero;

    /// <summary>
    /// cashed velocity prom previous frame;
    /// </summary>    
    private Vector3 velocityPrevious = Vector3.zero;



    private void Awake()
    {
        _inputActions = new TheInputActions();

        _inputActions.PlayerControls.Move.performed += cntx => InvokeMoveEvent(cntx.ReadValue<Vector2>());

        _inputActions.PlayerControls.AttackUp.performed += ctrl => InvokeNoParamEvents(NoParamEvents.AttackUp);
        _inputActions.PlayerControls.AttackDn.performed += ctrl => InvokeNoParamEvents(NoParamEvents.AttackDn);
        _inputActions.PlayerControls.AttackLt.performed += ctrl => InvokeNoParamEvents(NoParamEvents.AttackLt);
        _inputActions.PlayerControls.AttackRt.performed += ctrl => InvokeNoParamEvents(NoParamEvents.AttackRt);

#if INSTALL_CAMERA
        if (!_playerCameraPrefab)
            throw new System.Exception($"_ Camera prefab not set for {this}");

        // instantiate player camera
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.LookRotation(transform.position - position, Vector3.up);
        GameObject cameraGO = Instantiate(_playerCameraPrefab, position, rotation);

        _cameraT = _playerCameraPrefab.transform;
#else
        _cameraT = Camera.main.transform;
#endif
    }

    #region Move event
    private void InvokeMoveEvent(Vector2 vector2)
    {
        vector2 = DirectionCameraRelated(vector2);

        OnMoveEvent?.Invoke(vector2);
    }

    private Vector2 DirectionCameraRelated(Vector2 input)
    {
        Vector3 velocity = Vector3.zero;
        velocity.x = input.x;
        velocity.z = input.y;

        // camera forward and right vectors:
        Vector3 forward = _cameraT.forward;
        Vector3 right = _cameraT.right;

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

    private Vector3 DirectionCameraRelated(Vector3 input)
    {
        Vector3 velocity = Vector3.zero;
        velocity.x = input.x;
        velocity.z = input.y;

        // camera forward and right vectors:
        Vector3 forward = _cameraT.forward;
        Vector3 right = _cameraT.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        velocity = forward * velocity.z + right * velocity.x;

        // velocity magnitude not more than 1
        velocity = Vector3.ClampMagnitude(velocity, 1);
        return velocity;
    }

    #endregion

    /// <summary>
    /// Subscribe a method to move event (Vector2)
    /// </summary>
    /// <param name="callback"></param>
    public void SubscribeMeOnMoveEvent(MoveEvent callback)
    {
        OnMoveEvent += callback;
    }

    #region UnityEvents with no Params
    /// <summary>
    /// Adds events into dictionary. Key is enum variables
    /// </summary>
    private void FillUpDictionary()
    {
        foreach (var e in (NoParamEvents.GetValues(typeof(NoParamEvents))))
        {
            m_events.Add((NoParamEvents)e, new UnityEvent());
        }
        Debug.Log("Event dictionary filled");
    }

    /// <summary>
    /// Subscribe to the event from events enum (no parameters)
    /// </summary>
    /// <param name="noParamEvents"></param>
    /// <param name="callback"></param>
    public void SubscribeMeOnNoParamEvents(NoParamEvents noParamEvents, UnityAction callback)
    {
        if (m_events.Count == 0)
            FillUpDictionary();

        UnityEvent even;
        m_events.TryGetValue(noParamEvents, out even);

        even?.AddListener(callback);
    }

    /// <summary>
    /// Invoke event by enum name
    /// </summary>
    /// <param name="noParamEvents"></param>
    public void InvokeNoParamEvents(NoParamEvents noParamEvents)
    {
        UnityEvent even;
        m_events.TryGetValue(noParamEvents, out even);

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

}
