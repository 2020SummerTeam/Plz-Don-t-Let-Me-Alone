using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stages : MonoBehaviour
{
    int CurrentStage;
    int ClearStage;

    public GameObject[] ParentsStep = new GameObject [25];
    public GameObject[] SandyStep = new GameObject [25];

    void Awake()
    {
        // init
        GameLoad();
        Show();
    }

    void Start()
    {
        // plus
        GameLoad(); // 스테이지 번호와 클리어한 스테이지 번호 불러옴
        Show();
        Debug.Log(CurrentStage);
    }

    public void Show()
    {
        if (CurrentStage >= 2)
        {
            CurrentStage -= 2;
            for (int p = 0; p <= CurrentStage; p++)
            {
                ParentsStep[p].gameObject.SetActive(true);
            }
        }
        if (ClearStage >= 2)
        {
            ClearStage -= 2;
            for (int s = 0; s <= ClearStage; s++)
            {
                SandyStep[s].gameObject.SetActive(true);
            }
        }
    }
    
    
    // 데이터 불러오기
    public void GameLoad()
    {
        CurrentStage = PlayerPrefs.GetInt("stageLevel");    // 제일 높은 스테이지 번호
        ClearStage = PlayerPrefs.GetInt("ClearStage");    // 마지막 스테이지 클리어
    }


    // button
    public void OnClickBack()
    {
        SceneManager.LoadScene(0);
    }
}
