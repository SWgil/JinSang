using System.Resources;
using UnityEngine;


public class ChasingCamera : MonoBehaviour
{

    void Start()
    {

    }

    //현재는 게임화면을 입력처리해서 프리하게 볼수있는 카메라 테스트
    void Update()
    {
        MoveCamera();

    }

    // for testing
    private void MoveCamera()
    {
        float moveZ = 0f;
        float moveX = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveZ += 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveZ -= 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }

        transform.Translate(new Vector3(moveX, 0f, moveZ) * 0.1f);
    }



    /* 
    TODO 쓰러지는 도미노를 추적하게끔 해야함.
    
    private void Update() {
   
    if(!GameManager.instance.isGameover)
        transform.Translate(Vector3.left*speed *Time.deltaTime);
    }
    */

}
