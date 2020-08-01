using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//it is used to trigger button. in every stage
//there are public bool triggerd; so other script can use this
//if it's triggerd, it goes on setactive false;
public class ButtonEvent : MonoBehaviour
{
    public bool buttonTriggerd;
    //is button is pressed

    // Start is called before the first frame update
    void Start()
    {
        buttonTriggerd = false;
    }

    //collision is triggering button,
    //if button is once triggerd, set active false
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            buttonTriggerd = true;
            gameObject.SetActive(false);

        }
    }
}
