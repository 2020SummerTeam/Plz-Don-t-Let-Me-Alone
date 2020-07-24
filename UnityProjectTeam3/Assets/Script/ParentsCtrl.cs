﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsCtrl : MonoBehaviour
{
    private Rigidbody2D mRB;
    private Animator mAnim;
    private Transform mTr;

    public bool stageClear;
    private float coolTime = 2.0f;
    public float lefttime = 2.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent로 초기화.
        mRB = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mTr = GetComponent<Transform>();
       
        stageClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stageClear)
        { 
            // 테스트용. 맵이 바뀌면 회전값 바꿀 예정입니다. 
            mTr.rotation = Quaternion.Euler(0, 180, 0); // 오브젝트 회전
            mAnim.SetBool(AnimHash.RUN, true);  // 이동 애니메이션
            mRB.velocity = new Vector2(-3, mRB.velocity.y);  // 이동

            if(lefttime > 0)
            {
                lefttime -= Time.deltaTime;
            }
            else
            {
                mAnim.SetBool(AnimHash.RUN, false);
                mRB.velocity = new Vector2(0, mRB.velocity.y);
                lefttime = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && stageClear == false)
        {
            stageClear = true;
            lefttime = coolTime;
        }
    }

}