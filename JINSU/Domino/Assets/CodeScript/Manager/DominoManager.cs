using System.Collections.Generic;
using UnityEngine;

public class DominoManager : MonoBehaviour
{
    // Singleton, not thread safe
    public static DominoManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    public GameObject startingDomino;
    private List<GameObject> dominoes = new List<GameObject>();

    public void ActivateStartingDomino()
    {
        startingDomino.GetComponent<StartingDomino>().Activate();
    }


    public void RegisterObject(GameObject domino)
    {
        dominoes.Add(domino);
    }

    public void ResetAllDominoes()
    {
        startingDomino.GetComponent<StartingDomino>().Reset();

        foreach (GameObject domino in dominoes)
        {
            domino.GetComponent<ResetableDomino>().Reset();
        }
    }
}


