using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country3manager : MonoBehaviour
{

    // Kids
    public StoneEvent stoneEvent;    // player가 StoneZone에 있는지 전달받기 위해 사용
    public Stone stone;  // 조건 만족 후 돌을 던짐
    public SpriteRenderer KidsRenderer; // Kids가 쳐다보는 방향을 알려줌
    public GameObject Rock; // 가림막이 될 돌
    public GameObject Cap;  // 차고 뚜껑
    public GameObject Kids;


    // Button & Puzzle
    public ButtonEvent buttonEvent;
    public Rigidbody2D FallingTree;
    void Start()
    {
        // 초기화
        KidsRenderer = Kids.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        // Kids
        if (stoneEvent.isStoneEvent == true)  // player가 StoneZone에 있을 때
        {
            if (KidsRenderer.flipX == true)    // kids가 Player를 쳐다볼 때
            {
                if (Cap.activeSelf == false)    // 차고 뚜껑이 없을때
                {
                    if (Kids.transform.position.x > Rock.transform.position.x)  // Rock이 player를 가려주지 않을 때
                    {
                        stone.isThrow = true; // Stonezone 스크립트의 변수 수정
                    }
                }
            }
        }


        // Button & Puzzle
        if (buttonEvent.buttonTriggerd) // 높이가 높아서 Rock으로 버튼 안눌러짐
        {
            Cap.SetActive(false);
            FallingTree.gravityScale = 1;
        }
    }
}
