using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBtn : MonoBehaviour
{

    // button event

    public void OnClickResume()
    {
        gameObject.SetActive(false);
    }

    public void OnClickReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMain()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickStage()
    {
        SceneManager.LoadScene(1);
    }

}
