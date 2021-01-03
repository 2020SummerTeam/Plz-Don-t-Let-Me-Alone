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
    public GameObject backGround;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndingCorouitne());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EndingCorouitne()
    {
        float timer = 0;
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
        for(int i = 0; i < 5; i++)
        {
            timer = 0;

            while(timer < 1f)
            {
                timer += Time.deltaTime;
                animations[i].transform.position = animations[i].transform.position - new Vector3(Time.deltaTime * 13, 0, 0);
                backGround.transform.position = backGround.transform.position - new Vector3(Time.deltaTime * 3, 0, 0);
                yield return null;
            }
            timer = 0;
            while(timer<2f)
            {
                timer += Time.deltaTime;
                backGround.transform.position = backGround.transform.position - new Vector3(Time.deltaTime * 3, 0, 0);
                yield return null;
            }
            timer = 0;
            while (timer < 1f)
            {
                timer += Time.deltaTime;
                animations[i].transform.position = animations[i].transform.position - new Vector3(Time.deltaTime * 13, 0, 0);
                backGround.transform.position = backGround.transform.position - new Vector3(Time.deltaTime * 3, 0, 0);
                yield return null;
            }
        }
        while (timer < 3f)
        {
            timer += Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, timer / 3f);
            yield return null;
        }
        SceneManager.LoadScene(0);


    }
}
