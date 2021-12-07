using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class SearchState : State
{
    private Vector3 _onStateEnterPlayerPosition;
    private Coroutine _searchCoroutine;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _onStateEnterPlayerPosition = _player.transform.position;
        _searchCoroutine = Coroutines.StartRoutine(SearchCoroutine(animator));
        
        animator.SetBool(PlayerIsNotFound, false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UpdateFlags(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Coroutines.StopRoutine(_searchCoroutine);
    }

    private IEnumerator SearchCoroutine(Animator animator)
    {
        // moving to the position where player was saw for the last time
        var direction = _onStateEnterPlayerPosition - _NPC.transform.position;
        
        animator.SetFloat(Search, 5);
        yield return _NPC.RotateToAsync(direction);
        animator.SetFloat(Search, 0);
        
        yield return _NPC.MoveToAsync(_onStateEnterPlayerPosition);
        
        yield return null;

        // moving to 2 some random points
        for (var i = 0; i < 2; ++i)
        {
            animator.SetFloat(Search, 5);
            // staying here for 3 seconds
            yield return new WaitForSeconds(3);

            // moving to the new close random point
            var newPoint = _NPC.transform.position +
                           new Vector3(Random.Range(-4, 4), 0, Random.Range(-4, 4));
            var newDirection = newPoint - _NPC.transform.position;

            animator.SetFloat(Search, 5);
            yield return _NPC.RotateToAsync(newDirection);
            animator.SetFloat(Search, 0);
            
            yield return _NPC.MoveToAsync(newPoint);
        }
        
        animator.SetBool(PlayerIsNotFound, true);
    }
}