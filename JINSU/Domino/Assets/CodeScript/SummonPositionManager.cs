using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class SummonPositionManager : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject prefab;

    public bool fixedX = false;
    public bool fixedY = false;
    public bool fixedZ = false;

    void Start()
    {
        appendPrefab();
    }

    private void appendPrefab()
    {
        GameObject newObject = Instantiate(prefab, transform.position, transform.rotation);

        Destroy(newObject.GetComponent<Rigidbody>());
        newObject.GetComponent<BoxCollider>().isTrigger = true;

        newObject.transform.SetParent(this.transform);
    }

    void Update()
    {
        UpdatePosition();

        if (Input.GetMouseButtonDown(0) && isMousePointingWall())
        {
            SummonObject();
        }
    }

    private void UpdatePosition()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 nextPostion = hit.point;
            if (fixedX)
            {
                nextPostion.x = transform.position.x;
            }

            if (fixedY)
            {
                nextPostion.y = transform.position.y;
            }

            if (fixedZ)
            {
                nextPostion.z = transform.position.z;
            }

            transform.position = nextPostion;
        }
    }

    private void SummonObject()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    private bool isMousePointingWall()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, Mathf.Infinity, layerMask);
    }
}