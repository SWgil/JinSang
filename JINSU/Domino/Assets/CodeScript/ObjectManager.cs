using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    public List<GameObject> prefabs = new List<GameObject>();    //객체들의 원본프리팹
    private Dictionary<string, GameObject> objectPool = 
    new Dictionary<string, GameObject>(); //실제 인스턴스들


    public static ObjectManager Call
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<ObjectManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("ObjectManager");
                    instance = container.AddComponent<ObjectManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void MemoryDelete()
    {
        if(instance == null)
            return; 
        foreach (KeyValuePair<string, GameObject> obj in objectPool)
        {
            Destroy(obj.Value);
        }
        objectPool.Clear();
        objectPool=null;
    }
    void OnDestroy()
    {
        MemoryDelete();
        instance = null;
    }

    public void RegisterObject(GameObject obj, string name)
    {
        if (!objectPool.ContainsKey(name))
        {
            GameObject clone = Instantiate(obj) as GameObject;
            clone.transform.name = name;
            clone.transform.localPosition = Vector3.zero;
            clone.SetActive(false);
            clone.transform.parent = transform;
            objectPool.Add(name, obj);
        }
    }

    //UnUsed Code
    public GameObject MoveObject(string objName, Vector3 pos, Quaternion rot)
    {
        if (objectPool.ContainsKey(objName))
        {

            if (pos == null)
            {
                pos = Vector3.zero;
            }
            else
            {
                objectPool[objName].transform.position = pos;
            }

            if (rot == null)
            {
                rot = Quaternion.identity;
            }
            else
            {
                objectPool[objName].transform.rotation = rot;
            }
        }
        return objectPool[objName];
    }
    public GameObject GetObject(string name)
    {
        if (objectPool.ContainsKey(name))
        {
            return objectPool[name];
        }
        return null;
    }
    public GameObject GetPrefabs(string name)
{
    if (prefabs.Count > 0)
    {
        foreach (GameObject obj in prefabs)
        {
            if(obj==null)
                continue;
            else if (obj.name == name)
            {
                return obj;
            }
        }
    }
    return null;
}
    public void UnregisterObject(string name)
    {
        if (objectPool.ContainsKey(name))
        {
            objectPool.Remove(name);
        }
    }

    public Dictionary<string, GameObject> GetObjectPool()
    {
        return objectPool;
    }

    public int GetObjectCount()
    {
        return objectPool.Count;
    }

    void Start()
    {


    }

    void Update()
    {

    }
}
