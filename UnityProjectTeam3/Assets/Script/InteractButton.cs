using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    [SerializeField]
    GameObject mInteractObj;
    PlayerPush script;
    
    

    void Start()
    {
        mInteractObj = GameObject.FindWithTag("InteractObj");

        //change false if there is no interactable thing
        if (mInteractObj == null)
        {
            //false button
            gameObject.SetActive(false);
            return;
        }
        script = mInteractObj.GetComponent<PlayerPush>();
        
      
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
