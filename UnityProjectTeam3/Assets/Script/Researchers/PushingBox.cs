using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushingBox : MonoBehaviour
{
    private Rigidbody2D mRB;
    private Animator mAnim;
    private BoxCollider2D mCollider;
    private Transform mTr;
    public GameObject pushingBoxObj;     //내가 밀고있는 상자

    [SerializeField]
    private float mSpeed;
    public float horizontal;

    [HideInInspector]
    public bool IsInteracObj;

    public bool watchingRight;
    bool isPushingBox;          //상자를 미는중인지 알아야 호출을 하빈다


    void Start()
    {
        mRB = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mCollider = GetComponent<BoxCollider2D>();
        isPushingBox = false;
        pushingBoxObj = gameObject;
    }


    void Update()
    {
        mRB.velocity = new Vector2(horizontal * mSpeed, mRB.velocity.y);
        //rigidbody의 Velocity를 정하면 그방향으로 움직인다.

        //걷는 애니메이션 및 걷는 방향으로 보는 것 구현
        if (horizontal < 0)
        {
            //박스와 상호작용 하고있는 상태라면
            if (isPushingBox)
            {
                if (watchingRight)
                {
                    //기존에 오른쪽을 보다가 왼쪽으로 돌아온거라면 당기는 거겟죠
                    //당기는거는 방향을 바꿔줄 필요가 없습니다. 애니메이션이 나오면 여기에다가 추가합시다
                    mAnim.SetBool("Push", true);
                }
                else
                {
                    //이거는 그냥 미는거겠죠 미는애니메이션이 나오면 여기에다가 추가합시다
                    //그리고 보는방향이 왼쪽에서 왼쪽으로 동일하니까, 방향 굳이 바꿔줄 필요 없죠?
                    //그러니까 pushingBox일때는 우리 아무것도 건들지 맙시다
                    mAnim.SetBool("Push", true);
                }
            }
            else
            {
                //이거를 else로 놓아주는 이유는 보는방향이 바뀌면 안되니까
                watchingRight = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                mAnim.SetBool("Push", true);
            }

        }
        else if (horizontal > 0)
        {
            //이거는 위에거랑 정반대죠. 그냥 반대로만 합시다
            if (isPushingBox)
            {
                if (watchingRight)
                {
                    //기존에 오른쪽을 보다가 오른쪽으로 돌아온거라면 미는거겠죠
                    //그리고 보는방향이 오른쪽에서 오른쪽으로 동일하니까, 방향 굳이 바꿔줄 필요 없죠?
                    mAnim.SetBool("Push", true);
                }
                else
                {
                    //당기는거는 방향을 바꿔줄 필요가 없습니다. 애니메이션이 나오면 여기에다가 추가합시다
                    //그러니까 pushingBox일때는 우리 아무것도 건들지 맙시다
                    mAnim.SetBool("Push", true);
                }
            }
            else
            {
                //이거를 else로 놓아주는 이유는 보는방향이 바뀌면 안되니까
                watchingRight = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                mAnim.SetBool("Push", true);
            }


        }
        else
        {
            mAnim.SetBool("Push", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("InteractObj"))
        {
            IsInteracObj = true;
            if (!isPushingBox)
            {
                if (transform.position.y - collision.transform.position.y > 1.5f)
                {
                    return;
                }
                isPushingBox = true;
                pushingBoxObj = collision.gameObject;   //save first, to use it in update()
                pushingBoxObj.transform.SetParent(transform);   //set parent to player
            }

        }

    }

}


