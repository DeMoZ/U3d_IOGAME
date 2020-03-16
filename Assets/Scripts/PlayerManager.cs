using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    [SerializeField] SimpleKeyboardInput _input;
    [SerializeField] SimpleMove _move;

    private void Update()
    {
        //if (isLocalPlayer)
            _move.Move(_input.GetInput());
    }
}
