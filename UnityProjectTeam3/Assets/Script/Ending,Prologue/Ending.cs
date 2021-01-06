using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public Image fadeImage;
    public SpriteRenderer dogImage;
    public GameObject cam;
    public GameObject[] animations;
    public GameObject[] backGrounds;
    public BGMManager bgmManager;
    public AudioSource stepSource;
    public Text text;
    public string[] stringArr;
    // Start is called before the first frame update
    void Start()
    {
        bgmManager = BGMManager.instance;
        StartCoroutine(EndingCorouitne());
    }

    IEnumerator EndingCorouitne()
    {
        //bgmManager.bgmPlayer.volume = 0;
        float timer = 0;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (timer < 3f)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, 1- timer / 3f);
            yield return null;
        }
        timer = 0;
        while (timer < 6f)
        {
            timer += Time.deltaTime;
            cam.transform.position = Vector3.Lerp(new Vector3(-0.7f, -0.4f, -10), new Vector3(0.7f, 0.4f, -10), timer / 6f);
            fadeImage.color = new Color(0, 0, 0,  timer / 6f);
            yield return null;
        }
        dogImage.gameObject.SetActive(false);
        cam.transform.position = new Vector3(0, 0, -10);
        timer = 0;
        while (timer < 3f)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, 1 - timer / 3f);
            yield return null;
        }
        stepSource.Play();
        for (int i = 0; i < 6; i++)
        {
            timer = 0;
            text.text = stringArr[i];
            if (i != 1)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            }
            
            while (timer < 1f)
            {
                timer += Time.deltaTime;
                animations[i].transform.position = animations[i].transform.position - new Vector3(Time.deltaTime * 13, 0, 0);
                if (i != 1)
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, timer);
                }
                
                BackGroundMove();
                yield return null;
            }
            timer = 0;
            while(timer<6f)
            {
                timer += Time.deltaTime;
                BackGroundMove();
                yield return null;
            }
            timer = 0;
            while (timer < 1f)
            {
                if (i != 0)
                {
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - timer);
                }
                
                timer += Time.deltaTime;
                animations[i].transform.position = animations[i].transform.position - new Vector3(Time.deltaTime * 13, 0, 0);
                BackGroundMove();
                yield return null;
            }
            if (i != 0)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b,0);
            }
        }
        while (timer < 3f)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, timer / 3f);
            yield return null;
        }
        //bgmManager.bgmPlayer.volume = 1;
        stepSource.Stop();
        SceneManager.LoadScene(0);
    }

    void BackGroundMove()
    {
        for(int i = 0; i < 4; i++)
        {
            backGrounds[i].transform.position = backGrounds[i].transform.position + new Vector3(Time.deltaTime * (i+1), 0, 0);
            if(backGrounds[i].transform.position.x > -1)
            {
                backGrounds[i].transform.position = backGrounds[i].transform.position + new Vector3(-19, 0, 0);
            }
        }
    }
}
