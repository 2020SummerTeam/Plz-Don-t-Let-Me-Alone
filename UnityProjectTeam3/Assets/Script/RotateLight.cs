using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateLight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isButtonDown;
    public GameObject mlight;

    private int num;

    void Start()
    {
        num = 0;
    }
    private void Update()
    {
        if(num < 11)
        {
            if (isButtonDown)
            {
                rotate();
                num++;
            }
        }

    }

    public void rotate()
    {   
        mlight.transform.Rotate(new Vector3(0, 0, -13));
    }

   
    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonDown = false;
    }
}
