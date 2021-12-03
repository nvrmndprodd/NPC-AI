using UnityEngine;

public abstract class State : StateMachineBehaviour
{
    protected readonly int EnemyStrength = Animator.StringToHash("EnemyStrength");
    protected readonly int Distance = Animator.StringToHash("Distance");
    protected readonly int RouteIsFinished = Animator.StringToHash("RouteIsFinished");
    protected readonly int IdleTime = Animator.StringToHash("IdleTime");
    protected readonly int PlayerIsInAttackRange = Animator.StringToHash("PlayerIsInAttackRange");
    
    protected NPC _NPC;
    protected Player _player;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _NPC = animator.gameObject.transform.Find("NPC").GetComponent<NPC>();
        _player = _NPC.Player;
    }

    protected abstract void UpdateFlags(Animator animator);
}