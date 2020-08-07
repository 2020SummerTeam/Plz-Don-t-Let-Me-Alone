using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//question 1, where is this script's gameObject
//player한테 붙여서 player가 ray로 object를 검출하게
//question 2, what's this scritps doing;
//상호작용 버튼이 눌리고, 부딪친 물체가 InteractObj라면 밀리는 물체가 player를 따라가게
public class PlayerPush : MonoBehaviour
{
    BoxPull mBoxPull;
    PlayerCtrl mPlayerCtrl;

    bool ISButtonDown;
    bool IsPush;   //button이 눌리면 true, 떼지면 false
    void Start()
    {
        mBoxPull = GetComponent<BoxPull>();    //BoxPull script 가져오기
        mPlayerCtrl = GetComponent<PlayerCtrl>();
    }
    void Update()
    {

        //A Button이 눌렸으면 박스가 움직일 수 있게 하고, 마우스가 버튼에서 떼지면 못움직이게 
        if (ISButtonDown)
        {
            IsPush = true;
        }
        else if (!ISButtonDown)
        {
            IsPush = false;
        }
    }

    //A Button이 눌렸는지 체크해주는 함수
    public void AButtonDown(bool IsDown)
    {
        ISButtonDown = IsDown;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractObj") && IsPush)
        {
            collision.transform.parent = this.transform;
            collision.gameObject.GetComponent<BoxPull>().beingPushed = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractObj"))
        {
            collision.transform.parent = null;
            collision.gameObject.GetComponent<BoxPull>().beingPushed = false;
            mPlayerCtrl.IsInteracObj = true;   //interactObj가 검출되었고 상호작용버튼이 눌리지 않았다면 true return
        }
        else
        {
            mPlayerCtrl.IsInteracObj = false;
        }
    }
}
