using System.Security.Cryptography;
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
        Rigidbody rb = GetComponent<Rigidbody>();

        if (!rb.isKinematic)
        {
            rb.position = originPosition;
            rb.rotation = originRotation;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        else
        {
            transform.position = originPosition;
            transform.rotation = originRotation;
        }
    }
}
