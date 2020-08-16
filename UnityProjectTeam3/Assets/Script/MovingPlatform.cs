using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //  좌우로 이동하는 platform

    Transform mTr;
    public float min_x_scale;
    public float max_x_scale;
    public float direction = -1;

    public GameObject player;   // 같이 이동


    void Start()
    {
        mTr = GetComponent<Transform>();
        max_x_scale = mTr.position.x + 1.5f;
        min_x_scale = mTr.position.x - 1.5f;
    }

    void Update()
    {
        mTr.position += new Vector3(Time.deltaTime * direction, 0, 0);
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
