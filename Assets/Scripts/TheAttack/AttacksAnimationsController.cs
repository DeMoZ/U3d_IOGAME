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
    public class AttacksAnimationsController : MonoBehaviour, IAnimationAttackEventListenner
    {
        [Tooltip("The game object which contains all the classes related to animations")]
        [SerializeField] GameObject _animationsFolder;
        /// <summary>
        /// Dictionary of all the attacks for the person
        /// </summary>
        private Dictionary<GlobalEnums.AnimationNamesIDs, AttackAnimation> _attacksDict = new Dictionary<GlobalEnums.AnimationNamesIDs, AttackAnimation>();

        private Animator _animator;


        public void OnAnimationAttack(AnimationAttackEvent value)
        {
            Debug.Log($"Услышан эвент анимации {value.ToString()}");

            if (_attacksDict.ContainsKey(value.GetAnimationNamesIDs))
            {
                _attacksDict[value.GetAnimationNamesIDs].AttackState(value.GetAttackState);                
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
            AttackAnimation[] _attacks = _animationsFolder.GetComponents<AttackAnimation>();
            Debug.Log($"найдено атак на игроке {_attacks.Length}");
            foreach (AttackAnimation attack in _attacks)
            {
                if (!_attacksDict.ContainsKey(attack.GetIdName))
                {
                    Debug.Log($"добавлена в список");
                    _attacksDict.Add(attack.GetIdName, attack);

                    attack.GetAnimator = _animator;
                }
                else
                    throw new System.Exception($"Dublicate animation id in class for Animation Folder {gameObject}");
            }
        }

    }
}
