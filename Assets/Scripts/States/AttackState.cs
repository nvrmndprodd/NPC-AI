using System.Collections;
using UnityEngine;

public class AttackState : State
{
    private Coroutine attackCoroutine;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        attackCoroutine = Coroutines.StartRoutine(Attack());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateFlags(animator);
        
        var direction = _player.transform.position - _NPC.transform.position;
        
        _NPC.transform.rotation = Quaternion.Slerp(_NPC.transform.rotation,
            Quaternion.LookRotation(direction), 
            _NPC.RotationSpeed * Time.deltaTime);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Coroutines.StopRoutine(attackCoroutine);
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.1f);

            _NPC.AttackParticle.Play();

            yield return new WaitForSeconds(1.1f);
        }
    }
    
    protected override void UpdateFlags(Animator animator)
    {
        animator.SetInteger(EnemyStrength, _player.GetComponent<Player>().Strength);
        
        animator.SetFloat(Distance, 
            Vector3.Distance(_NPC.transform.position, _player.transform.position));

        if (animator.GetFloat(Distance) > _NPC.AttackRange)
            animator.SetBool(PlayerIsInAttackRange, false);
    }
}