﻿using System;
using System.Collections;
using System.Collections.Generic;
using TheGlobal;
using UnityEngine;
using Mirror;

namespace TheAttack
{
    /// <summary>
    /// This script also check the state machine for character behaviour
    /// The scripts listens animation attack state and also listens attack button press.
    /// The attack trigger only alaywed if button pressed during right time and state
    /// </summary>
    [RequireComponent(typeof(ActionStateMachine))]
    public class AttackQueueAction : NetworkBehaviour, IAnimationAttackEventListenner, IAttack, IAction
    {
        [SerializeField] private GlobalEnums.AnimatorTriggers _animatorAttackTrigger = GlobalEnums.AnimatorTriggers.Attack;

        private ActionStateMachine _actionStateMachine;
        private Animator _animator;
        private Animator GetAnimator
        {
            get
            {
                if (!_animator)
                    _animator = GetComponent<Animator>();

                return _animator;
            }
        }

        private GlobalEnums.AttackStates _currentAttackState = GlobalEnums.AttackStates.None;
        private GlobalEnums.AttackStates GetCurrentAttackState => _currentAttackState;

        private GlobalEnums.QueueStates _currentQueueState = GlobalEnums.QueueStates.Ready;

        private bool _ignoreAttack = false;

        private bool _triggerReadynes = false;

        /// <summary>
        /// true, when attack can be send to animator
        /// </summary>
        public bool GetTriggerReadynes => _triggerReadynes;

        public void OnAnimationAttack(AnimationAttackEvent value)
        {
            //            Debug.Log($"{this} OnAnimationAttack");
            GlobalEnums.AttackStates state = value.GetAttackState;

            AttackState(state);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        public void AttackState(GlobalEnums.AttackStates state)
        {
            _currentAttackState = state;
            switch (state)
            {
                case GlobalEnums.AttackStates.End:
                    _ignoreAttack = false;
                    Debug.Log($"Attack end, ignore attack = {_ignoreAttack}");
                    break;
            }

            /*
            switch (state)
            {
                case GlobalEnums.AttackStates.Warm:
                    PreAttack();
                    break;

                case GlobalEnums.AttackStates.Hit:
                    Attack();
                    break;

                case GlobalEnums.AttackStates.Hold:
                    Hold();
                    break;

                case GlobalEnums.AttackStates.Cold:
                    EndAttack();
                    break;

                case GlobalEnums.AttackStates.End:
                    EndAttack();
                    break;


                default:        // IAttack.AttackStates.None
                    None();
                    break;
            }
            */
        }

        /*
        /// <summary>
        /// no state or idle. Attack allowed
        /// </summary>
        private void None()
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// the weapon is going to position for start swing
        /// </summary>
        private void PreAttack()
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// actual attack is performing
        /// </summary>
        private void Attack()
        {
            // throw new NotImplementedException();
        }

        /// <summary>
        /// the attack has finished
        /// </summary>
        private void Hold()
        {
            //if (_stopAfterHit)
            //{
            //    _stopAfterHit = false;

            //    // send trigger for interrupt queue;
            //    // TODO
            //}
        }

        /// <summary>
        /// the weapon is going to idle position (attack not allowed)
        /// </summary>
        private void EndAttack()
        {
            // throw new NotImplementedException();
        }

        */


        void Awake()
        {
            _actionStateMachine = GetComponent<ActionStateMachine>();
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _animator = GetComponent<Animator>();

            _triggerReadynes = true;
            _ignoreAttack = false;
        }


        // queue
        void Update()
        {
            if (!isLocalPlayer) return;

            if (!_actionStateMachine.AllowAction(this)) return;

            if (Input.GetMouseButtonDown(0))
            {
                CmdAttack();
            }
        }

        [Command]
        void CmdAttack()
        {
            Debug.Log($"GetCurrentAttackState = {GetCurrentAttackState}, _ignoreAttack = {_ignoreAttack} ");

            switch (GetCurrentAttackState)
            {
                case GlobalEnums.AttackStates.Warm:
                    // leave that empty so attack trigger will not happen
                    break;

                case GlobalEnums.AttackStates.Hit:
                    // attack queuw should be stoped after End Attack

                    //!!! need to  stop listen to attack button
                    _ignoreAttack = true;
                    break;

                case GlobalEnums.AttackStates.Cold:
                    // leave that empty so attack trigger will not happen
                    break;


                default:

                    if (!_ignoreAttack)
                        RpcAttack();

                    break;
            }

            // }


        }
        [ClientRpc]
        void RpcAttack()
        {
            GetAnimator.SetTrigger("AttackUp");
        }

        public bool IsInAction()
        {
            bool rezult = false;
            // some state
            switch (GetCurrentAttackState)
            {
                case GlobalEnums.AttackStates.Warm:
                    rezult = true;
                    break;

                case GlobalEnums.AttackStates.Hit:
                    rezult = true;
                    break;

                case GlobalEnums.AttackStates.Hold:
                    rezult = true;
                    break;
            }

            Debug.Log($"{this} bussy = {rezult}");

            return rezult;
        }
    }
}