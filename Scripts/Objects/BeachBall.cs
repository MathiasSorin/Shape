//Made by: Mathias Sorin
//Last updated: 18/06/2021

using UnityEngine;

public class BeachBall : MonoBehaviour
{
    public float force;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
        }
    }
}
