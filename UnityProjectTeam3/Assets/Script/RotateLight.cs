using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateLight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isButtonDown;
    public GameObject mlight;
    public AudioSource audioSource;
    private int num;

    void Start()
    {
        isButtonDown = false;
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

                if (Time.deltaTime >= 0.00000009)
                    isButtonDown = false;
            }
        }

    }

    public void rotate()
    {
        audioSource.Play();
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
