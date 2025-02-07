using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    public GameObject startDomino;

    public void StartRound()
    {
        startDomino.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ResetRound()
    {

    }
}
