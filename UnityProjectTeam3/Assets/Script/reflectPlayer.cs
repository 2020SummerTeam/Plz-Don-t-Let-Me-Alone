using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reflectPlayer : MonoBehaviour
{
    // 플레이어가 부딪혔을 때, 반사각을 구해 플레이어를 반대편으로 튕겨냄

    public Rigidbody2D player;

    private Vector2 reflectPos;
    private bool isTrigger = false;

    public int direction; // y축을 뒤집기 위해 movingPlatform일 경우 -1(아래로), button일 경우 1(위로)으로 설정
    public Vector2 innormal;
    

    void FixedUpdate()
    {
        if (isTrigger) // 연달아 실행하지 않도록
        {
            player.AddForce(reflectPos);
            isTrigger = false;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            innormal = new Vector2(collision.contacts[0].point.x, collision.contacts[0].point.y * direction); // player와 부딪힌 곳의 좌표
            reflectPos = Vector2.Reflect(collision.transform.position, innormal); // 반사각을 구하는 함수
            isTrigger = true;
        }
    }
}
