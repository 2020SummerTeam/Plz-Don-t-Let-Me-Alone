using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest14 : MonoBehaviour
{

    public GameObject lamp;
    public GameObject lamplightNshadow;
    public SettingMenu settings;

    void Start()
    {
        if (PlayerPrefs.GetInt("NightMode")==1)
        {
            settings.OnClickNightMode();
        }
    }

    public void OnNightMode()
    {
        lamplightNshadow.SetActive(!lamplightNshadow.activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) // 설정-야간모드 활성화 미구현
        {
            lamplightNshadow.SetActive(false);
        }

    }
}
