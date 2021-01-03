using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City5 : MonoBehaviour
{
    public GameObject mCar;
    public GameObject mPlayer;
    public GameObject target;   // 터치한 타겟
    
    public GameObject glass;                    //유리를 찾아야대서
    public GameObject[] shopArray;              //유리를 꺠야대서
    bool isGlassCrashed;
    int glassCount = 0;
    float accelerometerUpdateInterval = 1.0f / 60.0f;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the
    // filtered value will converge towards current input sample (and vice versa).
    float lowPassKernelWidthInSeconds = 1.0f;
    // This next parameter is initialized to 2.0 per Apple's recommendation,
    // or at least according to Brady! ;)
    float shakeDetectionThreshold = 2.0f;

    float lowPassFilterFactor;
    Vector3 lowPassValue;
    int shakeNumber;
    public GameObject[] shakingObject;


    public SpriteRenderer[] pillarsSprite;      //색을 바꾸려는 기둥    
    public BoxCollider2D[] pillarsCollider;     //trigger에서 변하려는 기둥.
    public SettingMenu settingMenu;             //나이트모드인지 알아야돼서
    bool isNightMode;

    public GameObject escalatorCollider;        //없애줘야하는 애스컬레이터 콜라이더
    public GameObject actionedLever;
    public GameObject unActionedLever;
    bool isActioned;

    public Researchers researchers;
    public PlayerCtrl playerCtrl;
    bool isEnd;

    public Image endSprite;



    // Start is called before the first frame update
    void Start()
    {
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;//다 쉐이크를 위한거/
        isEnd = false;

        shakeNumber = 0;
        isNightMode = false;    //처음에 끄고 시작함.
        isActioned = false;
        isGlassCrashed = false;
        if (PlayerPrefs.GetInt("NightMode")==1)
        {
            settingMenu.OnClickNightMode();
            
        }
        researchers.EachNum = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (mPlayer.transform.position.x > 42.5f)
        {
            if (!isEnd)
            {
                StartCoroutine(EndCoroutine());
            }
            isEnd = true;
            playerCtrl.Die();
            if (mCar.transform.position.x > 42.5f){
                mCar.transform.Translate(-2f, 0f, 0f);
            }
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

        if (Input.GetKeyDown(KeyCode.Y))
        {
            OnShake();
        }

        if(mPlayer.transform.position.x > 16 && shakeNumber>=5 && !researchers.isFind && researchers.LorR == -1)
        {
            researchers.isFind = true;
            researchers.dieDistance = 1.5f;
            for(int i = 0; i < 4; i++)
            {
                shakingObject[i].GetComponent<BoxCollider2D>().enabled = false;
            }
            playerCtrl.enabled = false;
        }
        else if(mPlayer.transform.position.x < 16 || shakeNumber < 5)
        {
            researchers.isFind = false;
        }

    }

    public void OnShake()
    {
        if (!isGlassCrashed || shakeNumber>=5)
        {
            return;
        }
        shakingObject[shakeNumber].transform.localPosition = shakingObject[shakeNumber].transform.localPosition - new Vector3(0, 2, 0);
        shakeNumber++;
       

    }

    public void OnLever()
    {
        Debug.Log("되냐??");
        if(mPlayer.transform.position.x <6f && mPlayer.transform.position.x > -8f)
        {
            if (mPlayer.transform.position.y < 4 && mPlayer.transform.position.y > 3f)
            {
                if (isActioned)
                {

                    escalatorCollider.SetActive(true);
                    unActionedLever.SetActive(true);
                    actionedLever.SetActive(false);
                    isActioned = false;
                }
                else
                {
                    escalatorCollider.SetActive(false);
                    unActionedLever.SetActive(false);
                    actionedLever.SetActive(true);
                    isActioned = true;
                }
            }
        }
    }

    public void OnNightMode()
    {
        if (isNightMode)
        {
            for (int i = 0; i < 4; i++)
            {
                pillarsCollider[i].isTrigger = true;
                pillarsSprite[i].color = Color.white;
            }
            //나이트모드에서 오프할떄
        }
        else
        {
            //나이트모드를 켤 때
            for (int i = 0; i < 4; i++)
            {
                pillarsCollider[i].isTrigger = false;
                pillarsSprite[i].color = Color.black;
            }
            mPlayer.transform.position = new Vector3(mPlayer.transform.position.x, 2.74f, mPlayer.transform.position.z);


        }
        isNightMode = !isNightMode; //반대로 바꿔주고
    }

    IEnumerator EndCoroutine()
    {
        float timer = 0;
        endSprite.gameObject.SetActive(true);
        while (timer < 5)
        {
            timer += Time.deltaTime;
            endSprite.color = endSprite.color + new Color(0, 0, 0, Time.deltaTime / 5);
            yield return null;
        }
        playerCtrl.OnEnd();
        
    }


    void OnGUI()
    {

        Event e = Event.current;
        if (e.clickCount == 1)
        {
            CastRay();

            if (target == null)
            {
                return;
            }
            if (target == glass)
            {
                Debug.Log(glassCount);
                glassCount++;
                if(glassCount == 8)
                {
                    shopArray[0].SetActive(false);
                    shopArray[1].SetActive(true);
                }
                if(glassCount == 19)
                {
                    shopArray[1].SetActive(false);
                    shopArray[2].SetActive(true);
                    isGlassCrashed = true;
                }
            }

        }
    }

    void CastRay() // 유닛 히트처리 부분.  레이를 쏴서 처리합니다. 
    {

        target = null;

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);



        if (hit.collider != null)   //히트되었다면 여기서 실행
        {

            target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정

        }
    }
}

