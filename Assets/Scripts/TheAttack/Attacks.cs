﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheAttack
{
    /// <summary>
    /// General state machine for attack animation end behaviour
    /// </summary>
    abstract public class Attacks : MonoBehaviour, IAttack
    {
        [Tooltip("The unic name for the animation. The name will be splited by \":\" from event string to determine if the event is associated with the class")]
        [SerializeField] protected string _idName;

        /// <summary>
        /// Class should receive events from animations with string names
        /// </summary>
        /// <param name="attackStates"></param>
        public void Attack(string attackStates)
        {
            string[] parsed = attackStates.Split(':');
            if (parsed.Length < 2)
                throw new System.Exception($"Wrong string value from animation event on {gameObject} hould be ' id:PreStart ' ");

            // if first parth of string value
            if (_idName != parsed[0]) return;

            AttackStates state = AttackStates.None;

            System.Enum.TryParse(parsed[1], true, out state);

            // Debug.Log($"enum = {state}");

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
}
