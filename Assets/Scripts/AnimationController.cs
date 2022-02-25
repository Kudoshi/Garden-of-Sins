using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private string currentState;

    private void Start()
    {
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
    }
    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
