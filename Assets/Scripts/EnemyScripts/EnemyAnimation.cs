//Made by: Mathias Sorin
//Last updated: 18/06/2021

using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    //Components
    private Animator animator;
    private EnemyAI enemyAI;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public void Animate()
    {
        SetAllAnimatorBool(false);
        switch (enemyAI.currentState)
        {
            case EnemyAI.State.Idle:
                SetAnimatorBool("idle", true);
                break;
            case EnemyAI.State.Chase:
                SetAnimatorBool("chase", true);
                break;
            case EnemyAI.State.Attack:
                SetAnimatorBool("attack", true);
                break;
        }
    }

    private void SetAnimatorBool(string id, bool on)
    {
        animator.SetBool(id, on);
    }

    private void SetAllAnimatorBool(bool on)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, on);
            }
        }
    }
}
