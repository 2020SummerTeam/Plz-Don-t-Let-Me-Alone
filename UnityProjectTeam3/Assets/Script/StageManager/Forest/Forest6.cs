using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest6 : MonoBehaviour
{
    GameObject mPlayer;
    GameObject mBox;
    [SerializeField]
    Vector3 mPosition; //Player를 반대편으로 이동시킬 위치
    public Vector3 origidPosition;

    // Start is called before the first frame update
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mBox = GameObject.FindGameObjectWithTag("InteractObj");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(mPlayer.transform.position.x < -9.3f)
        {
            mPlayer.transform.position = mPosition;
        }
        if (mPlayer.transform.position.x > 9.3f)
        {
            mPlayer.transform.position = origidPosition;
        }

    }
}
