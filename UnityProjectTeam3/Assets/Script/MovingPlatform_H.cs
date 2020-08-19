using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_H : MonoBehaviour
{
    // horizontal // 좌우로 이동하는 platform

    Transform mTr;
    private float min_x_scale;
    private float max_x_scale;
    private float direction = -1;

    // 스테이지별로 적당히 설정
    public float distance = 1.5f;
    public float speed = 1;
    

    void Start()
    {
        mTr = GetComponent<Transform>();
        max_x_scale = mTr.position.x + distance;
        min_x_scale = mTr.position.x - distance;
    }

    void Update()
    {
        mTr.position += new Vector3(Time.deltaTime * direction * speed, 0, 0);
        if (mTr.position.x >= max_x_scale)
        {
            direction *= -1;
            mTr.position = new Vector3(max_x_scale, mTr.position.y, mTr.position.z);
        }
        else if (mTr.position.x <= min_x_scale)
        {
            direction *= -1;
            mTr.position = new Vector3(min_x_scale, mTr.position.y, mTr.position.z);
        }

    }

}
