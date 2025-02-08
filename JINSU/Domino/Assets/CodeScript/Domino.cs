using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Domino : MonoBehaviour
{

    private bool isCollision = false;
    public bool isSelected = false;
    RaycastHit hit, hitFloor;
    GameObject dragAnchor;
    Vector3 mainCameraPos;
    GameObject selectedObject;
    private Rigidbody rb;

    void Awake()
    {
        mainCameraPos = Camera.main.transform.position;
        rb = GetComponent<Rigidbody>();
    }


    void Start()
    {

    }

    void Update()
    {
        GetComponent<Rigidbody>().AddForce(0, -9.8f * Time.deltaTime, 0, ForceMode.Acceleration);

        //도미노가 이상하게 회전하는것을 방지
        Vector3 rotation = transform.rotation.eulerAngles;
        if (rotation.z > 90f && rotation.z < 270f)
        {
            rotation.z = 90f;
            transform.rotation = Quaternion.Euler(rotation);
            rb.angularVelocity = Vector3.zero;

        }


    }

    void FixedUpdate()
    {

    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Tag_InvisibleBall"))
        {
            Debug.Log(string.Format("{0} and {1} is collision Enter ", gameObject.name, collisionInfo.gameObject.name));
            isCollision = true;
        }
        else if (collisionInfo.gameObject.CompareTag("Tag_DominoBlock"))
        {
            Debug.Log(string.Format("{0} and {1} is collision Enter", gameObject.name, collisionInfo.gameObject.name));
            isCollision = true;
        }
        else
            isCollision = false;
    }


    //TODO
    //도미노 블럭 위치를 드래깅.유니티 기본함수. 함수들을 이해못했으므로 이상하게 동작함 -->변경예정.

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        isSelected = true;
        //오브젝트에 마우스가 클릭되었을때
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            selectedObject = hit.collider.gameObject;
            dragAnchor = new GameObject("DragAnchor");
            dragAnchor.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            transform.SetParent(dragAnchor.transform);
        }
    }
    void OnMouseUp()
    {
        Debug.Log("OnMouseUp");

        //오브젝트에서 마우스가 떼졌을때
        isSelected = false;
        transform.SetParent(null);
        Destroy(dragAnchor);
    }
    void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (isSelected == true)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) //LayerMask 를 어떻게 처리해야할지?
            {
                float h = dragAnchor.transform.position.y;

                Vector3 camToFloor = hitFloor.point - Camera.main.transform.position;
                Vector3 nextPosition = Vector3.zero;

                float low = 0.0f, high = 1.0f;
                for (int i = 0; i < 38; i++)
                {
                    float diff = high - low;
                    float p1 = low + diff / 3;
                    float p2 = high - diff / 3;

                    Vector3 v1 = mainCameraPos + camToFloor * p1;
                    Vector3 v2 = mainCameraPos + camToFloor * p2;
                    if (Mathf.Abs(v1.y - h) > Mathf.Abs(v2.y - h))
                    {
                        nextPosition = v2;
                        low = p1;
                    }
                    else
                    {
                        nextPosition = v1;
                        high = p2;
                    }
                }
                dragAnchor.transform.position = nextPosition;
            }
        }
    }
}