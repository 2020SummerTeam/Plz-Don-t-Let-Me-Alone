using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public GameObject Ending;
    public GameObject Start;
    public GameObject continueButton;
    int CurrentStage;
    void Awake()
    {
            GameLoad();
    }

    // 데이터 불러오기
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("CurrentStage"))
        {
            PlayerPrefs.SetInt("CurrentStage", 2);
        }
        if (!PlayerPrefs.HasKey("ClearStage"))
        {
            PlayerPrefs.SetInt("ClearStage", 1);
        }
        if (!PlayerPrefs.HasKey("Prologue"))
        {
            PlayerPrefs.SetInt("Prologue", 0);
        }
        CurrentStage = PlayerPrefs.GetInt("CurrentStage");    // 제일 높은 스테이지 번호
        int ClearStage = PlayerPrefs.GetInt("ClearStage");    // 마지막 스테이지 클리어
        
        if (PlayerPrefs.GetInt("Prologue") == 1)
        {
            Start.SetActive(false);
            continueButton.SetActive(true);
        }


        if (ClearStage == 26)   // 클리어했을 경우 Ending Btn 활성화
        {
            Ending.SetActive(true);
        }

    }


    // button click event

    public void OnClickEnding()
    {
        SceneManager.LoadScene(27);
    }

    public void OnClickStart()
    {

        //프롤로그 먼저 보여줘
        PlayerPrefs.SetInt("Prologue", 1);
        SceneManager.LoadScene(28);
    }

    public void OnClickContinue()
    {
        // current stage
         CurrentStage = PlayerPrefs.GetInt("CurrentStage");
        SceneManager.LoadScene(CurrentStage);
    }

    public void OnClickStages()
    {
        SceneManager.LoadScene(1);  // go to scene "stages"
    }

}
