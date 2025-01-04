using UnityEngine;

public class InvisibleBall : MonoBehaviour
{
    public float damage = 20.0f;
    public float speed = 1.2f;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            GetComponent<Rigidbody>().AddForce(speed * -1, 0, 0, ForceMode.Impulse);
        }

    }


    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Tag_DominoBlock"))
        {
            Debug.Log(string.Format("{0} and {1} is Collsion Enter ", gameObject.name, collisionInfo.gameObject.name));
            Destroy(gameObject);
        }

        //공과 도미노가 충돌처리는 됐지만, 도미노들이 우주 유영하듯 둥둥 떠다니는게 문제임. 
    }

}
