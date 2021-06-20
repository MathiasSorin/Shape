//Made by: Mathias Sorin
//Last updated: 19/06/2021

using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float rotationSpeed;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
