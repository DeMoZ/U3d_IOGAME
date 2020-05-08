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
        _inputActions.PlayerControls.AttackUp.performed += ctrl => AttackUp();
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
    private void AttackUp()
    {
        Debug.Log("Test");
    }
    //--------------------------------------------------------
    /*
    // Attack Up Down Left Right

    public delegate void DelegateAttack();
    // Attack UP
    public event DelegateAttack OnAttackUp;
    private void AttackUp()
    {
        OnAttackUp?.Invoke();
    }
    public void SubscribeMeOnAttackUp(DelegateAttack callback)
    {
        OnAttackUp += callback;
    }
    // Attack Down
    public event DelegateAttack OnAttackDn;
    private void AttackDn()
    {
        OnAttackDn?.Invoke();
    }
    public void SubscribeMeOnAttackDn(DelegateAttack callback)
    {
        OnAttackDn += callback;
    }
    // Attack Left
    public event DelegateAttack OnAttackLt;
    private void AttackLt()
    {
        OnAttackLt?.Invoke();
    }
    public void SubscribeMeOnAttackLt(DelegateAttack callback)
    {
        OnAttackLt += callback;
    }
    // Attack Right
    public event DelegateAttack OnAttackRt;
    private void AttackRt()
    {
        OnAttackRt?.Invoke();
    }
    public void SubscribeMeOnAttackRt(DelegateAttack callback)
    {
        OnAttackRt += callback;
    }
    */
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
    }

    public void SubscribeMeOnNoParamEvents(NoParamEvents noParamEvents, UnityAction callback)
    {
        if (m_events.Count == 0)
        {
            FillUpDictionary();
            Debug.Log("Event dictionary filled");
        }

        UnityEvent even;
        m_events.TryGetValue(noParamEvents, out even);

        even?.AddListener(callback);
    }

    public void InvokeNoParamEvents(NoParamEvents noParamEvents)
    {
        Debug.Log($"1) Invoked {noParamEvents.ToString()}");
        UnityEvent even;
        m_events.TryGetValue(noParamEvents, out even);

        even?.Invoke();

        Debug.Log($"2) Invoked {noParamEvents.ToString()}");
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
