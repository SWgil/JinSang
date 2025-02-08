using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{


    private ObjectManager objectManager;

    //public으로 변경후 단일씬에 하나씩 스크립트를 gameobject형태로 넣는것을 추천하긴함.
    //책임분리와 유연성이 있다고하며, 동적생성의 이점을 상실하는 이유
    //이렇게 등록을 하게되면, 여기에서 실제 프리팹(obj를 인스턴스로 생성)

    //대신에 따로 분리하게된다면 씬의 특성에 맞는 오브젝트마다 ObjectCreator를 만들어야한다는 데요?


    private int selectedPrefabIndex = 0;

    void Awake()
    {
        objectManager = ObjectManager.Call; //초기에 오브젝트매니저를 할당.


        if (objectManager == null)
        {
            Debug.LogError("ObjectManager instance is not found.");
        }
        objectManager.prefabs.Add(LoadPrefabs("Default_Invisible_ball", "InvisibleBall"));
        objectManager.prefabs.Add(LoadPrefabs("Default_Domino_block", "Domino"));
        objectManager.prefabs.Add(LoadPrefabs("Default_Map_plane", "Plane"));

        DontDestroyOnLoad(gameObject);

    }

    void OnDestory()
    {

    }

    private GameObject LoadPrefabs(string prefabsName, string objName)
    {
        GameObject obj = (GameObject)Resources.Load("Prefabs/" + prefabsName);
        obj.name = objName;
        objectManager.RegisterObject(obj, obj.name);
        return objectManager.GetObject(obj.name);
    }
    private GameObject CreateObject(string prefabsName, string objName, Vector3 position, Quaternion rotation)
    {
        if (position == null)
        {
            position = new Vector3(0, 0, 0);
        }
        if (rotation == null)
        {
            rotation = Quaternion.identity;
        }
        GameObject newObject = Instantiate(objectManager.GetPrefabs(objName), position, rotation);
        newObject.transform.parent = objectManager.transform;
        objectManager.RegisterObject(newObject, objName);
        return newObject;
    }

    private void CreateObjectAtMousePosition(string objName)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject newObject = Instantiate(objectManager.GetPrefabs(objName), new Vector3(hit.point.x, hit.point.y + 0.75f, hit.point.z), Quaternion.identity);
            newObject.transform.parent = objectManager.transform;
            objectManager.RegisterObject(newObject, objName);
        }


    }
    //UI를 통해 선택된 프리팹을 설정
    public void SelectPrefab(int index)
    {
        if (index >= 0 && index < objectManager.prefabs.Count)
        {
            selectedPrefabIndex = index;
        }
    }
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //왼클릭으로 생성 -->동작하는중
        {
            // CreateObjectAtMousePosition("Domino");

        }

        if (Input.GetMouseButtonDown(1)) //우클릭으로 생성 -->동작하는중
        {
            CreateObjectAtMousePosition("InvisibleBall");
        }

    }
}
