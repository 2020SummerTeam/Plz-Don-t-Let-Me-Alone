using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City5 : MonoBehaviour
{
    public GameObject mCar;
    public GameObject mPlayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (mPlayer.transform.position.x > 42.5f)
        {
            mCar.transform.Translate(-2f, 0f, 0f);
        }
    }
}

