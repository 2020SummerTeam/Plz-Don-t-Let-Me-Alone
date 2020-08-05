using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPull : MonoBehaviour
{
    public bool beingPushed;
    public Transform mTr;
    float xPos;
    float yPos;
    float zPos;

    // Start is called before the first frame update
    void Start()
    {
        mTr = GetComponent<Transform>();
        xPos = mTr.position.x;
        yPos = mTr.position.y;
        zPos = mTr.position.z;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beingPushed == false)
            transform.position = new Vector3(xPos, yPos, zPos);
        else
            xPos = transform.position.x;
    }
}
