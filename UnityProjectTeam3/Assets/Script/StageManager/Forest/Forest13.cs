using System.Collections;
using System.Collections.Generic;
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
    private float wallLimit;    // 벽이 올라갈 수 있는 한계
    private float parentsLimit; // 부모 오브젝트가 내려갈 수 있는 한계 = 플랫폼 콜라이더 max y좌표

    private float parentsY; // 부모 bound 사이즈 절반
    public float wallSpeed; // 벽 올라가는 속도

    // button event
    public GameObject bug;


    // Start is called before the first frame update
    void Start()
    {
        isCameraMove = false;
        boundX = bound.GetComponent<Transform>().position.x;
        cameraSize = Camera.main.orthographicSize * Camera.main.aspect;
        gameoverY = gameover.GetComponent<Transform>().position.y;

        isParentsMove = false;
        pTr = parents.GetComponent<Transform>();
        parentsLimit = gameover.GetComponent<BoxCollider2D>().bounds.max.y;
        parentsY = parents.GetComponent<BoxCollider2D>().bounds.size.y / 2;
        wallTr = wall.GetComponent<Transform>();
    }

    // Update is called once per frame
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
                    oldpos = pTr.position;
                    isParentsMove = true;
                    Debug.Log("1");
                }

            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (isParentsMove == true)
            {
                oldpos = pos;
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                pTr.position = new Vector3(pTr.position.x, pTr.position.y + (pos.y - oldpos.y), 0);
                wallTr.position = new Vector3(wallTr.position.x, wallTr.position.y - (pos.y - oldpos.y) * wallSpeed, 0);
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
        if(pTr.position.y - parentsY <= parentsLimit) // 부모 오브젝트가 바닥에 닿으면
        {
            isParentsMove = false;
        }

        // player game over
        if(player.transform.position.y < gameoverY)
        {
            Debug.Log("gameOver");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            // button event
        }
    }


}
