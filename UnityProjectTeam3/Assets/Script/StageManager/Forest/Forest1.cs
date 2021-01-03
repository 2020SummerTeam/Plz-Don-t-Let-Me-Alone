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

    float accelerometerUpdateInterval = 1.0f / 60.0f;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the
    // filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation,
    // or at least according to Brady! ;)
    float shakeDetectionThreshold = 2.0f;

    float lowPassFilterFactor;
    Vector3 lowPassValue;


    // Start is called before the first frame update
    void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
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

        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
        {
            // Perform your "shaking actions" here. If necessary, add suitable
            // guards in the if check above to avoid redundant handling during
            // the same shake (e.g. a minimum refractory period).
            Debug.Log("Shake event detected at time " + Time.time);
            OnShake();
        }

        //bear move
        if (!playerScript.stageEnd)
        {
            //스테이지가 끝나지 않았따면.
            movePos.x -= Time.deltaTime;
             bearObject.transform.position = movePos;
            bearScript.MoveAnimation();
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
        while (playerScript.IsSit == true && bearObject.transform.position.x >-10)
        {
            if (!playerScript.IsSit)
            {
                playerScript.OnStageFail();
            }
            yield return new WaitForSeconds(0.1f);
        }
        if (playerScript.IsSit)
        {
            //안움직이고 잘 기다려서 나온경우
            //앉는걸 풀어준다
            playerScript.Sit(false);
        }
    }


}
