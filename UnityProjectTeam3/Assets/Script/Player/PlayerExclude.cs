using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//캐릭터드래그와 오브젝트 드래그 중복 막는 함수
public class PlayerExclude : MonoBehaviour
{
    public GameObject target;



    void FixedUpdate()

    {

        if (Input.GetMouseButtonDown(0))
        {

            CastRay();

            if (target == this.gameObject)
            {
                gameObject.GetComponent<PlayerCtrl>().enabled = false;

            }
          
        }

    }



    void CastRay() // 유닛 히트처리 부분.  레이를 쏴서 처리합니다. 

    {

        target = null;



        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);



        if (hit.collider != null)
        {
            target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정

        }
    }
}
