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
    public Transform sTr;
    public GameObject button; //상자와 박스 좌표 동일시 문 열림
    public Transform bTr;
    public GameObject door;
    private float timer = 0.0f;
    public float max_y = 3f;   // 문이 닿을 수 있는 최대 y좌표
    public AudioSource doorAudio;
    bool[] doorBool;
    public AudioSource explanationAudio;
    bool expBool = false;
    public AudioSource buttonAudio;
    bool buttonTriggered = false;

    void Start()
    {
        doorBool = new bool[2];
        doorBool[0] = false;
        doorBool[1] = false;
        Researchers = GameObject.Find("Researchers").GetComponent<Researchers>(); // 연구원이 플레이어 발견
        findEvent = GameObject.Find("findEvent").GetComponent<findEvent>();

        rTr = researchers.GetComponent<Transform>();
        sTr = smallBox.GetComponent<Transform>();
        bTr = button.GetComponent<Transform>();
        Researchers.EachNum = 0;
    }


    void Update()
    {
        // Researchers
        if (findEvent.CanFind == true)  // player가 findEvent 박스 콜라이더 안에 있을 때
        {
            if (!expBool)
            {
                expBool = true;
                explanationAudio.Play();
            }
            Researchers.EachNum = 0;
            Researchers.isFind = true; // player를 발견했을때 // Researchers 스크립트의 변수 수정
        }
        if (rTr.position.x >= -2)
        {
            if (!doorBool[0])
            {
                doorBool[0] = true;
                doorAudio.Play();
            }
            movingPlatform.GetComponent<MovingPlatform_CIty1>().enabled = true; //player를 발견하면 플랫폼 올라감
        }

        if ((sTr.position.x >= (bTr.position.x-1)) || buttonTriggered)
        {
            buttonTriggered = true;
            //researchers.GetComponent<Researchers>().enabled = false;
            button.SetActive(false);

            timer += Time.deltaTime; // 버튼이 사라지고 나서 문이 올라갈 수 있도록 딜레이 줌
            if (timer >= 0.25)
            {
                if (!doorBool[1])
                {
                    doorBool[1] = true;
                    doorAudio.Play();
                    buttonAudio.Play();
                }
                door.transform.position += new Vector3(0, Time.deltaTime, 0);
                if (door.transform.position.y >= max_y)
                {
                    door.transform.position = new Vector3(door.transform.position.x, max_y, door.transform.position.y);
                }
            }
        }
    }

}


