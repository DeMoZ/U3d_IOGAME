using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheGlobal;

namespace TheAttack
{
    /// <summary>
    /// Collects all animations from pointed gameobject and keep in dictionary to forward animations events for asociated action class
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AttacksController : MonoBehaviour
    {
        [Tooltip("The game object which contains all the classes related to animations")]
        [SerializeField] GameObject _animationsFolder;
        /// <summary>
        /// Dictionary of all the attacks for the person
        /// </summary>
        private Dictionary<GlobalEnums.AnimationNamesIDs, AbstractAttack> _attacksDict = new Dictionary<GlobalEnums.AnimationNamesIDs, AbstractAttack>();

        private Animator _animator;


        public void Attack(AnimationAttackEvent value)
        {
            if (_attacksDict.ContainsKey(value.GetAnimationNamesIDs))
            {
                _attacksDict[value.GetAnimationNamesIDs].Attack(value.GetAttackState);                
            }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            FindAnimations();
        }

        /// <summary>
        /// collect animations from game object
        /// </summary>
        private void FindAnimations()
        {
            AbstractAttack[] _attacks = _animationsFolder.GetComponents<AbstractAttack>();

            foreach (AbstractAttack attack in _attacks)
            {
                if (!_attacksDict.ContainsKey(attack.GetIdName))
                {
                    _attacksDict.Add(attack.GetIdName, attack);

                    attack.GetAnimator = _animator;
                }
                else
                    throw new System.Exception($"Dublicate animation id in class for Animation Folder {gameObject}");
            }
        }

    }
}
