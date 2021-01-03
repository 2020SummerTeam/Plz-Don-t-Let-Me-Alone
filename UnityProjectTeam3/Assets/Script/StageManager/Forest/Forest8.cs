using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest8 : MonoBehaviour
{
    // Kids
    public StoneEvent stoneEvent;    // player가 StoneZone에 있는지 전달받기 위해 사용
    public StoneEvent stoneEvent2;
    public Stone stone;  // 조건 만족 후 투사체 던지기 위해 변수 전달
    public GameObject Kids;
    public GameObject Player;
    public KidsCtrl kidsCtrl;
    public BearScript bearScript;
    public PlayerCtrl playerCtrl;
    bool coroutineRunning;
    public Forest8Tree tree;
    public GameObject buttonObject;


    // open door
    public GameObject smallBox;
    private float boxHalfSize;
    private BoxCollider2D mCol;
    private float buttonHalfSize;
    public GameObject door;
    private BoxCollider2D doorCol;
    public bool isOpen;
    private float door_hh;   // door half height
    bool isPlayerOnButton;

    void Start()
    {
        isOpen = false;
        mCol = GetComponent<BoxCollider2D>();
        coroutineRunning = false;
        doorCol = door.GetComponent<BoxCollider2D>();
        door_hh = doorCol.bounds.size.y / 2;
        boxHalfSize = smallBox.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        buttonHalfSize = GetComponent<BoxCollider2D>().bounds.size.x / 2;
        isPlayerOnButton = false;
        bearScript.SleepAnimation();
        bearScript.attackPlayer = true;

    }

    // Update is called once per frame
    void Update()
    {

        // Kids
        if (stoneEvent.isStoneEvent == true)  // player가 StoneZone에 있을 때
        {
            if (kidsCtrl.watchingLeft) // Kids가 왼쪽 볼 때
            {
                stone.isThrow = true; // Stone 스크립트의 변수 수정
                stone.stageFail = true;
                Player.GetComponent<PlayerCtrl>().enabled = false;
            }

        }
        if (stoneEvent2.isStoneEvent == true)  // player가 StoneZone2에 있을 때
        {
            if (!kidsCtrl.watchingLeft)   // Kids가 오른쪽 볼 때
            {
                stone.isThrow = true;
                stone.stageFail = false;
            }
                
        }

        if (bearScript.isPlayerCol && !coroutineRunning && !tree.HitTree)
        {
            coroutineRunning = true;
            playerCtrl.enabled = false;
            StartCoroutine(BearWait1Seconds());
        }

        if(tree.HitTree == true)
        {
            bearScript.attackPlayer = false;
            bearScript.MoveAnimation();
        }

        // open door
        if(isOpen)  // open
        {
            if (doorCol.bounds.center.y + door_hh < 5.72f)
                door.transform.position += new Vector3(0, Time.deltaTime, 0);

        }
        else    // close
        {
            if (doorCol.bounds.center.y - door_hh > -1.195f)
                door.transform.position -= new Vector3(0, Time.deltaTime, 0);
        }

        if (((smallBox.transform.position.x - boxHalfSize) <= (transform.position.x + buttonHalfSize))
            && (smallBox.transform.position.x + boxHalfSize) >= (transform.position.x - buttonHalfSize))
        {
            // smallBox의 왼쪽이 button의 오른쪽에 닿았을 때부터 인식 시작
            mCol.enabled = false;   // 박스 콜라이더 비활성화. 막혀서 box 못 움직이는 것 방지
            isOpen = true;
            buttonObject.SetActive(false);
        }
        else
        {
            mCol.enabled = true;
            if (!isPlayerOnButton)
            {
                isOpen = false;
                buttonObject.SetActive(true);
            }
                
        }

    }

    IEnumerator BearWait1Seconds()
    {
        //애니메이션 보여줄려고 그렇다
        yield return new WaitForSeconds(1f);
        playerCtrl.OnStageFail();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("뭐냐");
            isPlayerOnButton = true;
            isOpen = true;
            buttonObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOpen == true && collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnButton = false;
            isOpen = false; // 열림 멈춤
            buttonObject.SetActive(true);
        }
    }


}
