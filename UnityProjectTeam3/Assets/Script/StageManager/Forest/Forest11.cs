﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Forest11 : MonoBehaviour
{
    // bear, boxs
    public BearScript bearScript;
    public float bearCooltime = 5.0f;   // 곰이 움직이기 전까지 시간
    public GameObject bear;
    private Animator bearAnim;
    public GameObject smallBox; // 곰이 부딪히면 부숴지는 박스
    public int cnt = 0; // 곰이 박스와 부딪힐 때, smallBox일 경우 cnt = 1, bigBox일 경우 cnt = 2가 됨
    public GameObject bee;
    public float attacktime = 0f;

    // bug
    public GameObject player;
    public GameObject tree;
    public GameObject bug;

    // Researchers
    public findEvent findEvent;    // player가 findEvent 박스 콜라이더 안에 있는지
    public Researchers Researchers;
    public GameObject ResearcherDoor;
    public bool isOpenDoor;

    // ballon ctrl
    public GameObject textBallon;

    void Start()
    {
        bearScript = bear.GetComponent<BearScript>();
        bearAnim = bear.GetComponent<Animator>();
        bear.transform.rotation = Quaternion.Euler(0, 180, 0);  // bear가 왼쪽 바라본 상태로 시작
        Researchers = GameObject.Find("Researchers").GetComponent<Researchers>();
        findEvent = GameObject.Find("findEvent").GetComponent<findEvent>();
        isOpenDoor = false;
    }

    void Update()
    {
        // Bear Control
        if (bearCooltime > 0)   // 곰이 움직이기 전 (5초)
        {
            bearCooltime -= Time.deltaTime;

            // 벌통을 떨어뜨리지 않고, 나무 지나갈 경우 bug 나타남 (game over)
            if (bee.activeSelf == false && player.transform.position.x >= tree.transform.position.x - 1.2f)
            {
                Debug.Log("Game Over");
                player.GetComponent<PlayerCtrl>().enabled = false;
                bug.SetActive(true);

                // bug의 player 추적
                if (bug.transform.position.x < player.transform.position.x - 0.01)   // bug가 player의 왼쪽에 있을 경우
                {
                    bug.transform.rotation = Quaternion.Euler(0, 180, 0);    // 회전
                    bug.transform.position += new Vector3(Time.deltaTime, 0, 0);
                }
                else if (bug.transform.position.x > player.transform.position.x + 0.01)   //  bug가 player의 오른쪽에 있을 경우
                {
                    bug.transform.rotation = Quaternion.Euler(0, 0, 0);
                    bug.transform.position -= new Vector3(Time.deltaTime, 0, 0);
                }
            }

        }
        else
        {
            if (bearScript.isBeeCol)    // 곰이 벌과 부딪혔을 때
            {
                bear.transform.position += new Vector3(0, 0, 0); // 이동 멈춤
                bearAnim.SetBool("Run", false);
            }
            else if (bearScript.isPlayerCol)    // 곰이 플레이어와 부딪혔을 때 (game over)
            {
                bear.transform.position += new Vector3(0, 0, 0); // 이동 멈춤
                bearAnim.SetBool("Run", false);
                Debug.Log("Game Over");
            }
            else if (bearScript.isSmallBoxCol)  // 곰이 박스와 부딪혔을 때
            {
                bear.transform.position += new Vector3(0, 0, 0); // 이동 멈춤
                bearAnim.SetBool("Run", false);

                if (cnt <= 2)
                cnt++;  // smallBox일 경우 cnt = 1, bigBox일 경우 cnt = 2가 됨

                if (cnt == 1)    // 작은 박스와 부딪힐 경우
                {
                    attacktime = 1f;
                    bearScript.isSmallBoxCol = false;
                }
                else if (cnt == 2)  // 큰 박스와 부딪힐 경우
                {
                    bearAnim.SetBool("Die", true);  // 기절
                }
            }
            else // 곰과 부딪힌 물체가 없을 경우 (default)
            {
                bearAnim.SetBool("Run", true);

                if (bee.activeSelf == false)    // bee가 나무에서 떨어지지 않았을 경우
                {
                    if (attacktime > 0)
                    {
                        attacktime -= Time.deltaTime;
                        if (attacktime > 0.5f)
                        {
                            bear.transform.position += new Vector3(0, 0, 0); // 이동 멈춤
                            bearAnim.SetBool("Run", false);

                            bearAnim.SetTrigger("Attack");  // 작은 박스를 부심
                        }
                        else
                        {
                            smallBox.SetActive(false);  //작은 박스 비활성화
                            bearAnim.SetBool("Run", true);
                        }
                    }
                    else
                    {
                        bear.transform.rotation = Quaternion.Euler(0, 0, 0);    // 회전
                        bear.transform.position -= new Vector3(Time.deltaTime, 0, 0);   // 5초의 쿨타임이 지나고 bear 왼쪽으로 이동

                    }
                }
                else
                {
                    if (bear.transform.position.x < bee.transform.position.x - 0.01)   // bear가 bee보다 왼쪽에 있을 경우
                    {
                        bear.transform.rotation = Quaternion.Euler(0, 180, 0);    // 회전
                        bear.transform.position += new Vector3(Time.deltaTime, 0, 0);
                    }
                    else if (bear.transform.position.x > bee.transform.position.x + 0.01)   // bear가 bee보다 오른쪽에 있을 경우
                    {
                        bear.transform.rotation = Quaternion.Euler(0, 0, 0);
                        bear.transform.position -= new Vector3(Time.deltaTime, 0, 0);
                    }
                    else // bear의 무한 회전 방지  // bear와 bee의 x좌표가 일치
                    {
                        bear.transform.position += new Vector3(0, 0, 0);
                        bearAnim.SetBool("Run", false);
                    }
                }
            }
        }

        // Researchers
        if (findEvent.CanFind && isOpenDoor)  // player가 findEvent 박스 콜라이더 안에 있을 때
        {
            Researchers.isFind = true; // Researchers 스크립트의 변수 수정
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ResearcherDoor.SetActive(false);
            textBallon.SetActive(false);
            isOpenDoor = true;
        }
    }

}