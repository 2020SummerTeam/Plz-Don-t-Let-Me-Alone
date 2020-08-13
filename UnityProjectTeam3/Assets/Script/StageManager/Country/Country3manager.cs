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
    public GameObject Player;

    // Button & Puzzle
    public ButtonEvent buttonEvent;
    public Rigidbody2D FallingTree;
    private float t = 1.0f;    // 큰 상자 내려오는 속도
    public GameObject Button;
    private float buttonHalfSize;
    private float rockHalfSize;
    private bool isCapOpen; // 차고 문이 완전히 열렸는지
    public GameObject roof; // 차고 뚜껑
    private float roofHeight;   // 올라오는 차고 문과 높이 비교
    private float playerHead;
    void Start()
    {
        // 초기화
        KidsRenderer = Kids.GetComponent<SpriteRenderer>();
        buttonHalfSize = Button.GetComponent<Renderer>().bounds.size.x / 2;
        rockHalfSize = Rock.GetComponent<Renderer>().bounds.size.x / 2;
        roofHeight = Cap.transform.position.y + Cap.GetComponent<Renderer>().bounds.size.y;
        isCapOpen = false;
    }


    void Update()
    {
        // Kids' StoneEvent
        if (stoneEvent.isStoneEvent == true)  // player가 StoneZone에 있을 때
        {
            if (KidsRenderer.flipX == true)    // kids가 Player를 쳐다볼 때
            {
                if (isCapOpen == true)    // 차고 뚜껑이 다 열렸을 때
                {
                    if (Player.transform.position.x > Rock.transform.position.x) // player가 rock보다 앞에 있거나
                    {
                        stone.isThrow = true; // Stonezone 스크립트의 변수 수정
                    }
                    else
                    {
                        if (Player.GetComponent<PlayerCtrl>().IsSit == false)   // rock 뒤에서 숨기 X 일때
                        {
                            stone.isThrow = true;
                        }
                    }
                }
            }
        }

        // Button & Puzzle
        if ((Rock.transform.position.x+rockHalfSize) >= (Button.transform.position.x - buttonHalfSize)) 
        {
            // Rock의 오른쪽과 Button의 왼쪽이 닿자마자 버튼 닿았다고 인식 
            buttonEvent.buttonTriggerd = true;

        }

        if (buttonEvent.buttonTriggerd) // 버튼이 활성화되면
        {
            if (Cap.transform.position.y < roofHeight)  // Cap(차고 뚜껑)이 지붕의 y좌표까지
            {
                Cap.transform.position += new Vector3(0, Time.deltaTime, 0) / 2;    // Cap이 느리게 올라감
            }
            else
            {
                isCapOpen = true;
            }

            if (t > 0)  // 큰 상자 하강
            {
                FallingTree.transform.position += new Vector3(0, -2 * Time.deltaTime, 0);
                t -= Time.deltaTime;
            }
        }
    }
}
