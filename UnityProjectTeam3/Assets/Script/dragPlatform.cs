using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dragPlatform : MonoBehaviour
{
    // 플랫폼 상하 드래그
    IEnumerator OnMouseDown()
    {
        Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(scrSpace.x, Input.mousePosition.y, scrSpace.z));
       
        
        while (Input.GetMouseButton(0))
        {
            Vector3 curScreenSpace = new Vector3(scrSpace.x, Input.mousePosition.y, scrSpace.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            Vector3 worldpos = curPosition;
           
            if (worldpos.y < -0.5f)  // 플랫폼 이동 범위 제한
                worldpos.y = -0.5f;
            if (worldpos.y > 1.5f)
                worldpos.y = 1.5f;
            
            transform.position = worldpos;
            yield return null;
        }
    }
}



