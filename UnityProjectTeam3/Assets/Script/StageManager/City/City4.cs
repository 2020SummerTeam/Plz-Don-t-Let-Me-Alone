using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City4 : MonoBehaviour
{
   
    public GameObject lightbulb;
    public GameObject teddybear;
    public Transform tTr;
    public GameObject shadow;

    private float timer = 0.0f;
    public bool isShadow;
    public GameObject findsign;
    public GameObject researchers;
    
    public GameObject button;
 
    public GameObject platform;
    public GameObject parents;

    void Start()
    {
        findsign.SetActive(false);
        shadow.SetActive(false);
        tTr = teddybear.GetComponent<Transform>();
    }

    
    void Update()
    {
        

        if (lightbulb.transform.eulerAngles.z <= 220)
        {
            if (tTr.position.x >= -3 && tTr.position.x <= -2.15)
            {
                if (tTr.position.y >= 0.3 && tTr.position.y <= 0.7)
                {
                    teddybear.GetComponent<TeddyBear>().enabled = false;
                    shadow.SetActive(true);
                }
                isShadow = true;
            }
            else
                shadow.SetActive(false);
        }
           
 
        
        if (isShadow)
        {
            timer += Time.deltaTime;
            if(timer >= 0.5)
            {
                findsign.SetActive(true);
                if(timer >= 2.5)
                {
                    findsign.SetActive(false);
                    researchers.transform.rotation = Quaternion.Euler(0, 0, 0);
                    researchers.transform.position += new Vector3(Time.deltaTime, 0, 0);
                }
            }
        }

        if(researchers.transform.position.x >= 8.05)
        {
            button.SetActive(false);
            platform.SetActive(false);
        }
    }
}
