using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Domino : MonoBehaviour
{

    private bool isCollision = false;

    void Start()
    {

    }

    void Update()
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
}