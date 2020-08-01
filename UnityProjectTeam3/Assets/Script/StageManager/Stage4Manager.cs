using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class Stage4Manager : MonoBehaviour
{
    public GameObject player;
    public ButtonEvent button;
    int buttonCounter;
    //get player's position, button, and button triggeredCounter
    //when player pushes button 1, buttonCounter++. and button gones
    //and player goes other position, which not triggers button.
    //then we spawn button again

    public GameObject arrowObj;
    //spawn arrowObject if you clicked button for 3times
    //and if you push arrow, then you setactive false the big cube. the obstacle.
    public GameObject obstacle;

    public GameObject talkBallon;
    //mal poong sun

    bool pauseClicked;
    //is pasueButton clicked? on right above;



    // Start is called before the first frame update
    void Start()
    {
        //initialazition. cho gi hwa
        buttonCounter = 0;
        pauseClicked = false;
        arrowObj.SetActive(false);
        talkBallon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //on pushing button
        if (button.buttonTriggerd)
        {
            if(buttonCounter <= 2)
            {
                if(buttonCounter == 0)
                {
                    //set gravity scale
                    obstacle.GetComponent<Rigidbody2D>().gravityScale = 100;
                }
                //on first to third time pushing button
                buttonCounter++;
                button.buttonTriggerd = false;
                StartCoroutine(SpawnButton());
            }
            else
            {
                //we dont spawn anymore
                buttonCounter++;
                button.buttonTriggerd = false;
                StartCoroutine(ArrowMove());
            }
        }

    }

    IEnumerator SpawnButton()
    {
        //spawn button after waiting for 2 secodns
        yield return new WaitForSeconds(2f);
        while(button.transform.position.x - player.transform.position.x < 1.5f)
        {
            //wait for distance
            yield return new WaitForSeconds(0.1f);
        }
        button.gameObject.SetActive(true);
    }

    //this coroutine is for moving arrow. on right above
    IEnumerator ArrowMove()
    {
        arrowObj.SetActive(true);
        talkBallon.SetActive(true);
        //true talkballon and arrow obj

        RectTransform rect = arrowObj.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;
        float start = 300;
        float end = 200;
        bool goingUp = false;//if arrow is going up, or down
        pauseClicked = false;
        pos.y = start;
        while (!pauseClicked)
        {
            /*
             * 이거는 설명하기가 어려워서 한글로 적습니다
             * pos는 y값을 바꾸며 위아래로 움직일건데, 위와 아래에 한계치가 각각 있고
            그게 start와 end인 것입니다.
            위로 갈수도있고 아래로갈수도있으니까 각 한계를 찍어주면
            방향을 틀어주어야겠지요? 
            그게 goingUp의 역할입니다
             */
            if (goingUp)
            {
                pos.y += 10f;
                if (pos.y > start)
                {
                    goingUp = false;
                }

            }
            else
            {
                pos.y -= 10f;
                if (pos.y < end)
                {
                    goingUp = true;
                }

            }
            //give changed position
            rect.anchoredPosition = pos;
            yield return new WaitForSeconds(0.1f);
        }
        //on pauseButtno pushed.
        arrowObj.SetActive(false);
        talkBallon.SetActive(false);
        obstacle.SetActive(false);
    }

    public void PauseButton()
    {
        Debug.Log("ok");
        pauseClicked = true;
    }
}
