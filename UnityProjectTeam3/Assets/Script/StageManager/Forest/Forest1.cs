using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest1 : MonoBehaviour
{
    public PlayerCtrl playerScript;  //script
    public GameObject bearObject;    //bearObject.
    BearScript bearScript;           //check bear's colision bool
    public GameObject smallBox;     //set false if bear colliisons
    Vector2 movePos;                //bear's moving position

    // Start is called before the first frame update
    void Start()
    {
        movePos = bearObject.transform.position;
        bearScript = bearObject.GetComponent<BearScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //check bear
        if (bearScript.isSmallBoxCol)
        {
            smallBox.SetActive(false);
        }
        if (bearScript.isPlayerCol)
        {

        }

        //bear move
        if (!playerScript.stageEnd)
        {
             movePos.x -= Time.deltaTime;
             bearObject.transform.position = movePos;
        }
        
    }

}
