using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Researchers : MonoBehaviour
{
    // Ctrl 스크립트는 좌 우 반전만 합니다
    private Animator mAnim;
    private Transform mTr;
    public float coolTime = 4.0f;  // 좌 우 번갈아보는 시간

    // player 발견
    public GameObject findSign; // 느낌표
    public bool isFind; // player를 발견했을 경우 (stageManager에서 넘어옴)
    public Transform playerTr; // target
    public GameObject player;   // 인게임에서 해주면 됩니다

    public float distance = 4.0f;
    public bool GoOn = false;  // 연구원들이 필요한 곳까지 계속 이동하게 하기 위한 코드

    public bool Spin = true;
    private int LorR;
    public int EachNum; // 각각 스테이지에서 설정해주세요.
                        /* 
                         * researchers가 왼쪽을 바라볼 때 player를 발견해야 한다면 -1,
                         * 오른쪽을 바라볼 때 player를 발견해야 한다면 1로 설정해주면 됩니다
                         * LorR과 일치할 때 player를 발견하고 쫓습니다
                         */
    private float timer;     /* City1 에서 연구원이 플레이어를 발견하고 나서 좀 지나고 움직이도록 만들기 위해서*
                              * GoOn이 참일때만 적용가능하다.*/

    void Start()
    {
        //GetComponent로 초기화.
        mAnim = GetComponent<Animator>();
        mTr = GetComponent<Transform>();

        coolTime = 4.0f;
        isFind = false;
        findSign.SetActive(false);
        playerTr = player.GetComponent<Transform>();
        timer = 0.0f;
    }

    void Update()
    {

        if (Spin)
        {
            // 4초마다 오브젝트 회전
            if (coolTime > 0)
            {
                coolTime -= Time.deltaTime;
            }
            else
            {
                // 회전 (좌<->우)
                if (mTr.rotation == Quaternion.Euler(0, 0, 0)) // y가 180일 때 좌, 0일 때 우
                {
                    mTr.rotation = Quaternion.Euler(0, 180, 0); // 좌
                    LorR = -1;
                }
                else
                {
                    mTr.rotation = Quaternion.Euler(0, 0, 0);   // 우
                    LorR = 1;
                }

                coolTime = 4.0f;    // 4초로 초기화
            }
        }

        if ((LorR == EachNum) && (isFind == true)) // player가 박스 콜라이더에 있을 때
        {
            Spin = false;
            findSign.SetActive(true);
            mAnim.SetBool("Run", true);
            if(!GoOn)
            {
                Vector3 newPos = new Vector3(playerTr.position.x, mTr.position.y, mTr.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime);
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= 0.7)
                {
                    Vector3 newPos = new Vector3(mTr.position.x + distance, mTr.position.y, mTr.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * 0.7f);
                }
            }
            
            if(playerTr.position.x < transform.position.x)  // player가 연구원의 좌측에 있을 때
            {
                mTr.rotation = Quaternion.Euler(0, 180, 0); // 좌
            }
            else
            {
                mTr.rotation = Quaternion.Euler(0, 0, 0);   // 우
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<PlayerCtrl>().enabled = false;
            mAnim.SetBool("Run", false);
            Debug.Log("Game Over");
        }
    }
}

