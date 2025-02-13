//Made by: Mathias Sorin
//Last updated: 28/05/2021

using UnityEngine;

//Script for the behavior of the floating text prefab
public class FloatingText : MonoBehaviour
{
    private void Update()
    {
        //Text will turn towards player camera
        transform.rotation = Quaternion.LookRotation(GameManager.Instance.playerCamera.transform.forward, Vector3.up);
    }

    //Called by FloatingTextDisplay class to initialize the properties of the text
    public void Initialize(string text, float duration, Color color)
    {
        TextMesh textMesh = GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.characterSize = UnityEngine.Random.Range(1f, 2.5f);
        textMesh.color = color;
        Invoke("Destroy", duration);
    }

    //Destroy (might change for pooling later)
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
