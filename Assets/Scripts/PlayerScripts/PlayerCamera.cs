//Made by: Mathias Sorin
//Last updated: 11/05/2021

using UnityEngine;
using MLAPI;
using UnityEngine.InputSystem;

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

    //Camera offset from player
    private Vector3 cameraOffset;

    private void Start()
    {
        audioListener = playerCamera.GetComponent<AudioListener>();
        playerTransform = playerController.GetComponent<Transform>();
        cameraOffset = transform.position - playerTransform.position;
    }

    private void Update()
    {
        OrbitCamera();
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

    //Called by PlayerController class every update
    public void UpdateMouseData(Vector2 newMousePosition)
    {
        mousePosition = newMousePosition;
    }
}
