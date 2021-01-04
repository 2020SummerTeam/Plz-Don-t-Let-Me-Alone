using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleClick : MonoBehaviour
{
    public GameObject target;
    public GameObject bee;
    public AudioSource audio;
    Event e = null;

    void OnGUI()
    {

        Event e = Event.current;
        if (e.clickCount == 2)
        {
            CastRay();
            if(target == null)
            {
                return;
            }
            if (target.name == "BigBox" || target.name == "parents_door")
            {
                audio.Play();
                target.SetActive(false);
            }
            else if (target.name == "tree")
            {
                audio.Play();
                bee.SetActive(true);
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
