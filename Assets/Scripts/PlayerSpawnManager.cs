using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : NetworkBehaviour
{

    // This fires on all clients when this player object is network-ready
    public override void OnStartClient()
    {
        base.OnStartClient();

        Debug.Log(" Looking for playersPanel");
        // Make this a child of the layout panel in the Canvas
        transform.SetParent(GameObject.Find("PlayersPanel").transform);
    }

    // This only fires on the local client when this player object is network-ready
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }
}

