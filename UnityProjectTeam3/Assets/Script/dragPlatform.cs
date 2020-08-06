using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragPlatform : MonoBehaviour
{
    Transform cachedTransform;
    Vector2 startingPos;
    float moveSpeed = 0.03f;

    void Start()
    {
        cachedTransform = transform;
        startingPos = cachedTransform.position;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            float dist = transform.position.y - Camera.main.transform.position.y;
            float leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x + gameObject.GetComponent<Renderer>().bounds.size.x * 0.5f;
            float rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x - gameObject.GetComponent<Renderer>().bounds.size.x * 0.5f;

            Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;
            cachedTransform.position = new Vector2(Mathf.Clamp((deltaPosition.x * moveSpeed) + cachedTransform.position.x, leftLimitation, rightLimitation), startingPos.y);
        }
    }
}
