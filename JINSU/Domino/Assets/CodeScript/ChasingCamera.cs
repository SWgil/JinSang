using System.Resources;
using UnityEngine;


public class ChasingCamera : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    //����� ����ȭ���� �Է�ó���ؼ� �����ϰ� �����ִ� ī�޶� �׽�Ʈ

    // Update is called once per frame
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
    private void Update() {
    // ���� ������Ʈ�� �������� ���� �ӵ��� ���� �̵��ϴ� ó��
    //�������� ���̳븦 �����ϰԲ� �ؾ���.
    if(!GameManager.instance.isGameover)
        transform.Translate(Vector3.left*speed *Time.deltaTime);
    }
    */

}
