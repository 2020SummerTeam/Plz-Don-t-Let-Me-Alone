using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WordDrag : MonoBehaviour
{
    private GameObject target;



    void FixedUpdate()

    {

        Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z));

        if (Input.GetMouseButtonDown(0))
        {

            CastRay();

            if (target == this.gameObject)
            {
                OnMouseDown();
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
    IEnumerator OnMouseDown() // 단어 드래그 드랍
    {
        Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z));


        while (Input.GetMouseButton(0))
        {
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            Vector3 worldpos = curPosition;

            if (worldpos.x < -5f)  // 플랫폼 이동 범위 제한
                worldpos.x = -5f;
            if (worldpos.y < -4f)
                worldpos.y = -4f;
            if (worldpos.x > 0f)
                worldpos.x = 0f;
            if (worldpos.y > 4f)
                worldpos.y = 4f;

            transform.position = worldpos;
            yield return null;
        }
    }
}


