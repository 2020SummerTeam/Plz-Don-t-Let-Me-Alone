using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findEvent : MonoBehaviour
{
    // stage별로 박스 콜라이더 조절해서 사용
    /* stageManager에서 조건 추가적으로 설정해서 사용하시면 됩니다 (여기서 x)

    public findEvent findEvent;    // player가 findEvent 박스 콜라이더 안에 있는지 전달받기 위해 사용
    public Researchers Researchers;

    void Start()
    {
        Researchers = GameObject.Find("Researchers").GetComponent<Researchers>();
        findEvent = GameObject.Find("findEvent").GetComponent<findEvent>();
    }


    void Update()
    {
        // Researchers
        if (findEvent.Canfind == true)  // player가 findEvent 박스 콜라이더 안에 있을 때
        {
                // * 조건 if로 추가

                Researchers.isFind = true; // player를 발견했을때 // Researchers 스크립트의 변수 수정
        }
    }

     */
    public bool CanFind;

    void Start()
    {
        CanFind = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            CanFind = true;  // player가 해당 박스 콜라이더 안에 있을 때
        }
    }
}
