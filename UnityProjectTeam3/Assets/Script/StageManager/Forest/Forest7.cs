using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Forest7 : MonoBehaviour
{
    int fingerCount = 0;
    private Vector3 scaleChange;
    public GameObject playerObject;
    public PlayerCtrl player;
    public AudioSource audioSource;
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    private void Awake()
    {
        scaleChange = new Vector3(0f, -2.2f, 0f);
    }
    void Update()
    {
        //마우스가 클릭한 위치가 wall 이라면 wall의 y 값을 줄여준다
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                fingerCount++;
                if (fingerCount < 5)
                {
                    spriteRenderer.sprite = sprites[fingerCount];
                    audioSource.Play();
                    boxCollider.offset = boxCollider.offset - new Vector2(0, 0.85f);
                    //hit.collider.transform.localScale += scaleChange;
                }

            }
        }
        if (playerObject.transform.position.x < -9)
        {
            player.OnStageFail();
        }

        //touch script
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
