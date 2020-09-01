using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform_CIty1 : MonoBehaviour
{
    Transform platform;
    public float distance = 3f;
    public float speed = 1;
    private float max_y_scale;
    private float direction = 1;  

    void Start()
    {
        platform = GetComponent<Transform>();
        max_y_scale = platform.position.y + distance;
    }

    
    void Update()
    {
        platform.position += new Vector3(0, Time.deltaTime * direction * speed, 0);
        platform.position += new Vector3(0, Time.deltaTime * direction * speed, 0);
        if (platform.position.y >= max_y_scale)
        {
            platform.position = new Vector3(platform.position.x, max_y_scale, platform.position.z);
        }
    }
}
