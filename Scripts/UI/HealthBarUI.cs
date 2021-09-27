//Made by: Mathias Sorin
//Last updated: 31/05/2021

using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(GameManager.Instance.playerCamera.transform.forward, Vector3.up);
    }
}
