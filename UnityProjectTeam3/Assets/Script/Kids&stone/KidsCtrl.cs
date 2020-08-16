using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidsCtrl : MonoBehaviour
{
    // KidsCtrl 스크립트는 좌 우 반전만 합니다

    private Animator mAnim;
    private Transform mTr;
    public float coolTime = 4.0f;  // 좌 우 번갈아보는 시간


    void Start()
    {
        //GetComponent로 초기화.
        mAnim = GetComponent<Animator>();
        mTr = GetComponent<Transform>();

        coolTime = 4.0f;
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
            if (mTr.rotation == Quaternion.Euler(0, 180, 0)) // true일 때 좌, false일 때 우
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
