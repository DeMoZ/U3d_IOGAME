using System.Collections;
using System.Collections.Generic;
using TheMove;
using UnityEngine;

namespace TheMove
{
    /// <summary>
    /// Script is required for Player Conrolller.
    /// Adapt event subscribtion for methods inheritated from interfaces.
    /// TODO: Find out how to inject dependency fom methods inhereited from interfaces
    /// </summary>
    [DisallowMultipleComponent]
    public class IMoveController : MonoBehaviour
    {
        private IMove _imove;
        private IMove GetMove
        {
            get
            {
                if (_imove == null)
                    _imove = GetComponent<IMove>();

                return _imove;
            }
        }

        public void Move(Vector2 vector2)
        {
            GetMove.Move(vector2);
        }
    }
}