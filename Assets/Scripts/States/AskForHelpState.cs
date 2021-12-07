using UnityEngine;

public class AskForHelpState : State
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        NPCController.AskForHelp();
        
        UpdateFlags(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateFlags(animator);
    }
}