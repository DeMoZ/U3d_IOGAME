﻿using System.Collections;
using System.Collections.Generic;
using TheCamera;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using TheInput;

namespace TheControllable
{
    /// <summary>
    /// characher that can be contolled with input system
    /// </summary>
    public interface IControllable
    {
        /// <summary>
        /// Initialize controllable with input system and camera
        /// </summary>
        /// <param name="input"></param>
        /// <param name="camera"></param>
        void Init(IInputSystem input, IPlayerCamera camera);

        /// <summary>
        /// returns cashed transform of the object
        /// </summary>
        Transform GetTransform { get; }
    }
}