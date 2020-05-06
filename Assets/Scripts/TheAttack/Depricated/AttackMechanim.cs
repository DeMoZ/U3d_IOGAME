using System.Collections;
using System.Collections.Generic;
using TheAttack;
using TheGlobal;
using UnityEngine;

/// <summary>
/// Depricated: Class contains time points to send animation state Messages to a classes that may concern
/// </summary>
public class AttackMechanim : StateMachineBehaviour
{
    //[Tooltip("The time where move character makes move to attack position start")]
    //[SerializeField] float _PreparationAt;  // 0
    [Tooltip("The time where attack started")]
    [SerializeField] float _startAttackAt;  // 1
    [Tooltip("The time where attack ended, and character makes pause to reduce the swing velocity (and listen to make other hit)")]
    [SerializeField] float _HoldAt;         // 2
    [Tooltip("The time where character returns the weapon to idle position")]
    [SerializeField] float _ReturnAt;       // 3

    private float _timer;

    private int _states = 0;

    private string _methodName = "AttackState";

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SendMessageUpwards(_methodName, GlobalEnums.AttackStates.Warm, SendMessageOptions.DontRequireReceiver);

        _timer = 0f;
        _states = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        switch (_states)
        {
            case 0:
                if (_timer >= _startAttackAt)
                {
                    animator.gameObject.SendMessageUpwards(_methodName, GlobalEnums.AttackStates.Hit, SendMessageOptions.DontRequireReceiver);
                    _states++;
                }
                break;
            case 1:
                if (_timer >= _HoldAt)
                {
                    animator.gameObject.SendMessageUpwards(_methodName, GlobalEnums.AttackStates.Hold, SendMessageOptions.DontRequireReceiver);
                    _states++;
                }
                break;
            case 2:
                if (_timer >= _ReturnAt)
                {
                    animator.gameObject.SendMessageUpwards(_methodName, GlobalEnums.AttackStates.Cold, SendMessageOptions.DontRequireReceiver);
                    _states++;
                }
                break;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SendMessageUpwards(_methodName, GlobalEnums.AttackStates.End, SendMessageOptions.DontRequireReceiver);
        Debug.Log($"Timer is {_timer}, time changed is {_timer * stateInfo.speed * stateInfo.speedMultiplier} animation speed is {stateInfo.speed} ., animation speed multi is {stateInfo.speedMultiplier} ");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}