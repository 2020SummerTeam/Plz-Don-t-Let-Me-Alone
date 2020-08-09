using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField]
    GameObject mPlayer;
    //2020 0809 playerpush에서 ctrl로 바꿉니다
    PlayerCtrl script;

    void Start()
    {
        mPlayer = GameObject.FindWithTag("Player");

        //change false if there is no interactable thing
        if (mPlayer == null)
        {
            //false button
            gameObject.SetActive(false);
            return;
        }
        //2020 0809 playerpush에서 ctrl로 바꿉니다
        script = mPlayer.GetComponent<PlayerCtrl>();


    }

    void Update()
    {

    }

    public void PointerDown()
    {
        script.isButtonDown = true;
        Debug.Log("IsButtonDown");
    }

    public void PointerUp()
    {
        script.isButtonDown = false;
        Debug.Log("IsButtonUp");
    }

}
