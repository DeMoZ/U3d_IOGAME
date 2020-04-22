using System.Collections;
using System.Collections.Generic;
using TheAttack;
using TheMove;
using UnityEngine;

/// <summary>
/// Collect all the classes of IAction interface and check if some action can not be interupted
/// </summary>
[DisallowMultipleComponent]
public class ActionStateMachine : MonoBehaviour
{
    private List<IAction> _actions = new List<IAction>();

    private void Awake()
    {
        _actions.AddRange(GetComponents<IAction>());
    }

    /// <summary>
    /// Check all the IAction classes for behaviour that can not be interrupted
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    public bool AllowAction(IAction action)
    {
        foreach(IAction act in _actions)
        {
            if (act != action)
                if (act.IsInAction()) return false;
        }

        return true;
    }

}
