using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forest4 : MonoBehaviour
{
    public ButtonEvent buttonEvent;
    public BearScript bearScript;
    public PlayerCtrl playerScript;
    public GameObject beeObject;
    List<GameObject> beeObjectList;//벌은 많이생기니까 리스ㅡㅌ로 만들었다
    public Text bearTextBallon;
    public GameObject platformObject;
    public GameObject smallBox;         //작은박스 위치도 알아야된다

    bool isPlayerGoRight;       //플레이어가 충분히 오른쪽으로 진행하였는지
    bool isBoxDunked;           //박스가 바닥에 빠졌는지
    bool isBeeSpawned;
    bool isBearDead;

    //스테이지 4의 주 재료들
    //
    private void Start()
    {
        isPlayerGoRight = false;
        isBoxDunked = false;
        isBeeSpawned = false;
        isBearDead = false;

        beeObjectList = new List<GameObject>();
        beeObject.SetActive(false);
        beeObjectList.Add(beeObject);

        for (int i =0; i < 10; i++)
        {
            beeObjectList.Add(Instantiate(beeObject));
            //벌들을 만들고 리스트ㅔㅇ 넣어주었따
        }
        bearTextBallon.text = "";     //텍스트 ! 넣어줘야된다
        StartCoroutine(BoolCheckCor());
    }
    private void Update()
    {

        //버튼 눌리는거 확인해주능거
        if (buttonEvent.buttonTriggerd)
        {
            OnButtonTrigger();
            buttonEvent.buttonTriggerd = true;
        }
        
    }

    //플레이어가 오른쪽으로 갔는지, 박스가 바닥에 빠졌는지 체크해주는겨
    IEnumerator BoolCheckCor()
    {
        //여기서는 샌디가 혼자 나무까지 가는지,
        //박스랑 같이 가는지,
        //가가지고 벌들을 깨울건지
        //아니면 곰을 먼저 깨울건지 결정해야한다.
        bool boxTree = false;
        bool playerTree = false;
        while (true)
        {
            //상자가 나무주변에 도달하면은.
            if (!boxTree && smallBox.transform.position.x > -0.5f)
            {
                boxTree = true;
                StartCoroutine(BearMoveCor());
            }

            if(!playerTree && playerScript.transform.position.x > -0.5f)
            {
                playerTree = true;
                if (!isBeeSpawned)
                {
                    StartCoroutine(SpawnBeeCor());
                }
                
            }
            

            //박스가 빠졌는지두 해준다
            if (smallBox.transform.position.y < -2.8f)
            {
                isBoxDunked = true;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
        
    }

    //베어가 움직인다
    IEnumerator BearMoveCor()
    {
        Vector2 pos = bearScript.transform.position;
        //bearTextBallon.SetActive(false);
        bearTextBallon.text = "!!!!";
        while (!bearScript.isBeeCol)    //벌의 공격을 받지않을때만 움직여요
        {
            //곰이 움직움직여요
            pos.x -= Time.deltaTime;
            if(pos.x < 3)
            {
                //3아래로 내려가면 벌이나와요
                if (!isBeeSpawned)
                {
                    isBeeSpawned = true;
                    StartCoroutine(SpawnBeeCor());
                }
            }
            bearScript.transform.position = pos;
            if (bearScript.isPlayerCol)
            {
                //곰이 부딪히면 끝~
                playerScript.OnStageFail();
            }
            yield return null;
        }
        //벌에 부딪히면 열루 내려와요
        bearTextBallon.text = "죽은곰";
        isBearDead = true;
    }

    //벌을 낳아요
    IEnumerator SpawnBeeCor()
    {
        int beeCount = 0;
        float beeTimer = 0;
        while (!isBearDead) //곰죽으면 끗
        {
            //어디를 타겟으로 할지 
            Vector3 target;

            

            if (isBoxDunked)
            {
                //박스가 아래로 빠졌다면
                target = playerScript.transform.position;
            }
            else
            {
                if (playerScript.IsSit)
                {
                    //박스가 빠지지않았는데 잘 숨었어
                    target = bearScript.transform.position;
                }
                else
                {
                    //박스가 빠지지않았고 잘 못숨었으면
                    target = playerScript.transform.position;
                }
            }
            //하나씩 하나씩 차례대로 해준다
            beeTimer += Time.deltaTime;
            if(beeTimer > 0.1f)
            {
                if (beeCount < beeObjectList.Count)
                {
                    beeObjectList[beeCount].SetActive(true);
                    //0.2초마다 벌을 하나씩 깨워준다
                    beeCount++;
                }
                beeTimer = 0;
            }


            //실제로 벌들이 이동하는 함수
            foreach (GameObject obj in beeObjectList)
            {
                //액티브해야만 한다.
                if (obj.activeSelf)
                {
                    //이동속도랑 방향이 꿈틀거리게 한다. 꿀벌이니까
                    target.x *= Random.Range(0f, 10f);
                    target.y *= Random.Range(0f, 10f);
                    obj.transform.position = Vector2.MoveTowards(obj.transform.position, target, Time.deltaTime * Random.Range(5, 10));

                    //벌들하고 거리재는 방법
                    Vector2 mag = obj.transform.position - playerScript.transform.position;
                    if (mag.sqrMagnitude < 0.3f)
                    {
                        playerScript.OnStageFail();
                    }
                }

            }
            yield return null;
        }
        //곰이 죽으면 열루 내려오니까.
        foreach (GameObject obj in beeObjectList)
        {
            obj.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
    }


    public void OnButtonTrigger()
    {
        StartCoroutine(PlatformMoveCor());
    }

    IEnumerator PlatformMoveCor()
    {
        //플랫폼이 움직이는 코루틴이다
        Vector3 pos = platformObject.transform.position;
        while(pos.y <= -5 && !isBoxDunked)
        {
            pos.y += Time.deltaTime;
            platformObject.transform.position = pos;
            yield return null;
            //또루틴
        }
        if (!isBoxDunked)
        {
            pos.y = -5;
            platformObject.transform.position = pos;
        }
        //마지막에 이걸 해줘야된다
    }




}
