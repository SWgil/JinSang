using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject ButtonCanvas;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleCanvas(menuCanvas.GetComponent<Canvas>());
            toggleCanvas(ButtonCanvas.GetComponent<Canvas>());
        }
    }

    private void toggleCanvas(Canvas canvas)
    {
        canvas.enabled = !canvas.enabled;
    }
}
