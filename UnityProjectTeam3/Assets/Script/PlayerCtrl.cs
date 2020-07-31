using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody2D mRB;
    //플레이어의 rigidbody

    private Animator mAnim;
    //플레이어의 animator

    private BoxCollider2D mCollider;

    [SerializeField]
    private float mSpeed;
    [SerializeField]
    private Vector2 mJumpVector;
    //player의 speed와 jumpVector인데, private이지만 serializeField가 붙어서 Unity의 Inspector에서 정해준다.

    ParentsCtrl parents;


    // Start is called before the first frame update
    void Start()
    {
        //GetComponent로 초기화.
        mRB = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mCollider = GetComponent<BoxCollider2D>();


        // stage clear
        parents = GameObject.Find("Parents").GetComponent<ParentsCtrl>();
    }


    // Update is called once per frame
    void Update()
    {
        if (parents.stageClear == false)
        {
            float horizontal = Input.GetAxis("Horizontal");
            //입력받는부분

            mRB.velocity = new Vector2(horizontal * mSpeed, mRB.velocity.y);
            //rigidbody의 Velocity를 정하면 그방향으로 움직인다.

            //걷는 애니메이션 및 걷는 방향으로 보는 것 구현
            if (horizontal < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                mAnim.SetBool(AnimHash.RUN, true);
            }
            else if (horizontal > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                mAnim.SetBool(AnimHash.RUN, true);
            }
            else
            {
                mAnim.SetBool(AnimHash.RUN, false);
            }

            //기어코 하기싫었던 update에 SetFloat넣기를 했습니다..
            //저는 업데이트에 입력 이외에걸 넣는걸 싫어하지만, 저희게임에서는 그닥 문제는 없을것 같습니다
            mAnim.SetFloat(AnimHash.JUMP, mRB.velocity.y);
            if (mRB.velocity.y <= 0.3f && mRB.velocity.y >= -0.3f)
            {
                mAnim.SetBool(AnimHash.IDLE, true);
            }
            else
            {
                mAnim.SetBool(AnimHash.IDLE, false);

            }

            //점프모션 및 점프 입력받기.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("doing jumo");
                Jump();
            }
            
            //added because i cant use spacebar
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Debug.Log("doing jumo");
                Jump();
            }
        }
        else
        {
            // 이동 멈춤
            mAnim.SetBool(AnimHash.RUN, false);
            mRB.velocity = new Vector2(0, mRB.velocity.y);

            if (parents.lefttime < 1)
            {
                // 테스트용. 맵이 바뀌면 회전값 바꿀 예정입니다. 
                //2020 07 30 sanghun making stage 1. i find rotation problem. bu tak hae yo~~
                transform.rotation = Quaternion.Euler(0, 180, 0);
                mAnim.SetBool(AnimHash.RUN, true);
                mRB.velocity = new Vector2((-1) * mSpeed, mRB.velocity.y);

                if (parents.lefttime <= 0)
                {
                    mAnim.SetBool(AnimHash.RUN, false);
                    mRB.velocity = new Vector2(0, mRB.velocity.y);
                }
            }
        }
    }



    //j버튼 누르면 점프가 발동되게 할 것이다.
    public void Jump()
    {
        //점프가 아닐 때만 위로 힘을 준다!
        if (mAnim.GetFloat(AnimHash.JUMP) == 0)
        {
            mRB.AddForce(mJumpVector, ForceMode2D.Impulse);
        }


    }

}
