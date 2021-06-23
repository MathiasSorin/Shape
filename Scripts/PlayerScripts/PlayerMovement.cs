//Made by: Mathias Sorin
//Last updated: 08/05/2021

using UnityEngine;
using MLAPI;
using System.Collections;

//Manages player movement/gravity
public class PlayerMovement : NetworkBehaviour
{
    [Header("Movement Variables")]
    [Range(1f, 50f)] public float movementSpeed = 5f;
    [Range(1f, 50f)] public float jumpStrength = 5f;
    [Range(0.01f, 5f)] public float rollDuration = 0.5f;
    [Range(1f, 50f)] public float rollSpeed = 10f;
    [Range(0.1f, 50f)] public float rollCooldown = 2f;
    [Range(1f, 50f)] public float gravityStrength = 10f;

    [Header("Character Controller Component")]
    [SerializeField] private CharacterController playerCharacterController;

    [Header("Player Scripts")]
    [SerializeField] private PlayerCamera playerCamera;

    //Movement variables
    private Vector3 movementDirection;
    private float movementY;
    private bool jumpInput = false;
    private bool rollInput = false;
    private bool canRoll = true;

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
        if (!playerCharacterController.isGrounded)
        {
            return;
        }
        jumpInput = true;
    }

    public void UpdateRollData()
    {
        if (!canRoll)
        {
            return;
        }
        rollInput = true;
    }

    //Move player taking into consideration movement input/gravity/jump
    private void PlayerMove()
    {
        playerCharacterController.Move(CalculatePlayerMovement() * Time.deltaTime);
    }

    private Vector3 CalculatePlayerMovement()
    {
        Vector3 cameraForward;
        Vector3 cameraRight;
        CheckCameraState(out cameraForward, out cameraRight);
        Vector3 movement = (cameraForward.normalized * movementDirection.z + cameraRight * movementDirection.x) * movementSpeed;
        PlayerRoll(movement);
        PlayerJump();
        PlayerGravity();
        movement.y = movementY;
        return movement;
    }

    //Check in wich state is the camera return forward, right vector according to state
    private void CheckCameraState(out Vector3 cameraForward, out Vector3 cameraRight)
    {
        if (playerCamera.currentCameraState == PlayerCamera.CameraState.Locked)
        {
            cameraForward = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z);
            cameraRight = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z);
            transform.rotation = Quaternion.LookRotation(cameraForward);
        }
        else
        {
            cameraForward = transform.forward;
            cameraRight = transform.right;
        }
    }

    private void PlayerJump()
    {
        if (jumpInput && playerCharacterController.isGrounded)
        {
            movementY = jumpStrength;
            jumpInput = false;
        }
    }

    private void PlayerRoll(Vector3 movement)
    {
        if (!rollInput || !canRoll)
        {
            return;
        }
        StartCoroutine(RollCooldown());
        StartCoroutine(Roll(movement));
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

    //Performs a roll during the duration
    private IEnumerator Roll(Vector3 movement)
    {
        float rollStart = Time.time;
        rollInput = false;

        while (Time.time < rollStart + rollDuration)
        {
            playerCharacterController.Move((movement/movementSpeed) * rollSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator RollCooldown()
    {
        canRoll = false;
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }
}