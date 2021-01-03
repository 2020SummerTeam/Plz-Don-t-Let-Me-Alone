using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City5Scale : MonoBehaviour
{
    public int teddyNumber;
    public GameObject scale;
    bool changeScale;
    public GameObject nextCollider;
    float timer = 0;

    private void Start()
    {
        changeScale = false;
    }

    private void Update()
    {
        if (changeScale)
        {
            scale.transform.eulerAngles = Vector3.Lerp(scale.transform.eulerAngles, new Vector3(0, 0, 360 - 90 * teddyNumber),timer);
            timer += Time.deltaTime;
            if (timer > 1)
            {
                changeScale = false;
            }
        }
        if(teddyNumber == 4)
        {
            nextCollider.SetActive(false);
        }
        else
        {
            nextCollider.SetActive(true);
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TeddyBear"))
        {
            teddyNumber++;
            changeScale = true;
            timer = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TeddyBear"))
        {
            teddyNumber--;
            changeScale = true;
            timer = 0;
        }
    }
}
