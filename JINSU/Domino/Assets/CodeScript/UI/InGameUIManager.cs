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
        DominoManager dm = FindFirstObjectByType<DominoManager>();
        dm.RemoveAllDominoes();
    }

    public void ResetAllDominoes()
    {
        DominoManager dm = FindFirstObjectByType<DominoManager>();
        dm.ResetAllDominoes();
    }
}
