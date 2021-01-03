using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Forest12 : MonoBehaviour
{
    public SpriteRenderer targetRend;
    public GameObject target;   // 투명도 조절 타겟 (터치한 타겟)
    public GameObject[] platform = new GameObject[5];   // 투명도 조절 타겟 (타겟 후보)
    public SpriteRenderer[] platformRend = new SpriteRenderer[5];   // 버튼 눌렀을 때 투명하게 하기 위해
    public BoxCollider2D[] platformTouchArea = new BoxCollider2D[5]; // 주변(터치하면 모습이 보이는 영역) // 2개의 박스콜라이더가 존재할 때 위에 있는 걸로 할당함
    public GameObject player;
    public PlayerCtrl playerCtrl;

    void Start()
    {
        for (int i = 0; i <= 4; i++)
        {
            platformRend[i] = platform[i].GetComponent<SpriteRenderer>();
            platformTouchArea[i] = platform[i].GetComponent<BoxCollider2D>();
        }
    }

    private void Update()
    {
        if (player.transform.position.y < -5)
        {
            playerCtrl.OnStageFail();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // 플레이어가 버튼 눌렀을 때, 바닥 플랫폼 제외하고 투명해짐
        {
            Debug.Log("1");
            for (int i = 0; i <= 3; i++)
            {
                platformRend[i].enabled = false;    // 투명해짐
            }
        }
    }


    void OnGUI()
    {

        Event e = Event.current;
        if (e.clickCount == 1)
        {
            CastRay();

            if (target == platform[0] || target == platform[1] || target == platform[2] || target == platform[3])
            {
                targetRend = target.GetComponent<SpriteRenderer>();
                targetRend.enabled = true;  // 보여짐
            }
            else if (target == platform[4]) // 바닥 플랫폼
            {
                targetRend = target.GetComponent<SpriteRenderer>();
                targetRend.enabled = false; // 투명해짐
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


}
