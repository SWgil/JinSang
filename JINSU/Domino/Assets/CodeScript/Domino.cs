using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
public class Domino : MonoBehaviour
{

    private bool isCollision = false;
    public bool isSelected = false;

    GameObject selectedObject;
    private Rigidbody rb;


    void Awake()
    {
        //ver2 mainCameraPos = Camera.main.transform.position;
        rb = GetComponent<Rigidbody>();
    }


    void Start()
    {

        yHeight = this.transform.localScale.y;

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

    RaycastHit hit, hitLayerMask;
    GameObject objectHitPosition;
    float yHeight;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.transform.position, this.transform.localRotation * Vector3.up * 3.0f);
    }

    Vector3 getContactPoint(Vector3 normal, Vector3 planeDot, Vector3 A, Vector3 B)
    {
        Vector3 nAB = (B - A).normalized;

        return A + nAB * Vector3.Dot(normal, planeDot - A) / Vector3.Dot(normal, nAB);
    }

    void OnMouseDown()
    {
        Debug.Log(string.Format("{0} is Down", gameObject.name));
        isSelected = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            objectHitPosition = new GameObject("Empty");
            objectHitPosition.transform.position = hit.point;
            this.transform.SetParent(objectHitPosition.transform);
        }
    }

    void OnMouseUp()
    {
        isSelected = false;
        Debug.Log(string.Format("{0} is Up", gameObject.name));
        this.transform.parent = null;
        Destroy(objectHitPosition);
    }

    void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.red);

        int layer = 1 << LayerMask.NameToLayer("Ground");
        if (isSelected == true)
        {
            if (Physics.Raycast(ray, out hitLayerMask, Mathf.Infinity, layer))
            {
                Vector3 normal = hitLayerMask.transform.up;
                Vector3 planeDot = hitLayerMask.point + hitLayerMask.collider.transform.up * yHeight;
                Vector3 A = Camera.main.transform.position;
                Vector3 B = hitLayerMask.point;

                this.transform.rotation
                  = Quaternion.LookRotation(hitLayerMask.collider.transform.forward);
                objectHitPosition.transform.position = getContactPoint(normal, planeDot, A, B);
                Debug.Log(string.Format("{0} is Dragging", gameObject.name));
                Debug.Log(string.Format("ray(mouse) Pos = {0}, {1}, {2}", ray.origin.x, ray.origin.y, ray.origin.z));
                Debug.Log(string.Format("obj Pos = {0}, {1}, {2}", gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
            }
        }
    }
}