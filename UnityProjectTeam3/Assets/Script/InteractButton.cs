using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField]
    GameObject mPlayer;
    PlayerPush script;
    
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
        script = mPlayer.GetComponent<PlayerPush>();
        
      
    }

    void Update()
    {
      
    }

    public void PointerDown()
    {
        script.AButtonDown(true);
        Debug.Log("IsButtonDown");
    }

    public void PointerUp()
    {
        script.AButtonDown(false);
        Debug.Log("IsButtonUp");
    }

}
