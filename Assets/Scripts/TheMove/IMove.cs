using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheMove
{
    public interface IMove
    {
        void Move(Vector2 direction);
      //  void Move(Vector3 direction);
    }
}