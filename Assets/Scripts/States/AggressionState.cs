﻿using UnityEngine;
using UnityEngine.Animations;

public class AggressionState : State
{

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateFlags(animator);

        var distanceToPlayer = animator.GetFloat(Distance);

        if (distanceToPlayer > _NPC.AttackRange)
        {
            // move to player
            
            var direction = _player.transform.position - _NPC.transform.position;
        
            _NPC.transform.rotation = Quaternion.Slerp(_NPC.transform.rotation,
                Quaternion.LookRotation(direction), 
                _NPC.RotationSpeed * Time.deltaTime);

            _NPC.CharacterController.Move(direction.normalized * (_NPC.Speed * Time.deltaTime));
        }
        else
        {
            // attack
            animator.SetBool(PlayerIsInAttackRange, true);
        }
    }

    protected override void UpdateFlags(Animator animator)
    {
        animator.SetInteger(EnemyStrength, _player.GetComponent<Player>().Strength);
        
        animator.SetFloat(Distance, 
            Vector3.Distance(_NPC.transform.position, _player.transform.position));
    }
}