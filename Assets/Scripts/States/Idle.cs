using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private float _idleTime;
    private Vector3 onStateEnterDistance;
    
    private void Awake()
    {
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _idleTime = 0f;
        onStateEnterDistance = _player.transform.position - _NPC.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _idleTime += Time.deltaTime;
        
        _NPC.transform.rotation = Quaternion.Slerp(_NPC.transform.rotation,
            Quaternion.LookRotation(onStateEnterDistance), 
            ROTATION_SPEED * Time.deltaTime);
        UpdateFlags(animator);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("IdleTime", 0f);
    }

    protected override void UpdateFlags(Animator animator)
    {
        animator.SetFloat("IdleTime", _idleTime);
        
        animator.SetInteger("EnemyStrength", _player.GetComponent<Player>().Strength);
        
        animator.SetFloat("Distance", 
            Vector3.Distance(_NPC.transform.position, _player.transform.position));
    }
}
