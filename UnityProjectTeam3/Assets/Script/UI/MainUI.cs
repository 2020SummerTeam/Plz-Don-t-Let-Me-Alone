using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public GameObject Ending;
    public GameObject Start;
    int CurrentStage;
    void Awake()
    {
        if (Start.activeSelf)   // Start Btn이 활성화 상태인 경우 (처음 시작)
        {
            // Init
            PlayerPrefs.SetInt("CurrentStage", 0);  // Main = 0, Stages(menu) = 1  // scene "stage 1" = 2, ...
            PlayerPrefs.SetInt("ClearStage", 0);   // 26일 경우 엔딩
        }
        else
        {
            GameLoad();
        }
    }

    // 데이터 불러오기
    public void GameLoad()
    {
        CurrentStage = PlayerPrefs.GetInt("stageLevel");    // 제일 높은 스테이지 번호
        int ClearStage = PlayerPrefs.GetInt("ClearStage");    // 마지막 스테이지 클리어

        if (ClearStage == 26)   // 클리어했을 경우 Ending Btn 활성화
        {
            Ending.SetActive(true);
        }

    }


    // button click event

    public void OnClickEnding()
    {
        // Ending
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(2);
    }

    public void OnClickContinue()
    {
        // current stage
        SceneManager.LoadScene(CurrentStage);
    }

    public void OnClickStages()
    {
        SceneManager.LoadScene(1);  // go to scene "stages"
    }

}
