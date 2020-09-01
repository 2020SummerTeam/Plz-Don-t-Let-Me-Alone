using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//it is used to trigger button. in every stage
//there are public bool triggerd; so other script can use this
//if it's triggerd, it goes on setactive false;
public class ButtonEvent : MonoBehaviour
{
    public bool objects = false; // 박스로 버튼 누를때 체크, 플레이어가 누를 시 체크 x
    public bool isPlayerTriggerd = false; // 플레이어가 버튼 누를때
    public bool isBoxTriggerd = false;
    //is button is pressed

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Button enable");
       
    }

    //collision is triggering button,
    //if button is once triggerd, set active false
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            this.gameObject.SetActive(false);
            isPlayerTriggerd = true;
        }



        if (collision.gameObject.CompareTag("interactObj"))
        {
            Debug.Log("hit smallBOX");
            this.gameObject.SetActive(false);
            isBoxTriggerd = true;
        }
    }
}
