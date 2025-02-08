using UnityEngine;

public class SummonPosition : MonoBehaviour
{
    public LayerMask layerMask;

    public bool fixedX = false;
    public bool fixedY = false;
    public bool fixedZ = false;

    void Update()
    {
        UpdatePosition();
    }


    public void appendPrefab(GameObject prefab)
    {
        GameObject newObject = Instantiate(prefab, transform.position, transform.rotation);

        Destroy(newObject.GetComponent<Rigidbody>());
        newObject.GetComponent<BoxCollider>().isTrigger = true;

        newObject.transform.SetParent(transform);
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
}