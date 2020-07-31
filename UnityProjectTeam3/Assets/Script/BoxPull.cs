using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPull : MonoBehaviour
{
    public bool beingPushed;
    float xPos;
    // Start is called before the first frame update
    void Start()
    {
        xPos = transform.position.x;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beingPushed == false)
            transform.position = new Vector3(xPos, transform.position.y);
        else
            xPos = transform.position.x;
    }
}
