using UnityEngine;

//chore : 디버그용 카메라입니다.

public class MainCamera : MonoBehaviour
{

    float rotX, rotY = 0;
    void Start()
    {

    }

    void Update()
    {
        // MoveCamera();
        // RotateCamera();
    }


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

    private void RotateCamera()
    {

        if (Input.GetKey(KeyCode.Q)) //왼쪽
        {
            rotX -= 0.1f * 400f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.E)) //오른쪽 
        {
            rotX += 0.1f * 400f * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.Z)) //아래
        {
            rotY -= 0.1f * 400f * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.C)) //위
        {
            rotY += 0.1f * 400f * Time.deltaTime;

        }
        // 카메라 각도회전각 제한
        // rotX = Mathf.Clamp(rotX, -60, 60);
        // rotY = Mathf.Clamp(rotY, -60, 60);
        this.transform.localEulerAngles = new Vector3(-rotY, rotX, 0);

    }
}
