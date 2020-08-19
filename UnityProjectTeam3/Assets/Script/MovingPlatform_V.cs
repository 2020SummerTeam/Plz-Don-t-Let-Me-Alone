using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_V : MonoBehaviour
{
    // vertical // 상하로 이동하는 platform

    Transform mTr;
    private float min_y_scale;
    private float max_y_scale;
    private float direction = -1;

    // 스테이지별로 적당히 설정
    public float distance = 1.5f;
    public float speed = 1;

    void Start()
    {
        mTr = GetComponent<Transform>();
        max_y_scale = mTr.position.y + distance;
        min_y_scale = mTr.position.y - distance;
    }

    void Update()
    {
        mTr.position += new Vector3(0, Time.deltaTime * direction * speed, 0);
        if (mTr.position.y >= max_y_scale)
        {
            direction *= -1;
            mTr.position = new Vector3(mTr.position.x, max_y_scale, mTr.position.z);
        }
        else if (mTr.position.y <= min_y_scale)
        {
            direction *= -1;
            mTr.position = new Vector3(mTr.position.x, min_y_scale, mTr.position.z);
        }

    }

}
