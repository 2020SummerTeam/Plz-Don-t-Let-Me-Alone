using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//it is used to trigger button. in every stage
//there are public bool triggerd; so other script can use this
//if it's triggerd, it goes on setactive false;
public class ButtonEvent : MonoBehaviour
{
    public bool buttonTriggerd = false; // 플레이어가 버튼 누를때
    public AudioSource audioSource;
    //is button is pressed

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Button enable");
        audioSource.transform.SetParent(null);
       
    }

    //collision is triggering button,
    //if button is once triggerd, set active false
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            audioSource.Play();
            this.gameObject.SetActive(false);
            buttonTriggerd = true;
        }
    }
}
