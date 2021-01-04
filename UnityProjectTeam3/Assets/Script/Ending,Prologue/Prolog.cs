using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prolog : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public BGMManager bgmManager;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        bgmManager = BGMManager.instance;
        bgmManager.bgmPlayer.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 24)
        {
            bgmManager.bgmPlayer.volume = 1;
            SceneManager.LoadScene(2);
        }
    }

    public void CarDown()
    {
        audioSource.clip = audioClip[0];
        audioSource.Play();
    }

    public void CarStop()
    {
        audioSource.clip = audioClip[1];
        audioSource.Play();
    }

    public void SandyDown()
    {
        audioSource.clip = audioClip[2];
        audioSource.Play();
    }

    public void CarClose()
    {
        audioSource.clip = audioClip[3];
        audioSource.Play();
    }

    public void FatherTalk()
    {
        audioSource.clip = audioClip[4];
        audioSource.Play();
    }

    public void SandyTalk()
    {
        audioSource.clip = audioClip[5];
        audioSource.Play();
    }

    public void CarEngine()
    {
        audioSource.clip = audioClip[6];
        audioSource.Play();
    }

    public void SandySigh()
    {
        audioSource.clip = audioClip[7];
        audioSource.Play();
    }
}
