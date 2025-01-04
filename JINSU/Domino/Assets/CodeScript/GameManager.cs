using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//Courtine�� ���� ���������� (prviate,public,protected�� �����������)

public class GameManager : MonoBehaviour
{
    //�ܺ� �ڵ� ���� �̱��� ��ü ���ٿ뵵
    public static GameManager Instance;

    /// <summary>
    /// ���� ����+���� ���¿� ����
    /// </summary>
    public bool isGameOn = true; //���� ���࿩��
    public bool isGamePlaying = false;  //���࿩��(�Ͻ�����)
    public bool isGameOver = false;     //����
    private int score = 0;          //�ִܰ��� ������
    private int dominoCount = 0;    //����� ���̳밹��

    /// <summary>
    /// �ش� ���� �ϳ��� �� �ʿ���.
    /// </summary>
    public GameObject invisibleBall; 
    public GameObject firstDomino; 
    public GameObject lastDomino;

    /// <summary>
    /// UI�� ����
    /// </summary>
    public Text timeText;
    public Text recordText;
    private float surviveTime;
    public Text scoreText = null;
    public GameObject gameOverUI; //���� ������ Ȱ��ȭ�� UI���� ������Ʈ. //������ ��� ���ھ��ؽ�Ʈ�� "END"��� ����


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("���� ���ӸŴ����� �ΰ��Դϴ�.");
            Destroy(gameObject); //�ڽ��� ���ӿ�����Ʈ, ���⼭�� Game_Manager Ŭ����(Ȥ�� �ν��Ͻ�)�� �ǹ�
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        isGameOver = false;
        isGamePlaying = true;

    }

    void OnApplicationPause(bool pauseStatus)
    {
        isGamePlaying = pauseStatus;

        if (isGamePlaying == true )
        {
            // LoadMenuScene or LoadOptionScene
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���ӿ����� �Ǿ���, �����̽��ٸ� ������ Ȱ��ȭ�� ���� �ٽ� �ҷ��´�. --> �������� �����
        if (isGameOver == true && Input.GetKeyDown(KeyCode.Space)) 
        {
               SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }


        //���� �ߴ�

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
        //��������
        if (Input.GetKeyDown(KeyCode.X))
        {
            Application.Quit(); //����Ƽ �����Ϳ����� ���õ�.
            UnityEditor.EditorApplication.isPlaying = false; //�����Ϳ��� �ش� ���� ��ü, �����Ϳ��� �÷���->������.
        }

        //TODO
        //0.�޴��� ��������, ������ �����ߴ��� ���ߴ���(Ư�� ���� �ε� ����� �ȉ���� Ȯ��)

        //1.ObjectManager���� ���̳��� ������ �����;���.
        //1-1.ó�� ���̳�, �����̳밡 �� �������ٸ� ��������

        //2.������ �Է� ó��(���̳����+�޴�,����ȯ ��)
        //2-1.���� ����,�޴�ȭ��(Ȥ�� �ɼ�) Ȱ��ȭ�� ����.
        //2-2.UI ������ ���� ���� ���ɼ� �ſ� ����.

    }


    public void SetPause()
    {
        OnApplicationPause(!isGamePlaying);

    }

    public void AddScore(int newScore)
    {
        if (isGameOver == false)
        {
            score += newScore;
            scoreText.text = "Score " + score;
        }
        else
        {
            scoreText.text = "END";
        }
    }
    public void OnPlayerDead()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }
}
