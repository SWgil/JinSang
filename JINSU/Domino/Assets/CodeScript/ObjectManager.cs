using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    public List<GameObject> prefabs = new List<GameObject>();    //객체들의 원본프리팹
    private Dictionary<string, List<GameObject>> objectPool =
    new Dictionary<string, List<GameObject>>(); //실제 인스턴스들

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

        foreach (string key in objectPool.Keys)
        {
            UnregisterAllObject(key);
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
        List<GameObject> objectList;
        if (!objectPool.ContainsKey(name))
        {
            objectList = new List<GameObject>();
            objectPool.Add(name, objectList);
        }
        else
        {
            objectList = objectPool[name];
        }

        GameObject gameObject = Instantiate(obj, position, rotation);
        gameObject.transform.name = name;
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.transform.parent = transform;


        objectList.Add(gameObject);

        return gameObject;
    }

    // TODO: 삭제 예정, 추후 한 object들에 대한 컨트롤이 필요할 떄 구현
    public GameObject GetObject(string name)
    {
        if (objectPool.ContainsKey(name))
        {
            // TODO: 임의로 0번째 object를 반환하도록 수정했음, 삭제 필요
            return objectPool[name][0];
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

    public void UnregisterAllObject(string name)
    {
        if (objectPool.ContainsKey(name))
        {
            List<GameObject> objects = objectPool[name];
            foreach (GameObject obj in objects)
            {
                Destroy(obj);
            }

            objectPool.Remove(name);
        }
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
