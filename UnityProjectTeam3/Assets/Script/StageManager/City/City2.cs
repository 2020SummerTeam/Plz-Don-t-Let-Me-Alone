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

   /* [Header("ButtonVariable")]
    private bool isB;
    private bool isN;
    private bool isO;
    private bool isU;
    private bool isT;
    private bool isX;*/

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
    public KidsCtrl kidsCtrl;
    public Stone stone;

    public bool[] buttonIndex;
    public bool[] boxIndex;
   
    
    [SerializeField]
    float speed = 1.0f;

    //platform 1 이동위치
    private Vector3 pos1 = new Vector3(26f, -1.2f, 0f);
    private Vector3 pos2 = new Vector3(26f, 2.5f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        boxIndex = new bool[3];
        for (int i =0; i<3; i++)
        {
            boxIndex[i] = false;
        }
        buttonIndex = new bool[6];
        for (int i = 0; i < 6; i++)
        {
            buttonIndex[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform.transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
        bool trueable = true;
        for (int i = 0; i < 3; i++)
        {
            if (!boxIndex[i])
            {
                trueable = false;
            }
        }
        if (trueable)
        {
            mBox.SetActive(true);
        }
        trueable = true;
        for (int i = 0; i < 6; i++)
        {
            if (!buttonIndex[i])
            {
                trueable = false;
            }
        }
        if (trueable)
        {
            mButton.SetActive(true);

        }


        if (mButtonEvent.buttonTriggerd)
        {
            mButton.SetActive(false);
            mBlind.SetActive(false);
            mPlatform.SetActive(true);
        }

        if (mStone.isStoneEvent)
        {
            if (kidsCtrl.watchingLeft)
            {
                if (!mPlayerCtrl.IsSit)
                {
                    stone.isThrow = true;
                    kidsCtrl.coolTime = 10;
                }
                
            }
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

        boxIndex[0] = true;
        buttonIndex[0] = true;
    }

    public void ClickN()
    {
        if (buttonIndex[4])
        {
            buttonIndex[5] = true;
        }
        else
        {
            ButtonIndexReset();
        }
    }

    public void ClickO()
    {
        if (boxIndex[1])
        {
            BoxIndexReset();
        }
        else
        {
            if (boxIndex[0] == true)
            {
                boxIndex[1] = true;
            }
            else
            {
                BoxIndexReset();
            }
        }


        if (buttonIndex[4])
        {
            ButtonIndexReset();
        }
        else
        {
            if (buttonIndex[3])
            {
                buttonIndex[4] = true;
            }
            else
            {
                ButtonIndexReset();
            }
        }
       

    }

    public void ClickX()
    {
        if (boxIndex[1] == true)
        {
            boxIndex[2] = true;
        }
        else
        {
            BoxIndexReset();
        }

    }

    public void ClickU()
    {
        if (buttonIndex[1])
        {
            ButtonIndexReset();

        }
        else
        {
            if (buttonIndex[0])
            {
                buttonIndex[1] = true;
            }
            else
            {
                ButtonIndexReset();
            }
        }
       

    }

    public void ClickT()
    {
        if (buttonIndex[3])
        {
            ButtonIndexReset();
        }
        else
        {
            if (buttonIndex[2])
            {
                buttonIndex[3] = true;
            }
            if (buttonIndex[1])
            {
                buttonIndex[2] = true;
            }
            else
            {
                ButtonIndexReset();
            }
        }
       
    }

    public void ButtonIndexReset()
    {
        for(int i = 0; i < 6;i++)
        {
            buttonIndex[i] = false;
        }
    }

    public void BoxIndexReset()
    {
        for (int i = 0; i < 3; i++)
        {
            boxIndex[i] = false;
        }

    }


}
