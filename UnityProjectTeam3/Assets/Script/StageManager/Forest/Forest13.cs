using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Forest13 : MonoBehaviour
{
    // swipe camera
    public GameObject swipeArea;    // 해당 영역 내에서 스와이프
    private bool isCameraMove;  // 스와이프 영역에서 카메라 움직이려고 하는지
    private Vector2 startPos;   // 스와이프 시작 위치
    private float speed = 0f;   // 스와이프 속도
    public GameObject camera;
    private float cameraSize;    // 카메라 가로 절반 사이즈
    public GameObject bound; // 카메라 이동 가능 범위 구하기 위해
    private float boundX;

    // game over
    public GameObject player;
    public GameObject gameover; // 플랫폼 위치를 기준으로 아래로 내려가면 Game Over
    private float gameoverY;

    // stage clear (parents)
    public GameObject parents;
    public GameObject wall;     // 부모 오브젝트 가로막고 있는 벽
    private Transform pTr;  // parents Transform
    private Transform wallTr;
    public bool isParentsMove;
    private Vector2 pos, oldpos;    // 부모 오브젝트 position
    private float platform_maxY; // 부모 오브젝트가 내려갈 수 있는 한계 = 플랫폼 콜라이더 max y좌표

    private float parentsPosY; // 시작할 때의 부모 머리 위 y좌표
    private Transform parents_minY; // 부모 발 밑 y좌표의 transform
    public float wallSpeed; // 벽 올라가는 속도

    // button event
    public GameObject bug;
    private bool BugActive;
    private Transform bugTr;
    private int direction = 1;

    void Start()
    {
        // camera moving init
        isCameraMove = false;
        boundX = bound.GetComponent<Transform>().position.x;
        cameraSize = Camera.main.orthographicSize * Camera.main.aspect;
        gameoverY = gameover.GetComponent<Transform>().position.y;

        // parents no moving & platform init
        isParentsMove = false;
        pTr = parents.GetComponent<Transform>();
        platform_maxY = gameover.GetComponent<BoxCollider2D>().bounds.max.y; // 바닥 플랫폼 콜라이더 max y좌표
        parents_minY = parents.transform.GetChild(0).transform;  // 부모 첫번째 자식 오브젝트의 transform
        parentsPosY = parents.transform.GetChild(1).transform.position.y;  // 부모 두번째 자식 오브젝트의 시작할 때의 y좌표
        wallTr = wall.GetComponent<Transform>();

        // button event
        bug.SetActive(false);
        BugActive = false;
        bugTr = bug.GetComponent<Transform>();
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))     // 터치 이벤트
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 터치 위치
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Vector2.zero, 0f);
            if (hitInformation.collider != null)
            {
                GameObject touchedObject = hitInformation.transform.gameObject; // 터치한 위치에 있는 오브젝트

                if ((touchedObject == swipeArea) && (swipeArea != null))    // swipeArea가 비어있지 않고, 터치한 오브젝트가 swipeArea 일 때
                {
                    startPos = Input.mousePosition;
                    isCameraMove = true; // 영역 내에서 터치 시작했을 때에 
                }
                if (touchedObject == parents)
                {
                    pos = pTr.position;
                    isParentsMove = true;
                }

            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (isParentsMove == true)
            {
                oldpos = pos;
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                float distance = pos.y - oldpos.y;
                if (distance < 0)
                {
                    pTr.position = new Vector3(pTr.position.x, pTr.position.y + distance, 0);
                    wallTr.position = new Vector3(wallTr.position.x, wallTr.position.y - distance * wallSpeed, 0);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isCameraMove)   // 카메라 이동 이벤트
            {
                Vector2 endPos = Input.mousePosition;
                float swipeLength = (endPos.x - startPos.x);    // 왼쪽으로 스와이프 했을 때만
                if (swipeLength < 0)
                {
                    swipeLength /= -1000;
                    speed = 0.2f * swipeLength;
                }
                isCameraMove = false;
            }
            if (isParentsMove)
            {
                isParentsMove = false;
            }
        }

        // 카메라 스와이프
        if ((speed > 0) && (camera.transform.position.x + cameraSize < boundX))  // 카메라가 이동할 수 있는 한계까지
        {
            camera.transform.Translate(speed, 0, 0);
            speed *= 0.98f; // 속도 감속
        }
        else if (camera.transform.position.x >= boundX - cameraSize)
        {
            speed = 0;
            swipeArea = null; 
        }

        // parents 
        if(parents_minY.position.y <= platform_maxY) // 부모 오브젝트가 바닥에 닿으면
        {
            isParentsMove = false;
        }

        // player game over // bug와 닿거나, 플랫폼 아래로 떨어질 때
        if((player.transform.position.y < gameoverY) || (player.transform.position.x == bug.transform.position.x))
        {
            Debug.Log("gameOver");
        }

        // button trigger
        if(BugActive)
        {
            bug.transform.position += new Vector3(Time.deltaTime * 2f, Time.deltaTime * direction, 0);
            if (bugTr.position.y >= -3.0f)
            {
                direction = -1;
            }
            else if (bugTr.position.y <= -3.3f)
            {
                direction = 1;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            bug.SetActive(true);
            BugActive = true;
        }
    }

}
