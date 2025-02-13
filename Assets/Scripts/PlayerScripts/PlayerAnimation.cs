//Made by: Mathias Sorin
//Last updated: 28/05/2021

using UnityEngine;

//Manages player animations
public class PlayerAnimation : MonoBehaviour
{
    //Player animator component
    [SerializeField] private Animator animator;

    //Use floats
    private void MovementAnimation(Vector3 rawInputMovement)
    {
        SetAllBool(false);
        if (rawInputMovement.z > 0)
        {
            SetSpecificBool("isRunningForward", true);
        }
        else if (rawInputMovement.x > 0)
        {
            SetSpecificBool("isRunningRight", true);
        }
        else if (rawInputMovement.x < 0)
        {
            SetSpecificBool("isRunningLeft", true);
        }
        else
        {
            SetSpecificBool("isIdle", true);
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
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.nameHash, on);
        }
    }

    //Set specific bool
    public void SetSpecificBool(string anim, bool on)
    {
        animator.SetBool(anim, on);
    }
}
