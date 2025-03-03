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


    public GameObject startingDominoPrefab;
    public GameObject dominoPrefab;
    public LayerMask layerMask;
    public Transform startPotision;

    private List<GameObject> dominoes = new List<GameObject>();
    private GameObject startingDomino;

    void Start()
    {
        startingDomino = Instantiate(startingDominoPrefab, startPotision.transform.position, startPotision.transform.rotation);

        GameObject summonPosition = GetSummonPosition();
        summonPosition.GetComponent<SummonPosition>().appendPrefab(dominoPrefab);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMousePointingWall())
        {
            GameObject domino = SummonDomino();
            DominoManager dm = FindFirstObjectByType<DominoManager>();
            dm.RegisterObject(domino);
        }
    }

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
        dominoes.Clear();
    }

    private GameObject GetSummonPosition()
    {
        Transform childTransform = transform.Find("SummonPosition");
        return childTransform.gameObject;
    }


    private GameObject SummonDomino()
    {
        Transform summonPosition = GetSummonPosition().transform;
        return ObjectManager.Call.RegisterObject(dominoPrefab, summonPosition.position, summonPosition.rotation, dominoPrefab.name);
    }

    public bool isMousePointingWall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, Mathf.Infinity, layerMask);
    }
}


