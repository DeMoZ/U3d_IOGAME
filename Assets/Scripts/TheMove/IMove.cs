using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMove
{
    /// <summary>
    /// The interface for all movement classes
    /// </summary>
    public interface IMove
    {
        void Move(Vector2 direction);
        void Turn(Vector2 rotation);
    }
}