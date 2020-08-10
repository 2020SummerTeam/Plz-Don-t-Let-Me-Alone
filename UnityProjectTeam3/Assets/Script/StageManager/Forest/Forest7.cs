﻿using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Forest7 : MonoBehaviour
{
    int fingerCount = 0;
    private Vector3 scaleChange;

    private void Awake()
    {
        scaleChange = new Vector3(0f, -2f, 0f);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerCount++;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                if (fingerCount < 6)
                {
                    hit.collider.transform.localScale += scaleChange;
                }
            }
        }

        //if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //{
        //    fingerCount++;
        //    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        //    RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        //    if (hit.collider != null && hit.collider.CompareTag("Wall"))
        //    {
        //        if (fingerCount < 6)
        //        {
        //            hit.collider.transform.localScale += scaleChange;
        //        }
        //    }
        //}
    }
}
