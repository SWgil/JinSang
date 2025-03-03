using UnityEngine;

public class StartingDomino : MonoBehaviour
{
    private float torquePower = 1f;
    private Vector3 torqueAxis = new Vector3(1, 0, 0);

    public void Activate()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddTorque(torqueAxis * torquePower, ForceMode.Impulse);
    }

    public void Reset()
    {
        GetComponent<ResetableDomino>().Reset();
    }
}
