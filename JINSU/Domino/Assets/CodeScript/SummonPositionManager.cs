using JetBrains.Annotations;
using UnityEngine;

public class SummonPositionManager : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject gameObject;

    public bool fixedX = false;
    public bool fixedY = false;
    public bool fixedZ = false;


    void Update()
    {
        UpdatePosition();

        if (Input.GetMouseButtonDown(0))
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
        Instantiate(gameObject, transform.position, transform.rotation);
    }
}
