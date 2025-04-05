using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;
public class DominoUI : MonoBehaviour
{
    private bool isMenuVisible = false;
    private GameObject circularMenu = null;
    private GameObject canvasObject = null;
    private Canvas canvas = null;

    public delegate void ButtonAction();
    private Dictionary<string, ButtonAction> buttonActions;
    void Start()
    {
        buttonActions = new Dictionary<string, ButtonAction>
        {
            { "이동", Move },
            { "회전", Rotate },
            { "메뉴닫기", CloseMenu },
            { "삭제", DeleteDomino }
        };
        if (canvasObject == null)
        {
            CreateCanvas();
        }
        if (circularMenu == null)
        {
            circularMenu = CreateCircularMenu();
            //ToDo : 동일 버튼이 만들어지는버그를 수정해야함.
            AddButtonsToCircularMenu(circularMenu, buttonActions);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ToggleCircularMenu();
        }
    }


    GameObject CreateCircularMenu()
    {
        if (GameObject.Find("CircularMenu") == null)
        {
            circularMenu = new GameObject("CircularMenu");
            circularMenu.layer = LayerMask.NameToLayer("UI");
            RectTransform rectTransform = circularMenu.AddComponent<RectTransform>();
            circularMenu.AddComponent<CanvasRenderer>();
            Image image = circularMenu.AddComponent<Image>();

            circularMenu.transform.SetParent(canvasObject.transform, false);
            // UI 설정
            image.color = new Color(0.2f, 0.2f, 0.2f, 0.8f); // 반투명 회색 배경
            rectTransform.sizeDelta = new Vector2(200, 200); // 크기 설정
            rectTransform.position = Input.mousePosition; // 마우스 위치에 생성

            isMenuVisible = true;
            return circularMenu;
        }
        else
        {
            return GameObject.Find("CircularMenu");
        }
    }

    void Move()
    {
        Debug.Log("이동 기능 실행");
    }

    void Rotate()
    {
        Debug.Log("회전 기능 실행");
    }

    void CloseMenu()
    {
        Debug.Log("메뉴 닫기 기능 실행");
    }

    void DeleteDomino()
    {
        Debug.Log("도미노 삭제 기능 실행");
    }
    void AddButtonsToCircularMenu(GameObject circularMenu, Dictionary<string, ButtonAction> actions)
    {

        int buttonCount = actions.Count;

        float angleStep = 360f / buttonCount;

        int i = 0;
        foreach (var action in actions)
        {
            GameObject button = new GameObject($"Button{action.Key}");
            button.transform.SetParent(circularMenu.transform, false);

            RectTransform rectTransform = button.AddComponent<RectTransform>();
            button.AddComponent<Image>();

            Button buttonComponent = button.AddComponent<Button>();

            // 버튼 위치 계산
            float angle = i * angleStep;
            float x = Mathf.Sin(angle * Mathf.Deg2Rad) * 80; // 80은 반지름
            float y = Mathf.Cos(angle * Mathf.Deg2Rad) * 80;
            rectTransform.anchoredPosition = new Vector2(x, y);
            rectTransform.sizeDelta = new Vector2(40, 40); // 버튼 크기

            GameObject textObject = new GameObject("ButtonText");
            textObject.transform.SetParent(button.transform, false);
            Text textComponent = textObject.AddComponent<Text>();
            textComponent.text = action.Key;
            textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            textComponent.alignment = TextAnchor.MiddleCenter;
            textComponent.color = Color.black;

            RectTransform textRectTransform = textComponent.GetComponent<RectTransform>();
            textRectTransform.anchorMin = Vector2.zero;
            textRectTransform.anchorMax = Vector2.one;
            textRectTransform.sizeDelta = Vector2.zero;
            // 버튼 기능 추가

            ButtonAction currentAction = action.Value;
            buttonComponent.onClick.AddListener(() => currentAction());
            i++;
        }
    }

    void CreateCanvas()
    {
        if (GameObject.Find("Domino_UI") == null) //캔버스생성
        {
            canvasObject = new GameObject("Domino_UI");
            canvasObject.layer = LayerMask.NameToLayer("UI");
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
        }
    }
    void ToggleCircularMenu()
    {
        circularMenu.GetComponent<RectTransform>().position = Input.mousePosition;
        circularMenu.SetActive(isMenuVisible);
        isMenuVisible = !isMenuVisible;
    }
}
