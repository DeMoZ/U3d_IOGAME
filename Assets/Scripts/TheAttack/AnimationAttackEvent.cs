using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGlobal;

namespace TheAttack
{

    [CreateAssetMenu(fileName = "AnimationEventScriptable", menuName = "ScriptableObjects/AnimationEvent")]
    public class AnimationAttackEvent : ScriptableObject
    {
        [SerializeField] private GlobalEnums.AnimationNamesIDs _animationNamesIDs;
        public GlobalEnums.AnimationNamesIDs GetAnimationNamesIDs => _animationNamesIDs;


        [SerializeField] private GlobalEnums.AnimatorLayers _animatorLayer;
        public GlobalEnums.AnimatorLayers GetAnimatorLayer => _animatorLayer;
        

        [SerializeField] private GlobalEnums.AttackStates _attackState;
        public GlobalEnums.AttackStates GetAttackState => _attackState;
        public override string ToString()
        {
            string rezult = "";
            rezult += $"_";
            rezult += $"AnimationNamesIDs = {GetAnimationNamesIDs} ; ";
            rezult += $"AnimatorLayer = {GetAnimatorLayer} ; ";
            rezult += $"AttackState = {GetAttackState} ; ";

            return rezult;
        }
    }
}
