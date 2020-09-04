using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest14 : MonoBehaviour
{

    public GameObject lamp;
    public GameObject lamplightNshadow;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // 설정-야간모드 활성화 미구현
        {
            lamplightNshadow.SetActive(false);
        }

    }
}
