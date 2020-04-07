using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Animator))]

public class ButtonsListenner : NetworkBehaviour
{
    [SerializeField] private string _animatorAttackLayer = "Attack";
    [SerializeField] private string _animatorAttackTrigger = "Attack";

    private Animator _animator;
    private LayerMask _attackLayer;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _attackLayer = _animator.GetLayerIndex(_animatorAttackLayer);
    }
    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetMouseButtonDown(0))
        {
            CmdAttack();
        }
    }

    [Command]
    void CmdAttack()
    {
        RpcAttack();
    }
    [ClientRpc]
    void RpcAttack()
    {
        SetLayerWeight();
        _animator.SetTrigger(_animatorAttackTrigger);
    }

    void SetLayerWeight()
    {
        if (_animator.GetLayerWeight(_attackLayer) == 0)
        {
            _animator.SetLayerWeight(_attackLayer, 0.001f);
        }
    }

    void SetLayerWeight(int layer, float weight)
    {
        _animator.SetLayerWeight(layer, weight);
    }
}
