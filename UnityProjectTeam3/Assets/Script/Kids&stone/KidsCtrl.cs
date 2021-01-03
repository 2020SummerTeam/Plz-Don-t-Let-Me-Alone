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
    public bool watchingLeft = false;
    private GameObject findSign;

    void Start()
    {
        //GetComponent로 초기화.
        mAnim = GetComponent<Animator>();
        mTr = GetComponent<Transform>();
        findSign = transform.GetChild(0).gameObject;
        watchingLeft = false;
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

            if(findSign.activeSelf == true) // findSign이 활성화되어 있으면 비활성화시킨다음 회전
            {
                findSign.SetActive(false);
            }
            // 회전 (좌<->우)
            if (mTr.rotation == Quaternion.Euler(0, 0, 0)) // y가 180일 때 좌, 0일 때 우
            {
                watchingLeft = true;
                mTr.rotation = Quaternion.Euler(0, 180, 0); // 좌
            }
            else
            {
                watchingLeft = false;
                mTr.rotation = Quaternion.Euler(0, 0, 0);   // 우
            }

            coolTime = 4.0f;    // 4초로 초기화
            
        }
    }

}
