//Made by: Mathias Sorin
//Last updated: 08/05/2021

using UnityEngine;
using UnityEngine.InputSystem;
using MLAPI;
using MLAPI.NetworkVariable;

//Manages player inputs
public class PlayerController : NetworkBehaviour
{
    //Network Variable
    public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
    {
        WritePermission = NetworkVariablePermission.Everyone,
        ReadPermission = NetworkVariablePermission.Everyone
    });

    [Header("Player Components")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private PlayerClass playerClass;
    [SerializeField] private PlayerCamera playerCameraScript;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject playerExtUI;
    [SerializeField] private Camera playerCamera;

    private Vector3 rawInputMovement;
    private Vector2 rawInputMouse;

    //Deactivate player components if not local player
    private void Start()
    {
        if (IsLocalPlayer)
        {
            GameManager.Instance.localPlayer = gameObject;
            GameManager.Instance.playerCamera = playerCamera;
            playerExtUI.SetActive(false);
            //Layer ignore raycast on self so you cannot target yourself
            this.gameObject.layer = 2;
        }
        else
        {
            playerInput.enabled = false;
            playerMovement.enabled = false;
            playerCamera.enabled = false;
            playerCamera.GetComponent<AudioListener>().enabled = false;
            playerUI.SetActive(false);
            this.enabled = false;
        }
    }

    //Called on movement player input
    public void OnMovement(InputAction.CallbackContext value)
    {
        rawInputMovement = new Vector3(value.ReadValue<Vector2>().x, 0, value.ReadValue<Vector2>().y);
    }

    public void OnMouseMove(InputAction.CallbackContext value)
    {
        rawInputMouse = value.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerMovement.UpdateJumpData();
        }
    }

    public void OnRoll(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerMovement.UpdateRollData();
        }
    }

    public void OnInteract(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerClass.Interact();
        }
    }

    public void OnMouseLeftClick(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if(playerClass.toggleAbility != null)
            {
                playerClass.toggleAbility.ConfirmAbility();
                return;
            }
            playerClass.LeftClickAbility();
        }
    }

    public void OnMouseRightClick(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (playerClass.toggleAbility != null)
            {
                playerClass.toggleAbility.ToggleFunctionOff();
                return;
            }
            playerClass.RightClickAbility();
        }
    }

    public void On1(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerClass.Ability1();
        }
    }

    public void On2(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerClass.Ability2();
        }
    }

    public void On3(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            playerClass.Ability3();
        }
    }

    private void Update()
    {
        UpdatePlayerMovement();
        UpdatePlayerMouse();
    }

    private void UpdatePlayerMovement()
    {
        GameManager.Instance.playerTransform = transform;
        playerMovement.UpdateMovementData(rawInputMovement);
        playerAnimation.UpdateMovementData(rawInputMovement);
    }

    private void UpdatePlayerMouse()
    {
        GameManager.Instance.UpdateMouseData(rawInputMouse);
        playerCameraScript.UpdateMouseData(rawInputMouse);
    }
}
