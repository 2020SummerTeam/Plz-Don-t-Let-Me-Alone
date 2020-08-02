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

    Rigidbody2D rigidBody;  //20200808 sanghun

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        mTr = GetComponent<Transform>();
        xPos = mTr.position.x;
        yPos = mTr.position.y;
        zPos = mTr.position.z;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beingPushed == false)
        {
            rigidBody.mass = 100;
        }
        //transform.position = new Vector3(xPos, yPos, zPos);
        else
        {
            rigidBody.mass = 1;
        }
            //xPos = transform.position.x;
    }
}
