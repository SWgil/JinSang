using UnityEngine;

public class ResetableDomino : MonoBehaviour
{
    private Vector3 originPosition;
    private Quaternion originRotation;

    void Start()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
    }

    public void Reset()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;
    }
}
