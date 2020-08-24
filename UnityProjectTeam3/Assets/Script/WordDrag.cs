using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WordDrag : MonoBehaviour
{ 
    IEnumerator OnMouseDown() // 단어 드래그 드랍
    {
        Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z));


        while (Input.GetMouseButton(0))
        {
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            Vector3 worldpos = curPosition;

            if (worldpos.x < -5f)  // 플랫폼 이동 범위 제한
                worldpos.x = -5f;
            if (worldpos.y < -4f)  
                worldpos.y = -4f;
            if (worldpos.x > 0f)
                worldpos.x = 0f;
            if (worldpos.y > 4f)
                worldpos.y = 4f;

            transform.position = worldpos;
            yield return null;
        }
    }
}


