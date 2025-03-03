using UnityEngine;

public class StartingDomino : MonoBehaviour
{
    private float torquePower = 1f;
    private Vector3 torqueAxis = new Vector3(0, 0, -1);

    public void Activate()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        Vector3 localTorqueAxis = transform.TransformDirection(torqueAxis);
        rigidBody.AddTorque(localTorqueAxis * torquePower, ForceMode.Impulse);
    }

    public void Reset()
    {
        GetComponent<ResetableDomino>().Reset();
    }
}
