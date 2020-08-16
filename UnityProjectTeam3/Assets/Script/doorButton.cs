using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorButton : MonoBehaviour
{
    // 해당 버튼을 player가 누르면 문이 열립니다
    public GameObject ResearcherDoor;
    public bool isOpenDoor;

    void Start()
    {
        isOpenDoor = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResearcherDoor.SetActive(false);
            isOpenDoor = true;
        }
    }
}
