using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City2 : MonoBehaviour
{
    [SerializeField]
    private GameObject MovePlatform;

    [Header("Buttons")]
    public Button mB;
    public Button mN;
    public Button[] mO;
    public Button mU;
    public Button mT;
    public Button mX;

    [Header("ButtonVariable")]
    private bool isB;
    private bool isN;
    private bool isO;
    private bool isU;
    private bool isT;
    private bool isX;

    private int mCount; //t should click twice

    public GameObject mBox;
    public GameObject mButton;
    public GameObject mBlind;
    public GameObject mPlatform;
    public GameObject mPanel;
    public GameObject mPlayer;

    public ButtonEvent mButtonEvent;
    public StoneEvent mStone;
    public PlayerCtrl mPlayerCtrl;
   
    
    [SerializeField]
    float speed = 1.0f;

    //platform 1 이동위치
    private Vector3 pos1 = new Vector3(26f, -1.2f, 0f);
    private Vector3 pos2 = new Vector3(26f, 2.5f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform.transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);

        if (isB)
        {
            if (isO)
            {
                if (isX)
                {
                    mBox.SetActive(true);
                }
            }
        }

        if (isB)
        {
            if (isU)
            {
                if (isT)
                {
                    if (isO && mCount ==2)
                    {
                        if (isN)
                        {
                            mButton.SetActive(true);
                        }
                    }
                }
            }
        }

        if (mButtonEvent.buttonTriggerd)
        {
            mButton.SetActive(false);
            mBlind.SetActive(false);
            mPlatform.SetActive(true);
        }

        if (mStone.isStoneEvent)
        {
            mPlayerCtrl.OnStageFail();
        }

        if(mPlayer.transform.position.x > 20f)
        {
            mPanel.SetActive(true);
        }
        else
        {
            mPanel.SetActive(false);
        }

    }

    public void ClickB()
    {
        isB = true;
    }

    public void ClickN()
    {
        isN = true;
    }

    public void ClickO()
    {
        isO = true;
    }

    public void ClickX()
    {
        isX = true;
    }

    public void ClickU()
    {
        isU = true;
    }

    public void ClickT()
    {
        isT = true;
        mCount++;
    }


}
