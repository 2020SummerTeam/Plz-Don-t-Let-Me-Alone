using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public GameObject mPlayer;
    public float mSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        Vector2 position = this.transform.position;
        position.x = Mathf.Lerp(this.transform.position.x, mPlayer.transform.position.x, mSpeed * Time.deltaTime);
        this.transform.position = position;
    }
}
