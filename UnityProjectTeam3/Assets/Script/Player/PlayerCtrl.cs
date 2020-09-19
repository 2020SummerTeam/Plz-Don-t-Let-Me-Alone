using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public float horizontal;

    private Vector2 preTouchPos; //used for drag movement
    private Vector2 deltaTouchPos; //used for drag movement
    private float centerOfScreen; //used for drag movement
    
    [HideInInspector]   //곰나오는 스크립트에서 써야되가지고 퍼블릭으로 바꿨습니다
    public bool IsSit;  //if drag down -> true
    public bool IsInteracObj;   //PlayerPush에서 쏜 ray에서 검출된 물체가 InteractObj라면 true

    ParentsCtrl parents;    // parents 오브젝트의 ParentsCtrl 스크립트
    Stone stone;

    [HideInInspector]
    public bool stageEnd;//20208080 sanghun. added because of bear move
    public bool isForestTen = false;  //검은숲 10단계에서 플레이어의 방향키를 반대로 하기위한 변수 -> forest 10 스크립트에서 받아옴

    public bool watchingRight;  //상자이동방향 맞추기위해서
    public bool isButtonDown;   //2020 0809 푸시 여기서 해결할래요
    bool isPushingBox;          //상자를 미는중인지 알아야 호출을 하빈다
    GameObject pushingBoxObj;     //내가 밀고있는 상자

    void Start()
    {
        GameSave(); // 스테이지 데이터 저장
        stageEnd = false;
        //GetComponent로 초기화.
        mRB = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
        mCollider = GetComponent<BoxCollider2D>();
        preTouchPos = Vector2.zero;
        deltaTouchPos = Vector2.zero;
        centerOfScreen = Screen.currentResolution.width / 2f;
        watchingRight = true;
        isPushingBox = false;
        pushingBoxObj = null;
        //find center x on scren

        // stage clear
        parents = GameObject.Find("Parents").GetComponent<ParentsCtrl>();

        // Kids StoneEvent
      //  stone = GameObject.Find("Stone").GetComponent<Stone>();
    } 

    //검은숲 10이 실행됐을 때 true 받아옴
    public void forestTen(bool isTen)
    {
        isForestTen = isTen;
    }

    void Update()
    {
 
        if (parents.stageClear == false)    // stage 진행중
        {

            horizontal = Input.GetAxis("Horizontal");
            float vertical;
            //입력받는부분

            //drag coding
            // 1. you got touch overthan 1
            // 2. check touchPosition at beginning of touch
            // 3. if you move your finger, then touchphase == moved
            // 4. check deltaposition with preTouchPos. and it goes on horizontal
            if (Input.touchCount != 0)
            {                
                //i want to use touch = null; but i cant....
                //so you start with getTouch(0) not to make nullError
                Touch touch = Input.GetTouch(0);
                for (int i = 0; i < Input.touchCount; i++)
                {
                    touch = Input.GetTouch(i);
                    //check if it's in left half of the screen
                    if (touch.position.x < centerOfScreen)
                    {
                        break;
                    }
                }
                //check if it's in left half of the screen
                //we do it twice because it could be only one touch
                //if there is only one touch in right half screen,
                //for loop will not do the work effectively
                if (touch.position.x < centerOfScreen)
                {
                    //if the touch is left half screen
                    if (touch.phase == TouchPhase.Began)
                    {
                        //save beginning position of the touch
                        preTouchPos = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Stationary
                        || touch.phase == TouchPhase.Moved)
                    {
                        //if touch moves, then character moves
                        deltaTouchPos = touch.position - preTouchPos;
                        horizontal = deltaTouchPos.x;
                        vertical = deltaTouchPos.y;

                        //검은숲 10단계가 아니면 제대로 동작
                        if (!isForestTen)
                        {
                            if (horizontal < -1)
                            {
                                horizontal = -1;
                            }
                            else if (horizontal > 1)
                            {
                                horizontal = 1;
                            }
                        }
                        else  //검은숲 10단계라면 방향을 거꾸로
                        {
                            if (horizontal < -1)
                            {
                                horizontal = 1;
                            }
                            else if (horizontal > 1)
                            {
                                horizontal = -1;
                            }
                        }

                        //아래로 드래그 했을 때 IsSit을 true -> oncollision에서 체크
                        if (vertical < -1)
                        {
                            Sit(true);
                        }
                        else if (vertical > 1)
                        {
                            Sit(false);
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        horizontal = 0;
                    }
                }

            }
            //20200808 상훈 추가
            //움직일 때 앉아있는 상태라면 앉은상태 해제를 위해 추가.
            if (horizontal == 1 || horizontal == -1)
            {
                Sit(false);
            }


            if (!IsSit)
            {
               
                //검은숲10이 아니라면
                if (!isForestTen)
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
                                mAnim.SetBool(AnimHash.RUN, true);
                            }
                            else
                            {
                                //이거는 그냥 미는거겠죠 미는애니메이션이 나오면 여기에다가 추가합시다
                                //그리고 보는방향이 왼쪽에서 왼쪽으로 동일하니까, 방향 굳이 바꿔줄 필요 없죠?
                                //그러니까 pushingBox일때는 우리 아무것도 건들지 맙시다
                                mAnim.SetBool(AnimHash.RUN, true);
                            }
                        }
                        else
                        {
                            //이거를 else로 놓아주는 이유는 보는방향이 바뀌면 안되니까
                            watchingRight = false;
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            mAnim.SetBool(AnimHash.RUN, true);
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
                                mAnim.SetBool(AnimHash.RUN, true);
                            }
                            else
                            {
                                //당기는거는 방향을 바꿔줄 필요가 없습니다. 애니메이션이 나오면 여기에다가 추가합시다
                                //그러니까 pushingBox일때는 우리 아무것도 건들지 맙시다
                                mAnim.SetBool(AnimHash.RUN, true);
                            }
                        }
                        else
                        {
                            //이거를 else로 놓아주는 이유는 보는방향이 바뀌면 안되니까
                            watchingRight = true;
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            mAnim.SetBool(AnimHash.RUN, true);
                        }


                    }
                    else
                    {
                        mAnim.SetBool(AnimHash.RUN, false);
                    }
                }
                else  //forest10 이라면 반대로
                {
                    mRB.velocity = new Vector2(horizontal * -mSpeed, mRB.velocity.y);

                    //걷는 애니메이션 및 걷는 방향으로 보는 것 구현
                    if (horizontal > 0)
                    {
                        //박스와 상호작용 하고있는 상태라면
                        if (isPushingBox)
                        {
                            if (watchingRight)
                            {
                                mAnim.SetBool(AnimHash.RUN, true);
                            }
                            else
                            {
                                mAnim.SetBool(AnimHash.RUN, true);
                            }
                        }
                        else
                        {
                            watchingRight = false;
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            mAnim.SetBool(AnimHash.RUN, true);
                        }
                    }
                    else if (horizontal < 0)
                    {
                        if (isPushingBox)
                        {
                            if (watchingRight)
                            {
                                mAnim.SetBool(AnimHash.RUN, true);
                            }
                            else
                            {
                                mAnim.SetBool(AnimHash.RUN, true);
                            }
                        }
                        else
                        {
                            //이거를 else로 놓아주는 이유는 보는방향이 바뀌면 안되니까
                            watchingRight = true;
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            mAnim.SetBool(AnimHash.RUN, true);
                        }
                    }
                    else
                    {
                        mAnim.SetBool(AnimHash.RUN, false);
                    }
                }
            }
                       

            if (Input.GetKeyDown(KeyCode.Z) && IsInteracObj)   //drag로 구현시 IsSit && IsInteractObj
            {
                Sit(true);
            }
            else if (Input.GetKeyUp(KeyCode.Z))
            {

                Sit(false);
            }

            //20200808 sanghun added for debug. cant press alphabet
            if (Input.GetKeyDown(KeyCode.DownArrow) && IsInteracObj)   //drag로 구현시 IsSit && IsInteractObj
            {
                Sit(true);
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                Sit(false);
            }

            //기어코 하기싫었던 update에 SetFloat넣기를 했습니다..
            //저는 업데이트에 입력 이외에걸 넣는걸 싫어하지만, 저희게임에서는 그닥 문제는 없을것 같습니다
            mAnim.SetFloat(AnimHash.JUMP, mRB.velocity.y);
            if (mRB.velocity.y <= 1f && mRB.velocity.y >= -1f)
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

            //미는중에 버튼을 떼면 parent를 뗀다
            if (isPushingBox && !isButtonDown)
            {
                isPushingBox = false;
                pushingBoxObj.transform.SetParent(null);
                pushingBoxObj = null;
            }
        }
        else    // 클리어 했을 때   
        {
            stageEnd = true;
            // 이동 멈춤
            mAnim.SetBool(AnimHash.RUN, false);
            mAnim.SetFloat(AnimHash.JUMP, 0);
            mAnim.SetBool(AnimHash.IDLE, true); // Jump ani 상태로 쫓는 것 방지
            mRB.velocity = new Vector2(0, mRB.velocity.y);

            if (parents.lefttime < 1)   // 부모 오브젝트가 움직인 시간(1초) 후
            {
                // 도망가는 방향과 똑같이 바라본 후 이동
                transform.rotation = Quaternion.Euler(0, 0, 0);
                mAnim.SetBool(AnimHash.RUN, true);
                mRB.velocity = new Vector2(mSpeed, mRB.velocity.y);

                if (parents.lefttime <= 0)  // 부모 쫓아간 후
                {
                    mRB.constraints = RigidbodyConstraints2D.FreezePosition;
                    mAnim.SetBool(AnimHash.RUN, false); // 이동 멈춤

                    int StageLevel = SceneManager.GetActiveScene().buildIndex;
                    PlayerPrefs.SetInt("ClearStage", StageLevel);   // main-stages 저장
                    if (StageLevel != 27)    // City 5 = 26, Ending = 27
                    {
                        StageLevel++;
                        SceneManager.LoadScene(StageLevel); // 다음 씬으로 이동
                    }
                }
            }

        }
    }

    //j버튼 누르면 점프가 발동되게 할 것이다.
    public void Jump()
    {
        //점프가 아닐 때만 위로 힘을 준다!
        //2020 08 08 changed ==0 to <=1 >=-1
        if (isPushingBox)
        {
            return;
        }
        if (mAnim.GetFloat(AnimHash.JUMP) >= -1
            && mAnim.GetFloat(AnimHash.JUMP) <=1)
        {
            mRB.AddForce(mJumpVector, ForceMode2D.Impulse);
        }
    }

    // 데이터 저장
    public void GameSave()
    {
        int CurrentStage = PlayerPrefs.GetInt("CurrentStage");  // 최근 scene
        int StageLevel = SceneManager.GetActiveScene().buildIndex;  // 현재 scene의 번호 저장
        if (StageLevel >= CurrentStage) // 제일 높은 스테이지 번호 저장
            PlayerPrefs.SetInt("CurrentStage", StageLevel);
    }

    //20200808 상훈
    //forset1에서 흔들었을 때 콜을 해줘야돼서 그렇다.
    public void Sit(bool isSitting)
    {
        IsSit = isSitting;
        mAnim.SetBool(AnimHash.SIT, isSitting);
    }

    //20200808 상훈
    //곰, 아이들과같은 요소가 등장하면서 fail이 생긴다
    //그 페일 떄 사용할 함수. 원하는대로 수정하세요
    //현재는 액티브된 씬만 리로드하도록 설정하였습니다.
    public void OnStageFail()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("InteractObj"))
        {
            IsInteracObj = true;
            if (isButtonDown && !isPushingBox)
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

    
    //차일드로 해보니 exit이 발동을 죽어도 안해서 update에서 해제를 해줘야됩니다.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("InteractObj"))
        {
            IsInteracObj = false;
        }
    }
}
