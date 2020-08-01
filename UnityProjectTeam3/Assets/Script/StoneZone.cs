using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneZone : MonoBehaviour
{
    public bool isStoneZone;  // player가 돌을 맞을 영역에 있는지
    public GameObject player;

    void Start()
    {
        isStoneZone = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision) // player가 StoneZone에 들어갈 경우
    {
        if (collision.gameObject == player)
            isStoneZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)  // player가 StoneZone에서 빠져나갔을 경우
    {
        if (collision.gameObject == player)
            isStoneZone = false;
    }

}
