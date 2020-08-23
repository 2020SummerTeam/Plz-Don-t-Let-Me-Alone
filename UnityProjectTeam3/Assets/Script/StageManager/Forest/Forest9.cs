using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest9 : MonoBehaviour
{
    public GameObject movingPlatform;
    private Transform mTr;
    private Vector3 pos, oldpos;
    private float min_x_scale;
    private float max_x_scale;
    public float distance = 2.5f;

    // 플레이어 컨트롤
    private Rigidbody2D pRB;
    [SerializeField]
    private Vector2 pJumpVector;
    private Animator pAnim;
    private int count = 1;   // 한번만 점프하도록 제한
    public float cooltime = 0.5f;  // 점프 쿨타임
    private float t;

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
        mTr = movingPlatform.GetComponent<Transform>();
        max_x_scale = mTr.position.x + distance;
        min_x_scale = mTr.position.x - distance;

        parents = GameObject.Find("Parents").GetComponent<ParentsCtrl>();
        pRB = player.GetComponent<Rigidbody2D>();
        playerCtrl = player.GetComponent<PlayerCtrl>();
        pAnim = player.GetComponent<Animator>();
        t = cooltime;
    }

    void Update()
    {
        if (parents.stageClear) // 클리어 이벤트 발동을 위해
        {
            playerCtrl.enabled = true;
            this.enabled = false;
        }
        else
        {
            if (t >= 0)
            {
                count = 1;
                t -= Time.deltaTime;
            }
            else
            {
                if (count == 1)
                {
                    pRB.AddForce(pJumpVector, ForceMode2D.Impulse); // 점프
                    count--;
                }
                t = cooltime;
            }

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
                if (mTr.position.x >= max_x_scale)
                {
                    mTr.position = new Vector3(max_x_scale, mTr.position.y, mTr.position.z);
                }
                else if (mTr.position.x <= min_x_scale)
                {
                    mTr.position = new Vector3(min_x_scale, mTr.position.y, mTr.position.z);
                }

            }
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