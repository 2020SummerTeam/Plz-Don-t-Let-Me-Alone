using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    float timer;
    float waitingTime;
    public GameObject Target;
    void Start()
    {
        timer = 0.0f;
        waitingTime = 5f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= waitingTime)
        {
            Target.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
            Target.GetComponent<SpriteRenderer>().enabled = true;
    }
}
