using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraCtrl : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z);

        if (transform.position.x < 0f)
        {
           transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 20f)
        {
            transform.position = new Vector3(20f, transform.position.y, transform.position.z);
        }
    }
}
