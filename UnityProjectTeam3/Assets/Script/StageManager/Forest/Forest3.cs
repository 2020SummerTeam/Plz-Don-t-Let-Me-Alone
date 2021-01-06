using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest3 : MonoBehaviour
{
    public SettingMenu settings;
    public AudioSource source;
    // Start is called before the first frame update


    void Start()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            //소리 꺼져있으면 켜주기
            settings.OnClickSound();
        }
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
