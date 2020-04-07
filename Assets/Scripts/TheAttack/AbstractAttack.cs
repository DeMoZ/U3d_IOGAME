using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheAttack
{
    public abstract class AbstractAttack : MonoBehaviour
    {
        /// <summary>
        /// Class should receive events from animations with string names and parce it
        /// </summary>
        /// <param name="attackStates"></param>
        public virtual void Attack(string attackStates)
        {
            string[] parsed = attackStates.Split(':');
            if (parsed.Length < 2)
                throw new System.Exception($"Wrong string value from animation event on {gameObject} hould be ' id:PreStart ' ");
                     
            AttackStates state = AttackStates.None;

            System.Enum.TryParse(parsed[1], true, out state);

            // Debug.Log($"enum = {state}");
            Attack(state);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void Attack(AttackStates state)
        {
            switch (state)
            {
                case AttackStates.PreAttack:
                    PreAttack();
                    break;

                case AttackStates.StartAttack:
                    StartAttack();
                    break;

                case AttackStates.EndAttack:
                    EndAttack();
                    break;

                case AttackStates.PostAttack:
                    PostAttack();
                    break;

                default:        // IAttack.AttackStates.None
                    None();
                    break;
            }
        }

        /// <summary>
        /// event for movement befoure attack
        /// </summary>
        public abstract void PreAttack();

        /// <summary>
        /// Damage trigger should be placed here
        /// </summary>
        public abstract void StartAttack();

        /// <summary>
        /// Damage grigger should be removed here
        /// </summary>
        public abstract void EndAttack();

        /// <summary>
        /// event for movement after attack
        /// </summary>
        public abstract void PostAttack();

        /// <summary>
        /// wrong method name is set in animation event
        /// </summary>
        public abstract void None();
    }

    public enum AttackStates
    {
        None = 0,       // state with no activity or state wasnt parsed

        PreAttack,      // animation started and if required some pre effect/code before damage trigger appear
        StartAttack,    // damage trigger to be appeared
        EndAttack,      // damage trigger to be dissappear
        PostAttack,     // animation end and if required some pos effect/code required

        BrokeAttack,    // if attack has been broke
    };
}
