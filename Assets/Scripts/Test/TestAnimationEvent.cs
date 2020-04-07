using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestAnimationEvent : MonoBehaviour
    {
        public void AttackTest(string attackType)
        {
            AttackConditions tryEnum = AttackConditions.Nothing;

            System.Enum.TryParse(attackType, true, out tryEnum);

            Debug.Log($"enum = {tryEnum}");

        }

        public enum AttackConditions
        {
            Nothing,
            PreAttack,
            StartAttack,
            PostAttack,
            EndAttack,
            //////
            BrokeAttack
        }


        /// <summary>
        /// enables the trigger for damage
        /// </summary>
        public void AttackStart()
        {
            // необхобимо отменить все включеные евенты до этого. Нужна общая стейт машина по атакам?

        }


        /// <summary>
        /// disables the trigger for damage
        /// </summary>
        public void AttackEnd()
        {

        }

        // можно определять что за анимация играется и за счет этого выставлять тригер нужной формы. 
        // переменной float можно и выбирать форму триггера. - надо проверить enum
    }
}
