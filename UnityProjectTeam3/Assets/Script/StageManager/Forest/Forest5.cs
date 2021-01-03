using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest5 : MonoBehaviour
{
    private bool IsRotate;
    public BearScript bearScript;
    public GameObject bearObject;
    public PlayerCtrl player;
    public GameObject smallBox;

    Rigidbody2D playerRigid;
    Rigidbody2D bearRigid;
    Rigidbody2D boxRigid;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;   //디바이스가 세워진 상태에서 홈 버튼이 오른쪽에 있는 가로 모드입니다.
        bearScript.MoveAnimation();
        bearRigid = bearObject.GetComponent<Rigidbody2D>();
        boxRigid = smallBox.GetComponent<Rigidbody2D>();
        playerRigid = player.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsRotate)
            bearObject.transform.position = bearObject.transform.position - new Vector3(Time.deltaTime, 0, 0);
        if (bearScript.isPlayerCol)
        {
            player.OnStageFail();
        }
        if (bearScript.isSmallBoxCol)
        {
            smallBox.SetActive(false);
        }
        isRotate();
        if (IsRotate || Input.GetKey(KeyCode.T))
        {
            if (player.transform.childCount ==0)
            {
                playerRigid.gravityScale = -1;
            }
            else
            {
                Transform child = player.transform.GetChild(0);
                if(child.name == "SmallBox")
                    playerRigid.gravityScale = -1;
            }
            
            bearRigid.gravityScale = -1;
            boxRigid.gravityScale = -1;
            
        }
        else
        {
            bearRigid.gravityScale = 0;
            boxRigid.gravityScale = 0;
            playerRigid.gravityScale = 0;
        }

        if (player.transform.position.y > 4)
        {
            player.OnStageFail();
        }
    }

    


    public void isRotate()
    {
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight) //device의 방향이 반대로 되었다면
        {
            IsRotate = true;
        }
        else
        {
            IsRotate = false;
        }
    }


}