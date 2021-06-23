//Made by: Mathias Sorin
//Last updated: 11/05/2021

using UnityEngine;
using MLAPI;
using UnityEngine.InputSystem;
using System.Collections;

//Manages player camera/camera control
public class PlayerCamera : NetworkBehaviour
{
    [Header("Camera Properties")]
    [Range(0.01f, 1f)] public float orbitCameraSpeedHorizontal;
    [Range(0.01f, 1f)] public float orbitCameraSpeedVertical;
    [Range(0.01f, 1f)] public float cameraSmoothing;

    [Header("Camera Component")]
    [SerializeField] private Camera playerCamera;
    [Header("Player Scripts")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerClass playerClass;

    //Components
    private AudioListener audioListener;
    private Transform playerTransform;

    //Mouse variables
    private Vector2 mousePosition;
    private bool mouseLeftClick = false;
    private bool mouseRightClick = false;

    //Camera offset from player, camera original rotation
    private Vector3 cameraOffset;
    private Vector3 cameraOrigin;
    private float cameraMaxAngleY = 45f;

    //Camera state machine
    public enum CameraState
    {
        Free, Locked
    }

    //Current camera state
    public CameraState currentCameraState;

    private void Start()
    {
        audioListener = playerCamera.GetComponent<AudioListener>();
        playerTransform = playerController.GetComponent<Transform>();
        cameraOffset = transform.position - playerTransform.position;
        cameraOrigin = transform.position;
    }

    private void Update()
    {
        if (mouseLeftClick)
        {
            currentCameraState = CameraState.Free;
            Select();
            OrbitCamera();
            CameraLookAt();
            return;
        }
        if (mouseRightClick)
        {
            currentCameraState = CameraState.Locked;
            OrbitCamera();
        }
        else
        {
            transform.position = (playerTransform.position + cameraOffset);
        }
        CameraLookAt();
    }

    //Rotate camera to look at player
    private void CameraLookAt()
    {
        transform.LookAt(playerTransform);
    }

    //Orbit camera around player by following mouse position
    private void OrbitCamera()
    {
        Quaternion camTurnAngleX = Quaternion.AngleAxis(mousePosition.x * orbitCameraSpeedHorizontal, transform.up);
        Quaternion camTurnAngleY = Quaternion.AngleAxis(-mousePosition.y * orbitCameraSpeedVertical, transform.right);
        camTurnAngleX = camTurnAngleX * camTurnAngleY;
        cameraOffset = camTurnAngleX * cameraOffset;
        Vector3 newPos = playerTransform.position + cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, cameraSmoothing);
    }

    //Select a ITargetable friendly/foe/etc..
    private void Select()
    {
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            ITargetable selection = hit.collider.GetComponent<ITargetable>();
            if (selection != null)
            {
                Unselect();
                selection.GetTargeted(true);
                playerClass.target = hit.transform.gameObject;
            }
        }
    }

    //Unselect
    private void Unselect()
    {
        if (playerClass.target == null)
        {
            return;
        }
        playerClass.target.GetComponent<ITargetable>().GetTargeted(false);
    }

    //Called by PlayerController class every update
    public void UpdateMouseData(Vector2 newMousePosition, bool newMouseLeftClick, bool newMouseRightClick)
    {
        mousePosition = newMousePosition;
        mouseLeftClick = newMouseLeftClick;
        mouseRightClick = newMouseRightClick;
    }
}
