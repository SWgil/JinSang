using UnityEngine;

public class StartingDomino : MonoBehaviour
{
    public void Activate()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void Reset()
    {
        GetComponent<ResetableDomino>().Reset();
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
