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


    // open door
    public GameObject smallBox;
    private float boxHalfSize;
    private BoxCollider2D mCol;
    private float buttonHalfSize;
    public GameObject door;
    private BoxCollider2D doorCol;
    public bool isOpen;
    private float door_hh;   // door half height

    void Start()
    {
        isOpen = false;
        mCol = GetComponent<BoxCollider2D>();

        doorCol = door.GetComponent<BoxCollider2D>();
        door_hh = doorCol.bounds.size.y / 2;
        boxHalfSize = smallBox.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        buttonHalfSize = GetComponent<BoxCollider2D>().bounds.size.x / 2;

    }

    // Update is called once per frame
    void Update()
    {

        // Kids
        if (stoneEvent.isStoneEvent == true)  // player가 StoneZone에 있을 때
        {
            if (Kids.transform.rotation == Quaternion.Euler(0, 180, 0)) // Kids가 왼쪽 볼 때
                stone.isThrow = true; // Stone 스크립트의 변수 수정
        }
        if (stoneEvent2.isStoneEvent == true)  // player가 StoneZone2에 있을 때
        {
            if (Kids.transform.rotation == Quaternion.Euler(0, 0, 0))   // Kids가 오른쪽 볼 때
                stone.isThrow = true;
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
        }
        else
        {
            mCol.enabled = true;
            isOpen = false;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isOpen == true)
        {
            isOpen = false; // 열림 멈춤
        }
    }
}
