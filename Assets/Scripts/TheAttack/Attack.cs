using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace TheAttack
{

    [RequireComponent(typeof(Animator))]
    public class Attack : AbstractAttack
    {
        /// <summary>
        /// need to check by calling class by the interface iAttack
        /// можно метод задать в интерфейсе и реализовать в абстрактном классе и сделать абстрактным!!!
        /// </summary>
        public void CheckIfInterfaceStillWorks()
        {
            Debug.Log("for sure it works");
        }
    }
}
