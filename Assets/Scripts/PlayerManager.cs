using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField] IInput _input;
    [SerializeField] IMove _move;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _input = GetComponent<IInput>();
        _move = GetComponent<IMove>();
    }

    private void Update()
    {        
       if (isLocalPlayer)
       {
            Vector3 dirtVelocity = _input.GetInput();
            Vector3 velocity = Vector3.zero;
            velocity.x = dirtVelocity.x;
            velocity.z = dirtVelocity.y;

            _move.Move(_input.GetInput());
       }

    }
}
