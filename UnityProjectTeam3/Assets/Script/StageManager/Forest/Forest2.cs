using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Forest2 : MonoBehaviour
{
    public ButtonEvent buttonEvent;
    public GameObject parentPlatform;
    public BearScript bearScript;
    public PlayerCtrl playerScript;
    public GameObject bearTextBallon;   //곰이 자고있으면 zzz가 뜨니까 그거 지워주는 역할
    bool isMuted;
    public AudioSource audioSource;
    public AudioClip explanationClip;

    //스테이지 2의 주 재료들

    void Start()
    {
        isMuted = BGMManager.instance.bgmPlayer.mute;
        bearScript.SleepAnimation();
        audioSource.Play();
    }

    private void Update()
    {
        //버튼이 눌렸다면은~
        if (buttonEvent.buttonTriggerd)
        {
            OnButtonEvent();
            buttonEvent.buttonTriggerd = false;
            //귀찮으니까 폴스처리~

        }
    }

    // Update is called once per frame

    public void OnButtonEvent()
    {
        //플랫폼을 없애줘요
        parentPlatform.SetActive(false);
        isMuted = BGMManager.instance.bgmPlayer.mute;
        if (!isMuted)
        {
            //음소거가 안됐을때
            audioSource.Stop();
            audioSource.clip = explanationClip;
            audioSource.loop = false;
            audioSource.Play();
            StartCoroutine(BearMoveCoroutine());
        }
    }

    //일시정지 버튼 누르면 될거
    //진짜게임에서는 뮤트버튼 누르면 바뀌도록 할 예정
    public void OnMuteButton()
    {
        isMuted = !isMuted;
    }

    //뮤트 안하면 얘가 움직인당.
    IEnumerator BearMoveCoroutine()
    {
        Vector2 pos = bearScript.transform.position;
        bearScript.MoveAnimation();
        //bearTextBallon.SetActive(false);
        bearTextBallon.SetActive(true);
        while (true)
        {
            //곰이 움직움직여요
            pos.x -= Time.deltaTime;
            bearScript.transform.position = pos;
            if (bearScript.isPlayerCol)
            {
                //곰이 부딪히면 끝~
                playerScript.OnStageFail();
            }
            yield return null;
        }
    }
}
