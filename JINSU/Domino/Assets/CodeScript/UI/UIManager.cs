using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startMenuPanel;

    public void StartGame()
    {
        startMenuPanel.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
