using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City1 : MonoBehaviour
{
    public findEvent findEvent;    // player가 findEvent 박스 콜라이더 안에 있는지 전달받기 위해 사용
    public Researchers Researchers;

    public GameObject movingPlatform;

    // 연구원이 버튼이 눌리면 멈추도록 함
    public GameObject researchers;
    public Transform rTr;

    // open door
    public GameObject smallBox;
    public GameObject button;
    public ButtonEvent buttonEvent;
    public GameObject door;
    private bool isOpen;
    public float max_y = 5f;   // 문이 닿을 수 있는 최대 y좌표
    private float door_hh;   // door half height

    void Start()
    {
        Researchers = GameObject.Find("Researchers").GetComponent<Researchers>(); // 연구원이 플레이어 발견
        findEvent = GameObject.Find("findEvent").GetComponent<findEvent>();

        rTr = researchers.GetComponent<Transform>();
        buttonEvent = button.GetComponent<ButtonEvent>();
        isOpen = false;
        door_hh = door.GetComponent<BoxCollider2D>().bounds.size.y / 2;
    }


    void Update()
    {
        // Researchers
        if (findEvent.CanFind == true)  // player가 findEvent 박스 콜라이더 안에 있을 때
        {
            Researchers.isFind = true; // player를 발견했을때 // Researchers 스크립트의 변수 수정
        }
        if (rTr.position.x >= -3.555)
        {
            movingPlatform.GetComponent<MovingPlatform_CIty1>().enabled = true; //player를 발견하면 플랫폼 올라감
        }

        //door
        if (isOpen)  // open
        {
            if (door.transform.position.y + door_hh < max_y)
                door.transform.position += new Vector3(0, Time.deltaTime, 0);

        }

        if (buttonEvent.isBoxTriggerd)
        {
            researchers.transform.position += new Vector3(0, 0, 0);
            isOpen = true;
        }
      
    }

}


