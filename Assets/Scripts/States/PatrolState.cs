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
        _waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
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
                animator.SetBool(RouteIsFinished, true);

                return;
            }
        }

        var direction = _waypoints[_currentWaypoint].transform.position - _NPC.transform.position;
        
        _NPC.transform.rotation = Quaternion.Slerp(_NPC.transform.rotation,
            Quaternion.LookRotation(direction), 
            _NPC.RotationSpeed * Time.deltaTime);

        _NPC.CharacterController.Move(direction.normalized * (_NPC.Speed * Time.deltaTime));
        
        UpdateFlags(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(RouteIsFinished, false);
    }

    protected override void UpdateFlags(Animator animator)
    {
        animator.SetInteger(EnemyStrength, _player.GetComponent<Player>().Strength);
        
        animator.SetFloat(Distance, 
            Vector3.Distance(_NPC.transform.position, _player.transform.position));
    }
}