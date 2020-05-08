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
#if INSTALL_CAMERA
    [SerializeField] GameObject _playerCameraPrefab;
#endif
    Transform _cameraT;

    private TheInputActions _inputActions;
    private Vector3 _direction = Vector3.zero;

    /// <summary>
    /// cashed velocity prom previous frame;
    /// </summary>    
    private Vector3 velocityPrevious = Vector3.zero;

    public Vector3 GetInput()
    {
        return _direction;
    }

    private void Awake()
    {
        _inputActions = new TheInputActions();
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

    //----------------------------------------------------------

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

    public Dictionary<NoParamEvents, UnityEvent> m_events = new Dictionary<NoParamEvents, UnityEvent>();

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


    //

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

}
