using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidsCtrl : MonoBehaviour
{
    private Animator mAnim;
    private Transform mTr;

    private float coolTime = 4.0f;  // 좌 우 번갈아보는 시간
    StoneZone StoneZone; // player가 StoneZone에 있는지

    void Start()
    {
        //GetComponent로 초기화.
        mAnim = GetComponent<Animator>();
        mTr = GetComponent<Transform>();

        coolTime = 4.0f;
        StoneZone = GameObject.Find("StoneZone").GetComponent<StoneZone>();
    }

    void Update()
    {
        // 4초마다 오브젝트 회전
        if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
        else
        {
            // 회전 (좌<->우)
            if (mTr.rotation.y == 180)   // Kids와 Player가 마주보게 됨
            {
                if (StoneZone.isStoneZone)  //  + 돌을 던질 위치에 player 있는지 확인
                {
                    GameObject.Find("Stone").GetComponent<ThrowStone>().Throw();
                    Debug.Log("throw Stone!!");
                    // Game Over
                }
                else
                {
                    mTr.rotation = Quaternion.Euler(0, 180, 0);
                } 
            }
            else
            {
                mTr.rotation = Quaternion.Euler(0, 180, 0);
            }

            coolTime = 4.0f;    // 4초로 초기화
        }


    }

}
