using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//Courtine의 경우는 접근제한자 (prviate,public,protected를 사용하지않음)

public class GameManager : MonoBehaviour
{
    //외부 코드 에서 싱글톤 객체 접근용도
    public static GameManager Instance;

    /// <summary>
    /// 게임 판정+유저 상태용 변수
    /// </summary>
    public bool isGameOn = true; //게임 실행여부
    public bool isGamePlaying = false;  //진행여부(일시정지)
    public bool isGameOver = false;     //승패
    private int score = 0;          //최단갯수 판정용
    private int dominoCount = 0;    //사용한 도미노갯수

    /// <summary>
    /// 해당 씬에 하나는 꼭 필요함. 근데 public??
    /// </summary>
    public GameObject invisibleBall;
    public GameObject firstDomino;
    public GameObject lastDomino;

    /// <summary>
    /// UI용 변수
    /// </summary>
    public Text timeText;
    public Text recordText;
    private float surviveTime;
    private float totalPlayTime;
    public Text scoreText = null;
    public GameObject gameOverUI; //게임 오버시 활성화할 UI게임 오브젝트. //끝났을 경우 스코어텍스트로 "END"출력 예정


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("씬에 게임매니저가 두개입니다.");
            Destroy(gameObject); //자신의 게임오브젝트, 여기서는 Game_Manager 클래스(혹은 인스턴스)를 의미
        }
    }
    void Start()
    {

        isGameOver = false;
        isGamePlaying = true;

    }

    void OnApplicationPause(bool pauseStatus)
    {
        isGamePlaying = pauseStatus;

        if (isGamePlaying == true)
        {
            // TODO : 일시정지 상태에서 어떤 화면을 띄울지 UI파트에서 결정
            // 메뉴씬을 불러올것인지 게임씬 위에 옵션 창을 띄울것인지 ex (LoadMenuScene or LoadOptionScene)
        }
    }

    void Update()
    {

        surviveTime += Time.deltaTime;

        //게임오버가 되었고, 스페이스바를 누르면 활성화된 씬을 다시 불러온다. --> 스테이지 재시작
        // if (isGameOver == true && Input.GetKeyDown(KeyCode.Space)) //게임오버판정이 없기때문에, 테스트단계에선 미뤄둠.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //TODO
            //재시작할경우 사라져버리는 object가 있었음(null에러)
            //오브젝트매니저도 없고 오브젝트풀도없고 이래저래 없었음.
            //게임매니저가 두개입니다. debuglog가 뜸.
            //그리고 재시작의 경우 GetActiveScene().buildIndex사용추천
        }


        //게임 중단
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }

        //게임종료
        if (Input.GetKeyDown(KeyCode.X))
        {
            Application.Quit(); //유니티 에디터에서는 무시됨.
            UnityEditor.EditorApplication.isPlaying = false; //에디터에서 해당 역할 대체, 에디터에서 플레이->정지됨.
        }

        //TODO
        //0.메뉴를 기준으로, 게임을 시작했는지 안했는지(특정 씬이 로드 됬는지 안됬는지 확인)

        //1.ObjectManager에서 도미노의 갯수를 가져와야함.
        //1-1.처음 도미노, 끝도미노가 다 쓰러졌다면 게임판정

        //2.유저의 입력 처리(도미노생성+메뉴,씬전환 등)
        //2-1.게임 종료,메뉴화면(혹은 옵션) 활성화는 구현.
        //2-2.UI 구성에 따라 변경 가능성 매우 많음.

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

    //TODO : 어떤데이터를 저장할것인지 고민. 마찬가지로 UI에 구현해야할 필요성?
    //1.클리어한 스테이지번호.
    //1-1.클리어한 스테이지정보 :  클리어한 시간+도미노 사용갯수.
    //2.총 플레이,클리어시간계산

    public void Save()
    {

        //PlayerPrefs.SetString("StageNumber", SceneManager.GetActiveScene()); //스테이지번호

        //"시간" + SetTime((int)surviveTime); 분:초 형태
        PlayerPrefs.SetFloat("ClearTime", surviveTime); //클리어시간
        PlayerPrefs.SetInt("DominoCount", dominoCount); //도미노사용갯수

    }

    string SetTime(int t)
    {
        string min = (t / 60).ToString();
        if (int.Parse(min) < 10) min = "0" + min;
        string sec = (t % 60).ToString();
        if (int.Parse(sec) < 10) sec = "0" + sec;
        return min + ":" + sec;
    }

    //DB에서 Game으로 데이터 Load
    //Path : 
    //regedit - > HKEY_CURRENT_USER -> Software -> Unity -> UnityEditor -> [CompanyName]->[ProductName] // 5.x -> EditorPrefs

    public void Load()
    {
        if (PlayerPrefs.HasKey("StageNumber"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("StageNumber"));
        }
        totalPlayTime += PlayerPrefs.GetFloat("surviveTime");
    }


}
