using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterHitPlayer : MonoBehaviour
{
    PlayerCtrl Ctrl;    // player 오브젝트의 PlayerCtrl 스크립트
    ThrowStone Stone;   // Stone 오브젝트의 ThrowStone 스크립트
    private Animator mAnim; // 쓰러지는 모션에 필요한 애니메이터

    void Start()
    {
        Ctrl = GetComponent<PlayerCtrl>();
        Stone = GameObject.Find("Stone").GetComponent<ThrowStone>();
        mAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Stone.HitPlayer == true)    // stone을 던졌는지 체크
        {
            Ctrl.enabled = false;   // 모든 조작을 제어
            // 쓰러지는 모션 추가할 예정
        }
        else
        {
            Ctrl.enabled = true;    // 부활할 경우 다시 true 시켜야 함
        }
    }
}
