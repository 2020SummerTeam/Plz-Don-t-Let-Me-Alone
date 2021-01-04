using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyBear : MonoBehaviour
{
    public GameObject player;//draw character
    public AudioSource audioSource;
    IEnumerator OnMouseDown() // 단어 드래그 드랍
    {
        Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z));


        while (Input.GetMouseButton(0))
        {
            player.GetComponent<PlayerCtrl>().enabled = false; //단어를 터치한 상태에는 PlayerCtrl 스크립트 비활성화
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
            Vector3 worldpos = curPosition;

            if (worldpos.x < -6f)  // 플랫폼 이동 범위 제한
                worldpos.x = -6f;
            if (worldpos.y < -4f)
                worldpos.y = -4f;
            if (worldpos.x > 8f)
                worldpos.x = 8f;
            if (worldpos.y > 4f)
                worldpos.y = 4f;

            transform.position = worldpos;
            yield return null;
        }
        player.GetComponent<PlayerCtrl>().enabled = true; //단어를 터치하지 않을때는 스크립트 활성화
    }
    IEnumerator OnMouseUp()
    {
        audioSource.Play();
        yield return null;
    }
}
