using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public void StartRound()
    {

        DominoManager dm = FindFirstObjectByType<DominoManager>();
        dm.ActivateStartingDomino();
    }

    public void RestartRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResetAllDominoes()
    {
        DominoManager dm = FindFirstObjectByType<DominoManager>();
        dm.ResetAllDominoes();
    }
}
