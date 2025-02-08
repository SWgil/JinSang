using UnityEngine;

public class StartingDomino : MonoBehaviour
{
    public void Activate()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }

    public void Reset()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<ResetableDomino>().Reset();
    }
}
