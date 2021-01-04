using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflectPlayer : MonoBehaviour
{
    // 플레이어가 부딪혔을 때 다시 점프하게 만듦
    // 플랫폼에 적용

    // 플레이어 컨트롤
    public GameObject player;
    private Rigidbody2D pRB;
    public Vector2 pJumpVector = new Vector2(5, 35);
    ParentsCtrl parents;
    public int count;
    public float time;
    public bool isJump;
    public AudioSource audio;
    void Start()
    {
        pRB = player.GetComponent<Rigidbody2D>();
        parents = GameObject.Find("Parents").GetComponent<ParentsCtrl>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !parents.stageClear)
        {
            audio.Play();
            pRB.velocity = Vector2.zero;
            pRB.AddForce(pJumpVector, ForceMode2D.Impulse); // 점프
            isJump = false;
        }
    }

}
