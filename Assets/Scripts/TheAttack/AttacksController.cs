using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheAttack
{
    /// <summary>
    /// Collects all animations from pointed gameobject and keep in dictionary to forward animations events for asociated action class
    /// </summary>
    public class AttacksController : MonoBehaviour, AbstractAttack
    {
        [Tooltip("The game object which contains all the classes related to animations")]
        [SerializeField] GameObject _animationsFolder;
        /// <summary>
        /// Dictionary of all the attacks for the person
        /// </summary>
        private Dictionary<string, AbstractAttack> _attacksDict = new Dictionary<string, AbstractAttack>();

        private Animator _animator;

        /// <summary>
        /// Class should receive events from animations with string names
        /// </summary>
        /// <param name="attackStates">The stirng will be splited by \":\" from event string to determine if the event is associated with the class</param>
        public void Attack(string attackStates)
        {
            string[] parsed = attackStates.Split(':');

            // check if splitted correctly
            if (parsed.Length < 2 && !string.IsNullOrEmpty(parsed[0]) && !string.IsNullOrEmpty(parsed[1]))
                throw new System.Exception($"Wrong string value from animation event on {gameObject} should be ' AnimationIdName:PreStart ' ");

            // check if animation id name is in dictionary
            if (_attacksDict.ContainsKey(parsed[0]))
            {
                AttackStates state = AttackStates.None;

                System.Enum.TryParse(parsed[1], true, out state);

                _attacksDict[parsed[0]].Attack(state);
            }
            else
                throw new System.Exception($"No animation id name [{parsed[0]}] in dictionary");
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
