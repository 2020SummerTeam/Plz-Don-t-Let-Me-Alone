using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest8Tree : MonoBehaviour
{
    public GameObject stone;
    public GameObject bee;
    public GameObject Pos;
    private Vector3 InitPos;

    public bool HitTree;
    void Start()
    {
        HitTree = false;
        InitPos = stone.transform.position;

    }

    void Update()
    {
        if (HitTree)
        {
            bee.transform.position = Vector3.Lerp(bee.transform.position, Pos.transform.position, Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == stone)
        {
            stone.transform.position = InitPos - new Vector3(0, 0, 20);    // 돌이 플레이어에게 닿기 전에 위치 초기화
            HitTree = true;
        }
    }
}
