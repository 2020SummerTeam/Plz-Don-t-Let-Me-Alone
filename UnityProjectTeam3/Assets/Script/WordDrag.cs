using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class WordDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Transform cachedTransform;
    Vector2 startingPos;

    void Start()
    {
        cachedTransform = transform;
        startingPos = cachedTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}


