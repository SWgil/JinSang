using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    public List<GameObject> prefabs = new List<GameObject>();    //객체들의 원본프리팹
    private Dictionary<string, GameObject> objectPool =
    new Dictionary<string, GameObject>(); //실제 인스턴스들
    // TODO: 이름별로 cnt 관리 필요
    int cnt = 0;

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
        if (instance == null)
            return;
        foreach (KeyValuePair<string, GameObject> obj in objectPool)
        {
            Destroy(obj.Value);
        }
        objectPool.Clear();
        objectPool = null;
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
            RegisterObject(obj, Vector3.zero, Quaternion.identity, name);
        }
    }

    public GameObject RegisterObject(GameObject obj, Vector3 position, Quaternion rotation, string name)
    {
        if (objectPool.ContainsKey(name))
        {
            cnt++;
            name = name + cnt;
        }

        GameObject gameObject = Instantiate(obj, position, rotation);
        gameObject.transform.name = name;
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.transform.parent = transform;

        objectPool.Add(name, gameObject);

        return gameObject;
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
                if (obj == null)

                {
                    continue;
                }

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
