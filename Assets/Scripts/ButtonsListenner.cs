using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Animator))]
public class ButtonsListenner : NetworkBehaviour
{    
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Attack");
        }
    }
}
