using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsCtrl : MonoBehaviour
{
    private Animator mAnim;
    private Transform mTr;

    private float coolTime = 4.0f;  // 좌 우 번갈아보는 시간
    public bool throwStone; // player와 마주칠 떄 돌을 던짐 // bool 값 바꿔 Projectile에서 참조

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent로 초기화.
        mAnim = GetComponent<Animator>();
        mTr = GetComponent<Transform>();

        coolTime = 4.0f;
        throwStone = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 4초마다 오브젝트 회전
        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
        else
        {
            // 회전
            if (mTr.rotation.y == 180)
            {
                mTr.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                mTr.rotation = Quaternion.Euler(0, 180, 0);
            }

            coolTime = 4.0f;    // 4초로 초기화
        }


    }

}
