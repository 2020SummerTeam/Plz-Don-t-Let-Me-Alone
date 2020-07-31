using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//question 1, where is this script's gameObject
//밀리는 물체한테 붙여야됨
//question 2, what's this scritps doing;
//상호작용 버튼이 눌리고, 부딪친 물체가 player라면 밀리는 물체가 player를 따라가게
public class PlayerPush : MonoBehaviour
{
    Rigidbody2D mRB_InteractObj;
    FixedJoint2D mFixed_InteractObj;
    PlayerCtrl mPlayerCtrl;
    BoxPull mBoxPull;

    bool ISButtonDown;
    bool IsPush;
    void Start()
    {
        mRB_InteractObj = GetComponent<Rigidbody2D>();
        mFixed_InteractObj = GetComponent<FixedJoint2D>();
        mPlayerCtrl = GetComponent<PlayerCtrl>();
        mBoxPull = GetComponent<BoxPull>();
    }
    void Update()
    {
        //A Button이 눌렸으면 박스가 움직일 수 있게 하고, 마우스가 버튼에서 떼지면 못움직이게 
        if (ISButtonDown)
        {
            IsPush = true;
            mBoxPull.beingPushed = true;
        }
        else if (!ISButtonDown)
        {
            IsPush = false;
            mBoxPull.beingPushed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌한 물체가 player 고, 마우스가 눌렸다면 fixed joint = true, 박스가 플레이어를 따라가게
        if (collision.gameObject.CompareTag("Player") && ISButtonDown)
        {
            Rigidbody2D collisionRB = collision.gameObject.GetComponent<Rigidbody2D>();
            mFixed_InteractObj.connectedBody = collisionRB;
            mFixed_InteractObj.enabled = true;

        }
        else if (!ISButtonDown)
        {
            mFixed_InteractObj.enabled = false;
            //마우스가 버튼에서 떼졌다면 fixed joint를 false 로
        }
    }

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        mFixed_InteractObj.enabled = false;
    //    }
    //}

    //A Button이 눌렸는지 체크해주는 함수
    public void AButtonDown(bool IsDown)
    {
        ISButtonDown = IsDown;
    }

}
