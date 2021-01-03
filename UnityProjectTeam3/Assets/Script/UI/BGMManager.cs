using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    [SerializeField] AudioClip[] bgmClip; // 브금 목록 배열   // mp3 파일
    public AudioSource bgmPlayer; // 브금 플레이어

    public static BGMManager instance = null;
    void Awake()
    {
        if (instance != this && instance != null)
            Destroy(this.gameObject);   // DontDestroyOnLoad 중복 방지
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        PlayBGM();   
    }

    public void PlayBGM()
    {
        int sceneNum = SceneManager.GetActiveScene().buildIndex;

        // scene 별로 배경음악 바꿈
        if (sceneNum == 0 || sceneNum == 1) // BGM
        {
            bgmPlayer.clip = bgmClip[0];
        }
        else if (sceneNum >= 2 && sceneNum <= 6) // Country
        {
            bgmPlayer.clip = bgmClip[1];
        }
        else if (sceneNum >= 7 && sceneNum <= 21)   // Forest
        {
            bgmPlayer.clip = bgmClip[2];
        }
        else if (sceneNum >= 22 && sceneNum <= 26)  // City
        {
            bgmPlayer.clip = bgmClip[3];
        }

        if (bgmPlayer.isPlaying)
        {
            return; // 재생되고 있으면 return
        }
        else if(bgmPlayer)
        {
            bgmPlayer.Play();
            DontDestroyOnLoad(this);
        }
    }

}
