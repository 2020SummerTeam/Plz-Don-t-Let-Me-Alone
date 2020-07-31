using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//question 1, where is this script's gameObject
//question 2, what's this scritps doing;
public class PlayerPush : MonoBehaviour
{
    Rigidbody2D mRB_InteractObj;
    FixedJoint2D mFixed_InteractObj;
    PlayerCtrl mPlayerCtrl;

    bool ISButtonDown;
    bool IsPush;
    void Start()
    {
        mRB_InteractObj = GetComponent<Rigidbody2D>();
        mFixed_InteractObj = GetComponent<FixedJoint2D>();
        mPlayerCtrl = GetComponent<PlayerCtrl>();
    }

    void Update()
    {
        if (ISButtonDown)
            IsPush = true;
        else
            IsPush = false;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && IsPush)
        {
            mFixed_InteractObj.enabled = false;
            Rigidbody2D collisionRB = collision.gameObject.GetComponent<Rigidbody2D>();
            mRB_InteractObj.velocity += collisionRB.velocity;
        }
        else
        {
            mFixed_InteractObj.enabled = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mFixed_InteractObj.enabled = true;
        }
    }

    public void AButtonDown(bool IsDown)
    {
        ISButtonDown = IsDown;
    }

}
