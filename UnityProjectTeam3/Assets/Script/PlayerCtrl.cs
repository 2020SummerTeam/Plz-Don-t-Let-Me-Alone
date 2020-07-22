using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private Rigidbody2D mRB;
    private Animator mAnim;
    [SerializeField]
    private float mSpeed; 

    // Start is called before the first frame update
    void Start()
    {
        mRB = GetComponent<Rigidbody2D>();
        mAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        mRB.velocity = new Vector2(horizontal * mSpeed, mRB.velocity.y);

        if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            mAnim.SetBool(AnimHash.RUN, true);
        }
        else if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            mAnim.SetBool(AnimHash.RUN, true);
        }
        else
        {
            mAnim.SetBool(AnimHash.RUN, false);
        }

    }

}
