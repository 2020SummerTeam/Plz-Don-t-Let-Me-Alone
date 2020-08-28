using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest9 : MonoBehaviour
{

    public GameObject target;   // 터치한 타겟
    public GameObject[] m_platform = new GameObject[2];   // 타겟 후보 // 0 이 왼쪽, 1 이 오른쪽
    private Transform mTr;  // 타겟의 Transform 컴포넌트
    private Vector3 pos, oldpos;
    private float []min_x_scale = new float[2];
    private float []max_x_scale = new float[2];
    private int platform_num;

    PlayerCtrl playerCtrl; // 스테이지 클리어시 playerCtrl 스크립트 활성화
    public GameObject player;
    ParentsCtrl parents;

    // 버튼 컨트롤 // 플레이어와 부딪힐 시 부모 위 플랫폼 활성화
    public bool buttonTriggerd = false;
    public GameObject platform;

    public Transform originObj;
    public Transform reflectObj;


    void Start()
    {
        // Moving Platform 범위 지정
        max_x_scale[0] = m_platform[0].GetComponent<Transform>().position.x + 5;
        min_x_scale[0] = m_platform[0].GetComponent<Transform>().position.x;
        max_x_scale[1] = m_platform[1].GetComponent<Transform>().position.x + 2;
        min_x_scale[1] = m_platform[1].GetComponent<Transform>().position.x;

        parents = GameObject.Find("Parents").GetComponent<ParentsCtrl>();
        playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    void Update()
    {
        if (parents.stageClear) // 클리어 이벤트 발동(부모 이동->플레이어 이동)을 위해
        {
            playerCtrl.enabled = true;
            this.enabled = false;
        }
        else
        {
            // Moving Platform 마우스로 제어
            if (Input.GetMouseButtonDown(0))
            {
                pos = mTr.position;
            }
            if (Input.GetMouseButton(0))
            {
                oldpos = pos;
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                mTr.position = new Vector3(mTr.position.x + (pos.x - oldpos.x), mTr.position.y, 0);

                // MovingPlatform 범위 제한
                if (mTr.position.x >= max_x_scale[platform_num])
                {
                    mTr.position = new Vector3(max_x_scale[platform_num], mTr.position.y, mTr.position.z);
                }
                else if (mTr.position.x <= min_x_scale[platform_num])
                {
                    mTr.position = new Vector3(min_x_scale[platform_num], mTr.position.y, mTr.position.z);
                }

            }
        }
    }
    

    void OnGUI()
    {

        Event e = Event.current;
        if (e.clickCount == 1)
        {
            CastRay();

            if (target == m_platform[0])
            {
                mTr = m_platform[0].GetComponent<Transform>();
                platform_num = 0;
            }
            else if (target == m_platform[1])
            {
                mTr = m_platform[1].GetComponent<Transform>();
                platform_num = 1;
            }
            else
            {
                mTr = null; // 다른 곳을 선택했을 때
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            buttonTriggerd = true;
            platform.SetActive(true);
            Debug.Log(reflectObj.position);
        }
    }
}