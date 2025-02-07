using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public GameObject startDomino;

    public void StartRound()
    {
        startDomino.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void ResetRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
