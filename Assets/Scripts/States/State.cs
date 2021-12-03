using UnityEngine;

public abstract class State : StateMachineBehaviour
{
    protected const float SPEED = 1.0f;
    protected const float ROTATION_SPEED = 2.0f;
    protected const float ACCURACY = 2.0f;
    
    protected GameObject _NPC;
    protected CharacterController _controller;
    protected GameObject _player;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _NPC = animator.gameObject.transform.Find("NPC").gameObject;
        _controller = animator.gameObject.GetComponent<CharacterController>();
        _player = animator.gameObject.GetComponent<NPC>().Player;
    }

    protected abstract void UpdateFlags(Animator animator);
}