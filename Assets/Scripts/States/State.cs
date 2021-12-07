using UnityEngine;

public abstract class State : StateMachineBehaviour
{
    protected readonly int EnemyIsTooStrong = Animator.StringToHash("EnemyIsTooStrong");
    protected readonly int Distance = Animator.StringToHash("Distance");
    protected readonly int RouteIsFinished = Animator.StringToHash("RouteIsFinished");
    protected readonly int IdleTime = Animator.StringToHash("IdleTime");
    protected readonly int PlayerIsInAttackRange = Animator.StringToHash("PlayerIsInAttackRange");
    protected readonly int PlayerIsNotFound = Animator.StringToHash("PlayerIsNotFound");
    protected readonly int Search = Animator.StringToHash("Search");
    protected readonly int SomeoneAsksForHelp = Animator.StringToHash("SomeoneAsksForHelp");
    
    protected NPC _NPC;
    protected Player _player;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _NPC = animator.gameObject.GetComponent<NPC>();
        _player = _NPC.Player;
        animator.SetBool(SomeoneAsksForHelp, false);
    }

    protected virtual void UpdateFlags(Animator animator)
    {
        animator.SetBool(EnemyIsTooStrong, _player.GetComponent<Player>().Strength > _NPC.Strength);
        
        animator.SetFloat(Distance, 
            Vector3.Distance(_NPC.transform.position, _player.transform.position));
    }
}