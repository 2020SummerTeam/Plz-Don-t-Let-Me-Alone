using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Forest7 : MonoBehaviour
{
    int i = 0;
    private Vector3 scaleChange;

    private void Awake()
    {
        scaleChange = new Vector3(0f, -2f, 0f);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            i++;
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                if (i < 6)
                {
                    hit.collider.transform.localScale += scaleChange;
                }
            }
        }
    }
}
