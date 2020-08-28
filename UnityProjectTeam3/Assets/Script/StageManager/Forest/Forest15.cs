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
    Researchers researchersCtrl;
    ParentsCtrl parentsCtrl;
    public GameObject findSign;

    public findEvent findEvent;    // player가 findEvent 박스 콜라이더 안에 있는지 전달받기 위해 사용

    void Start()
    {
        findEvent = GameObject.Find("findEvent").GetComponent<findEvent>();
        researchersCtrl = researchers.GetComponent<Researchers>();
        researchersCtrl.enabled = false;
        parentsCtrl = parents.GetComponent<ParentsCtrl>();
    }

    void Update()
    {

        // Researchers
        if (findEvent.CanFind == true)  // player가 findEvent 박스 콜라이더 안에 있을 때
        {
            // player를 발견
            findSign.SetActive(true);

            // 따라가야 하는 것 추가
            // +애니메이션
        }

        if (parentsCtrl.stageClear) // 클리어 이벤트 발동(부모 이동->플레이어 이동)을 위해
        {
            this.enabled = false;
        }
        else
        {
            // ballon 위치 마우스로 제어
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

            if(expandBox)
            {
                if (box.transform.localScale.x < 1.7f)
                    box.transform.localScale += new Vector3(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, 0f);
                else
                    box.transform.localScale = new Vector3(1.7f, 1.7f, 0f);
            }
            if(expandPa)
            {
                if (parents.transform.localScale.x < 0.6f)
                    parents.transform.localScale += new Vector3(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, 0f);
                else
                    parents.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
            }
            if(expandRe)
            {
                if (researchers.transform.localScale.x < 0.6f)
                    researchers.transform.localScale += new Vector3(Time.deltaTime * 0.3f, Time.deltaTime * 0.3f, 0f);
                else
                {
                    researchers.transform.localScale = new Vector3(0.6f, 0.6f, 0f);
                    researchers.transform.rotation = Quaternion.Euler(0, 180, 0); 
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
        if(collision.gameObject == box)
        {
            expandBox = true;
        }
        if (collision.gameObject == parents)
        {
            expandPa = true;
        }
        if(collision.gameObject == researchers)
        {
            expandRe = true;
        }
    }
}
