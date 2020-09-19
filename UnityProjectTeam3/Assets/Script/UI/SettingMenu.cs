using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class SettingMenu : MonoBehaviour
{

    // sound Btn
    public Transform soundBtn;
    public GameObject soundOff;
    public GameObject soundOn;

    // night mode Btn
    public Transform nightmodeBtn;
    public GameObject nightmodeOff;
    public GameObject nightmodeOn;

    void Start()
    {
        // init
        soundBtn = GameObject.Find("soundBtn").GetComponent<Transform>();
        soundOff = GameObject.Find("soundOff");
        soundOn = GameObject.Find("soundOn");

        nightmodeBtn = GameObject.Find("nightmodeBtn").GetComponent<Transform>();
        nightmodeOff = GameObject.Find("nightmodeOff");
        nightmodeOn = GameObject.Find("nightmodeOn");

    }

    void Update()
    {
        
    }


    // button event
    // move btn, green&red light on/off
    public void OnClickSound()
    {
        if (soundOff.activeSelf)    // on
        {
            soundBtn.localPosition = new Vector3(344.5f, soundBtn.localPosition.y, 0);
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else    // off
        {
            soundBtn.localPosition = new Vector3(440f, soundBtn.localPosition.y, 0);
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }

    public void OnClickNightMode()
    {
        if (nightmodeOff.activeSelf)    // on
        {
            nightmodeBtn.localPosition = new Vector2(344.5f, nightmodeBtn.localPosition.y);
            nightmodeOn.SetActive(true);
            nightmodeOff.SetActive(false);
        }
        else    // off
        {
            nightmodeBtn.localPosition = new Vector2(440f, nightmodeBtn.localPosition.y);
            nightmodeOn.SetActive(false);
            nightmodeOff.SetActive(true);
        }
    }

}
