//Made by: Mathias Sorin
//Last updated: 28/05/2021

using UnityEngine;

//Manages player animations
public class PlayerAnimation : MonoBehaviour
{
    //Player animator component
    [SerializeField] private Animator animator;

    private void MovementAnimation(Vector3 rawInputMovement)
    {
        SetAllBool(false);
        if (rawInputMovement.z > 0)
        {
            animator.SetBool("isRunningForward", true);
        }
        else if (rawInputMovement.x > 0)
        {
            animator.SetBool("isRunningRight", true);
        }
        else if (rawInputMovement.x < 0)
        {
            animator.SetBool("isRunningLeft", true);
        }
        else
        {
            animator.SetBool("isIdle", true);
        }
    }

    //Called by PlayerController class every update
    public void UpdateMovementData(Vector3 rawInputMovement)
    {
        MovementAnimation(rawInputMovement);
    }

    //Set all animator bool
    private void SetAllBool(bool on)
    {
        animator.SetBool("isIdle", on);
        animator.SetBool("isRunningForward", on);
        animator.SetBool("isRunningRight", on);
        animator.SetBool("isRunningLeft", on);
    }
}
