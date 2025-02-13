using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float lifeTime = 0.8f;
    private void Awake()
    {
        Invoke("Destroy", lifeTime);
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
