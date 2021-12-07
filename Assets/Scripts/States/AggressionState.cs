using UnityEngine;
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
            _NPC.RotateTo(direction);
            _NPC.MoveTo(direction);
        }
        else
        {
            // attack
            animator.SetBool(PlayerIsInAttackRange, true);
        }
    }
}