//Made by: Mathias Sorin
//Last updated: 11/05/2021

using UnityEngine;

public class FloatingTextDisplay : MonoBehaviour
{
    [SerializeField] private GameObject floatingText;

    public void DisplayFloatingNumber(float number, Transform transform, float offset, Color color, float duration = 0.5f)
    {
        GameObject textObj = Instantiate(floatingText, OffsetTransformVector3(transform, offset), OffsetRotationVector3(transform));
        string text = Mathf.RoundToInt(number).ToString();
        textObj.GetComponent<FloatingText>().Initialize(text, duration, color);
    }

    public void DisplayFloatingText(string text, Transform transform, float offset, Color color, float duration = 0.5f)
    {
        GameObject textObj = Instantiate(floatingText, OffsetTransformVector3(transform, offset), OffsetRotationVector3(transform));
        textObj.GetComponent<FloatingText>().Initialize(text, duration, color);
    }

    private Vector3 OffsetTransformVector3(Transform transform, float offset)
    {
        return new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + offset + Random.Range(-0.2f, 0.8f), transform.position.z + Random.Range(-0.2f, 0.2f));
    }

    private Quaternion OffsetRotationVector3(Transform transform)
    {
        //Random range rotation around the forward axis
        Quaternion quaternion = Quaternion.AngleAxis(Random.Range(-35, 35), transform.forward);
        //Multiply by lookrotation of player camera to rotate facing player + random (multiply quaternion to add them up)
        quaternion = quaternion * Quaternion.LookRotation(GameManager.Instance.playerCamera.transform.forward, Vector3.up);
        return quaternion;
    }
}
