using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{


    private ObjectManager objectManager;

    //public으로 변경후 단일씬에 하나씩 스크립트를 gameobject형태로 넣는것을 추천하긴함.
    //책임분리와 유연성이 있다고하며, 동적생성의 이점을 상실하는 이유
    //이렇게 등록을 하게되면, 여기에서 실제 프리팹(obj를 인스턴스로 생성)

    //대신에 따로 분리하게된다면 씬의 특성에 맞는 ObjectCreator를 만들어야한다는 데요?
    private int selectedPrefabIndex = 0;
    float fixedY;

    void Awake()
    {
        objectManager = ObjectManager.Call; //초기에 할당해줘버림.
        if (objectManager == null)
        {
            Debug.LogError("ObjectManager instance is not found.");
        }
       
        fixedY = transform.position.y;
        objectManager.prefabs.Add(CreateObjectWithPrefabs("Default_Invisible_ball", "InvisibleBall"));
        objectManager.prefabs.Add(CreateObjectWithPrefabs("Default_Domino_block", "Domino"));
        // objectManager.prefabs.Add( CreateObjectWithPrefabs("Default_Map_plane", "Plane"));

        DontDestroyOnLoad(gameObject);

    }

    void OnDestory()
    {
        int k = 0;
    }
    //TODO
    //ObjectManager의 Prefabs에 등록.
    //Layer에는 등록이안되는?
    private GameObject CreateObjectWithPrefabs(string prefabsName, string objName)
    {
        GameObject obj = Instantiate((GameObject)Resources.Load("Prefabs/" + prefabsName), new Vector3(0, 0, 0), Quaternion.identity);
        obj.name = objName;
        objectManager.RegisterObject(obj, obj.name);
        return objectManager.GetObject(obj.name);
    }
  
    private void CreateObjectAtMousePosition()
    {

          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject newObject = Instantiate(objectManager.GetPrefabs("Domino"), new Vector3(hit.point.x,hit.point.y+0.75f,hit.point.z), Quaternion.identity);
                newObject.transform.parent = objectManager.transform;
                objectManager.RegisterObject(newObject,"Domino");
            }

        // Vector3 mousePosition = Input.mousePosition;
        // mousePosition.z = 10;
        // Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // RaycastHit hit;
        // if (Physics.Raycast(ray, out hit))
        // {
        //     Vector3 worldPosition = hit.point;
        //     // worldPosition.z = 10;  //여기 분명히 꼬일거임. //자동완성 코드인데 뭔소리임? worldPosition.z = hit.point.z; 로 수정해야함.
        //     GameObject obj = objectManager.MoveObject("Domino", worldPosition, Quaternion.identity);
        //     //GameObject obj = objectManager.MoveObject(objPrefabs[selectedPrefabIndex], worldPosition, Quaternion.identity);
        //     //int objectId = objectManager.Instanace.RegisterObject(obj.name);

        //     obj.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // }
    }
    //UI를 통해 선택된 프리팹을 설정
    public void SelectPrefab(int index)
    {
        if (index >= 0 && index < objectManager.prefabs.Count)
        {
            selectedPrefabIndex = index;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //선택으로 생성 -->동작하는중
        {
         //   CreateObjectAtMousePosition();
        }
        /*
        if (Input.GetMouseButton(0) && selectedObject != null) //드래깅
        {
            //     DragObject();
        }
        if (Input.GetMouseButtonUp(0) && selectedObject != null)  //선택해제
        {
            //     DeselectObject();
        }
        */
    }
}
