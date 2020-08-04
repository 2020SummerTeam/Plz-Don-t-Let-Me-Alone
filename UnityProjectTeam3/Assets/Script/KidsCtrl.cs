using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidsCtrl : MonoBehaviour
{
    // Ctrl 스크립트는 좌 우 반전만 합니다

    private Animator mAnim;
    private SpriteRenderer renderer;
    public float coolTime = 4.0f;  // 좌 우 번갈아보는 시간

    public GameObject findMark; // kids가 발견하면 

    void Start()
    {
        //GetComponent로 초기화.
        mAnim = GetComponent<Animator>();   // 임시로 player animator을 쓰고 있지만 나중에 kids로 수정
        renderer = GetComponent<SpriteRenderer>();

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
            if (renderer.flipX) // true일 때 좌, false일 때 우
            {
                renderer.flipX = false;
            }
            else
            {
                renderer.flipX = true;
            }

            coolTime = 4.0f;    // 4초로 초기화
        }
    }

}
