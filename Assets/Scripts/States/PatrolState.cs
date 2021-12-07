using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolState : State
{
    private GameObject[] _waypoints;
    private int _currentWaypoint;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        UpdateFlags(animator);
        _waypoints = _NPC.Waypoints;
        _currentWaypoint = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_waypoints.Length == 0) return;
        
        if (Vector3.Distance(_waypoints[_currentWaypoint].transform.position, _NPC.transform.position) < _NPC.MovementAccuracy)
        {
            ++_currentWaypoint;
            UpdateFlags(animator);
            
            if (_currentWaypoint >= _waypoints.Length)
            {
                _currentWaypoint = 0;
                animator.SetBool(RouteIsFinished, true);

                return;
            }
        }

        var direction = _waypoints[_currentWaypoint].transform.position - _NPC.transform.position;
        
        _NPC.RotateTo(direction);
        _NPC.MoveTo(direction);
        
        UpdateFlags(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(RouteIsFinished, false);
    }
}