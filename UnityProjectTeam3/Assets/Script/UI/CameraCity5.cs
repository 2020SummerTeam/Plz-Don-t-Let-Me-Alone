using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCity5 : MonoBehaviour
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
        transform.position = new Vector3(playerPos.x, playerPos.y + 2f, transform.position.z);

        if (transform.position.x < 0f)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 41f)
        {
            transform.position = new Vector3(41f, transform.position.y, transform.position.z);
        }
    }
}
