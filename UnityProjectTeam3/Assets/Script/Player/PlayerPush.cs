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
    public float distance = 1f;   //ray를 쏘는 거리
    public LayerMask boxMask;     //밀리는 object를 검출할 layer
    float horizontal;
    GameObject box;     //ray에 부딪친 물체 가져올 gameObject

    bool ISButtonDown;
    bool IsPush;   //button이 눌리면 true, 떼지면 false
    void Start()
    {
        mBoxPull = GetComponent<BoxPull>();    //BoxPull script 가져오기
        mPlayerCtrl = GetComponent<PlayerCtrl>();
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit;

        if (horizontal > 0)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);  //오른쪽으로 이동시 오른쪽으로 ray쏘기
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance, boxMask); //왼쪽으로 이동시 왼쪽으로 ray쏘기
        }

        if (hit.collider != null && hit.collider.CompareTag("InteractObj") && IsPush)   //충돌체가 null이 아니고 Tag가 InteractObj고, 버튼이 눌렸다면
        {
            box = hit.collider.gameObject;
            box.transform.parent = this.transform;
            box.GetComponent<BoxPull>().beingPushed = true;

        }
        else if (!IsPush && hit.collider != null && hit.collider.CompareTag("InteractObj"))
        {
            hit.transform.parent = null;
            hit.collider.GetComponent<BoxPull>().beingPushed = false;
            mPlayerCtrl.IsInteracObj = true;   //interactObj가 검출되었고 상호작용버튼이 눌리지 않았다면 true return
        }
        else if(hit.collider == null)
        {
            mPlayerCtrl.IsInteracObj = false; //아무것도 검출이 안되었다면 return false
        }

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

    //화면상 ray 그려주기
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (horizontal > 0)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
        }
        else
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * distance);
        }
    }

    //A Button이 눌렸는지 체크해주는 함수
    public void AButtonDown(bool IsDown)
    {
        ISButtonDown = IsDown;
    }

}
