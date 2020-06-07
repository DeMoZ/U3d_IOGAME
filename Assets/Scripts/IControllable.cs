using System.Collections;
using System.Collections.Generic;
using TheCamera;
using UnityEngine;

/// <summary>
/// characher that can be contolled with input system
/// </summary>
public interface IControllable
{
    //PlayerInputSystem GetPlayerInputSystem { get; set; }
    void Init(PlayerInputSystem input, IPlayerCamera camera);
}
