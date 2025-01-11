using UnityEngine;

public class InvisibleBall : MonoBehaviour
{
    public float damage = 20.0f;
    public float speed = 1.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.K))
        {
            GetComponent<Rigidbody>().AddForce(speed * -1,0, 0,ForceMode.Impulse);
        }
    }


    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("DominoBlock"))
        {
            Debug.Log(string.Format("{0} ������Ʈ�� {1} �� �浹�߽��ϴ�.", gameObject.name, collisionInfo.gameObject.name));
            Destroy(gameObject);
        }

        //���� ���̳밡 �浹ó���� ������, ���̳���� ���� �����ϵ� �յ� ���ٴϴ°� ������.
    }

}
