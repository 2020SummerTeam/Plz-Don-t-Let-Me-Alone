using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Forest1 : MonoBehaviour
{
    public PlayerCtrl playerScript;  //script
    public GameObject bearObject;    //bearObject.
    BearScript bearScript;           //check bear's colision bool
    public GameObject smallBox;     //set false if bear colliisons
    Vector2 movePos;                //bear's moving position
    bool isShaked;                  //폰을 흔들었는지 아닌지

    // Start is called before the first frame update
    void Start()
    {
        isShaked = false;
        movePos = bearObject.transform.position;
        bearScript = bearObject.GetComponent<BearScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //check bear
        if (bearScript.isSmallBoxCol)
        {
            smallBox.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //for debug
            OnShake();
        }

        //bear move
        if (!playerScript.stageEnd)
        {
            //스테이지가 끝나지 않았따면.
            movePos.x -= Time.deltaTime;
             bearObject.transform.position = movePos;
            if (bearScript.isPlayerCol)
            {
                //부딪혔을 때 흔들지 않았따면 끝
                if (!isShaked)
                {
                    Debug.Log("end");
                    playerScript.OnStageFail();
                }
            }
        }
    }

    //아직은 암것두없지만 일단 만들어놓는 함수.
    void OnShake()
    {
        //흔들어서 앉았
        isShaked = true;
        playerScript.Sit(true);

        //기절중에 움직였는지 판단.
        StartCoroutine(ShakeCoroutine());
    }

    //흔들고나서 움직임이 있으면 isshaked를 폴스로 만들어줘야되니까.
    IEnumerator ShakeCoroutine()
    {
        //쉐이크로 기절한 직후부터 베어가 사라질때까지 대기
        //중간에 한번이라도 앉는게 풀리면 끝
        while (playerScript.IsSit == true && bearObject.transform.position.x >-12)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (playerScript.IsSit)
        {
            //안움직이고 잘 기다려서 나온경우
            //앉는걸 풀어준다
            playerScript.Sit(false);
        }
        else
        {
            //앉다가 움직여서 나온 경우
            //끗!
            playerScript.OnStageFail();
        }
    }


}
