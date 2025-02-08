using System.Collections.Generic;
using UnityEngine;

public class DominoManager : MonoBehaviour
{
    // Singleton, not thread safe
    static DominoManager instance = null;
    public static DominoManager Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = new GameObject(typeof(DominoManager).Name);
                instance = obj.AddComponent<DominoManager>();
            }
            return instance;
        }
    }


    public GameObject startingDomino;
    private List<GameObject> dominoes = new List<GameObject>();
    public GameObject dominoPrefab;

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

    public void RemoveAllDominoes()
    {
        ObjectManager.Call.UnregisterAllObject(dominoPrefab.name);
    }
}


