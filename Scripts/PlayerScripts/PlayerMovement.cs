//Made by: Mathias Sorin
//Last updated: 08/05/2021

using UnityEngine;
using MLAPI;

//Manages player movement/gravity
public class PlayerMovement : NetworkBehaviour
{
    [Header("Movement Variables")]
    [Range(1f, 50f)] public float movementSpeed = 5f;
    [Range(1f, 50f)] public float jumpStrength = 5f;
    [Range(1f, 50f)] public float gravityStrength = 10f;

    [Header("Character Controller Component")]
    [SerializeField] private CharacterController playerCharacterController;

    [Header("Player Scripts")]
    [SerializeField] private PlayerCamera playerCamera;

    private Vector3 movementDirection;
    private float movementY;
    private bool jumpInput = false;

    private void Update()
    {
        PlayerMove();
        GroundCheck();
    }

    //Called by PlayerController class every update
    public void UpdateMovementData(Vector3 newMovementDirection)
    {
        movementDirection = newMovementDirection.normalized;
    }

    //Called by PlayerController class every event pressed
    public void UpdateJumpData()
    {
        if (playerCharacterController.isGrounded)
        {
            jumpInput = true;
        }
    }

    //Move player taking into consideration movement input/gravity/jump
    private void PlayerMove()
    {
        playerCharacterController.Move(CalculatePlayerMovement() * Time.deltaTime);
    }

    private Vector3 CalculatePlayerMovement()
    {
        Vector3 cameraForward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z);
        Vector3 cameraRight = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z);
        Vector3 movement = (cameraForward.normalized * movementDirection.z + cameraRight * movementDirection.x) * movementSpeed;
        PlayerJump();
        PlayerGravity();
        movement.y = movementY;
        transform.rotation = Quaternion.LookRotation(cameraForward);
        return movement;
    }

    private void PlayerJump()
    {
        if (jumpInput && playerCharacterController.isGrounded)
        {
            movementY = jumpStrength;
            jumpInput = false;
        }
    }

    private void PlayerGravity()
    {
        movementY -= gravityStrength * Time.deltaTime;
    }

    private void GroundCheck()
    {
        if (playerCharacterController.isGrounded)
        {
            //movementY is set to -1 to make sure that the player is grounded otherwise player will float
            movementY = -1;
        }
    }
}
