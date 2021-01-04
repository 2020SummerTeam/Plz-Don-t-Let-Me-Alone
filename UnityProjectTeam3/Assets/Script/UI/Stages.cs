using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stages : MonoBehaviour
{
    // 나타낼 발자국 개수
    int CurrentStage;
    int ClearStage;

    // 발자국
    public GameObject[] ParentsStep = new GameObject [25];
    public GameObject[] SandyStep = new GameObject [25];


    // swipe camera
    private bool isCameraMove;  // 스와이프 영역에서 카메라 움직이려고 하는지
    private Vector2 startPos;   // 스와이프 시작 위치
    private float speed = 0f;   // 스와이프 속도
    public GameObject camera;
    private float cameraSize;    // 카메라 가로 절반 사이즈
    public GameObject bound; // 카메라 이동 가능 범위 구하기 위해
    public GameObject target;
    private float boundX;
    private float boundsize;

    void Awake()
    {
        GameLoad();
        Show();

        isCameraMove = false;
        boundX = bound.GetComponent<BoxCollider2D>().bounds.center.x;
        boundsize = bound.GetComponent<BoxCollider2D>().size.x / 2;
        cameraSize = Camera.main.orthographicSize * Camera.main.aspect;

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))     // 터치 이벤트
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 터치 위치
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Vector2.zero, 0f);
            if (hitInformation.collider != null)
            {
                startPos = Input.mousePosition;
                isCameraMove = true; // 영역 내에서 터치 시작했을 때에 

            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isCameraMove)   // 카메라 이동 이벤트
            {
                Vector2 endPos = Input.mousePosition;
                float swipeLength = (endPos.x - startPos.x);
                swipeLength /= -1000;
                speed = 0.2f * swipeLength;

                isCameraMove = false;
            }
        }

        // 카메라 스와이프
        if (speed > 0)
        {
            if (camera.transform.position.x + cameraSize < boundX + boundsize)  // 오른쪽으로 카메라가 이동할 수 있는 한계까지

            {
                camera.transform.Translate(speed, 0, 0);
                speed *= 0.98f; // 속도 감속
            }
            else
            {
                speed = 0;
            }
        }
        else if (speed < 0)
        {
            if (camera.transform.position.x - cameraSize > boundX - boundsize)   //  왼쪽으로 카메라가 이동할 수 있는 한계까지
            {

                camera.transform.Translate(speed, 0, 0);
                speed *= 0.98f; // 속도 감속
            }
            else
            {
                speed = 0;
            }
        }

    }


    public void Show()
    {
        if (CurrentStage >= 2)
        {
            CurrentStage -= 2;
            for (int p = 0; p <= CurrentStage; p++)
            {
                ParentsStep[p].gameObject.SetActive(true);
            }
        }
        if (ClearStage >= 2)
        {
            ClearStage -= 2;
            for (int s = 0; s <= ClearStage; s++)
            {
                SandyStep[s].gameObject.SetActive(true);
            }
        }
    }


    void OnGUI()
    {

        Event e = Event.current;
        if (e.clickCount == 1)
        {
            CastRay();
            if (target == null)
            {
                return;
            }
            for(int i = 0;i<25;i++)
            {
                GameObject obj = ParentsStep[i];
                if (target.gameObject == obj)
                {
                    Debug.Log("로딩됐어");
                    SceneManager.LoadScene(i+2);

                }
            }


        }
    }

    void CastRay() // 유닛 히트처리 부분.  레이를 쏴서 처리합니다. 
    {

        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);



        if (hit.collider != null)   //히트되었다면 여기서 실행
        {

            target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정

        }

    }


    // 데이터 불러오기
    public void GameLoad()
    {
        CurrentStage = PlayerPrefs.GetInt("CurrentStage");    // 제일 높은 스테이지 번호
        ClearStage = PlayerPrefs.GetInt("ClearStage");    // 마지막 스테이지 클리어
    }


    // button
    public void OnClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
