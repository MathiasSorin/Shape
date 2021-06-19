using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Destroy", 0.8f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(GameManager.Instance.playerCamera.transform.forward, Vector3.up);
    }
}
