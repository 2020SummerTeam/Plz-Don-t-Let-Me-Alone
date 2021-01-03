using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest15 : MonoBehaviour
{
    public GameObject target;   // 터치한 타겟
    private Transform mTr;  // 타겟의 Transform 컴포넌트
    private Vector3 pos, oldpos;
    public GameObject ballon;   // 말풍선

    public GameObject box;
    public bool expandBox = false;
    public GameObject parents;
    public bool expandPa = false;
    public GameObject researchers;
    public bool expandRe = false;

    ParentsCtrl parentsCtrl;
    private BoxCollider2D paCol;
    private Rigidbody2D paRB;


    public GameObject findSign;
    public findEvent findEvent;    // player가 findEvent 박스 콜라이더 안에 있는지 전달받기 위해 사용
    private Researchers Researchers; // researchers 스크립트
    private BoxCollider2D ReCol;
    private Rigidbody2D ReRB;

    void Start()
    {
        // parents init
        parentsCtrl = parents.GetComponent<ParentsCtrl>();
        parentsCtrl.enabled = false;
        paCol = parents.GetComponent<BoxCollider2D>();
        paRB = parents.GetComponent<Rigidbody2D>();
        paRB.constraints = RigidbodyConstraints2D.FreezePositionY;  // Y축 못 움직이게
        paCol.isTrigger = true; // 스쳐 지나갈 수 있게

        // researchers init
        findEvent = GameObject.Find("findEvent").GetComponent<findEvent>();
        Researchers = researchers.GetComponent<Researchers>();
        Researchers.enabled = false;
        Researchers.EachNum = 0;
        ReCol = researchers.GetComponent<BoxCollider2D>();
        ReRB = researchers.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Researchers
        if (findEvent.CanFind == true)  // player가 findEvent 박스 콜라이더 안에 있을 때
        {
            Researchers.isFind = true; // player를 발견했을때 // Researchers 스크립트의 변수 수정
        }

        if (parentsCtrl.stageClear && parentsCtrl.enabled) // 클리어 이벤트 발동(부모 이동->플레이어 이동)을 위해
        {
            this.enabled = false;
        }

        // ballon 위치 마우스로 제어
        if (mTr != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                pos = mTr.position;
            }
            if (Input.GetMouseButton(0))
            {
                oldpos = pos;
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                mTr.position = new Vector3(mTr.position.x + (pos.x - oldpos.x), mTr.position.y + (pos.y - oldpos.y), 0);
            }
        }
        

        // 오브젝트 확대
        if (expandBox)  // 상자
        {
            if (box.transform.localScale.x < 1.7f)
                box.transform.localScale += new Vector3(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, 0f);
            else
                box.transform.localScale = new Vector3(1.7f, 1.7f, 0f);
        }
        if (expandPa && parentsCtrl.enabled == false)   // 부모
        {
            paRB.constraints = RigidbodyConstraints2D.None; // FreezeY 해제
            paCol.isTrigger = false;    // stageClear 가능하게

            if (parents.transform.localScale.x < 0.6f)
                parents.transform.localScale += new Vector3(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, 0f);
            else
            {
                parents.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
                parentsCtrl.enabled = true; // 스크립트 활성화
            }
        }
        if (expandRe && Researchers.enabled == false)   // 연구원
        {
            if (researchers.transform.localScale.x < 0.6f)
                researchers.transform.localScale += new Vector3(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, 0f);
            else
            {
                researchers.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
                researchers.transform.rotation = Quaternion.Euler(0, 180, 0);
                ReRB.constraints = RigidbodyConstraints2D.FreezePositionY;  // Y축 못 움직이게
                ReCol.isTrigger = true;
                Researchers.enabled = true;
            }
        }
    }


    void OnGUI()
    {

        Event e = Event.current;
        if (e.clickCount == 1)
        {
            CastRay();
            if(target == null)
            {
                return;
            }
            if (target == ballon)
            {
                mTr = ballon.GetComponent<Transform>();
                
            }
            else
            {
                mTr = null;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == box)
        {
            expandBox = true;
        }
        if (collision.gameObject == parents)
        {
            expandPa = true;
        }
        if (collision.gameObject == researchers)
        {
            expandRe = true;
        }
    }
}