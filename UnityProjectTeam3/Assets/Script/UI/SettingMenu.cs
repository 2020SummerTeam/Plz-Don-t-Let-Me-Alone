using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingMenu : MonoBehaviour
{
    // GameObject SettingMenu와 같이 사용
    // 비활성화 시켜놨기 때문에 settingmenu-getchild로 초기화
    public Transform menu;  // 직접 설정    // SettingMenu

    // sound Btn
    Transform soundBtn;
    GameObject soundOn;

    // night mode Btn
    Transform nightmodeBtn;
    GameObject nightmodeOn;

    public GameObject nightModeObject;

    //public SettingsData Data;   // scene 바뀌어도 data 저장 // asset-creat-settingsdata와 연결
    //이거하니까 씬사이 건너갈때 오류생겨서 playerPrefs로 바꿈ㅠㅠ

    void Awake()
    {
        // init
        soundBtn = menu.GetChild(3).GetComponent<RectTransform>();
        soundOn = menu.GetChild(2).gameObject;

        nightmodeBtn = menu.GetChild(6).GetComponent<RectTransform>();
        nightmodeOn = menu.GetChild(5).gameObject;

        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        if (!PlayerPrefs.HasKey("NightMode"))
        {
            PlayerPrefs.SetInt("NightMode", 1);
        }

        if (PlayerPrefs.GetInt("NightMode") == 1)
        {
            nightModeObject.SetActive(true);
        }

    }

    void Update()
    {
        if (menu.gameObject.activeSelf)
        {
            if (PlayerPrefs.GetInt("Sound")==1) // on 상태
            {
                soundBtn.localPosition = new Vector3(344.5f, soundBtn.localPosition.y, 0);
                soundOn.SetActive(true);
            }
            else    // off 상태
            {
                soundBtn.localPosition = new Vector3(440f, soundBtn.localPosition.y, 0);
                soundOn.SetActive(false);
            }

            if (PlayerPrefs.GetInt("NightMode") == 1)
            {
                nightmodeBtn.localPosition = new Vector2(344.5f, nightmodeBtn.localPosition.y);
                nightmodeOn.SetActive(true);
            }
            else
            {
                nightmodeBtn.localPosition = new Vector2(440f, nightmodeBtn.localPosition.y);
                nightmodeOn.SetActive(false);
            }
        }
    }


    // button event
    // move btn, green&red light on/off
    public void OnClickSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 1)    // on -> off
        {
            PlayerPrefs.SetInt("Sound",0);
        }
        else  // off -> on
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        BGMBtn();
    }

    public void OnClickNightMode()
    {
        if (PlayerPrefs.GetInt("NightMode") == 1)    // on -> off
        {
            nightModeObject.SetActive(false);
            PlayerPrefs.SetInt("NightMode", 0);
        }
        else  // off -> on
        {
            nightModeObject.SetActive(true);
            PlayerPrefs.SetInt("NightMode", 1);
        }
    }

    public void BGMBtn()
    {
        // BGM Mute On/Off

        GameObject bgmManager = GameObject.Find("BGMManager");
        if (!bgmManager)
            Debug.Log("bgmManager is null!");

        AudioSource bgmPlayer = bgmManager.GetComponent<AudioSource>();
        if (bgmPlayer.mute)
        {
            bgmPlayer.mute = false;  // 음소거 해제
        }
        else
        {
            bgmPlayer.mute = true; // 음소거
        }
    }
}
