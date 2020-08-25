using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City2 : MonoBehaviour
{
    [SerializeField]
    private GameObject MovePlatform;

    [SerializeField]
    float speed = 1.0f;

    //platform 1 이동위치
    private Vector3 pos1 = new Vector3(26f, -1f, 0f);
    private Vector3 pos2 = new Vector3(26f, 2.5f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform.transform.position = Vector3.Lerp(pos1, pos2, (Mathf.Sin(speed * Time.time) + 1.0f) / 2.0f);
    }
}
