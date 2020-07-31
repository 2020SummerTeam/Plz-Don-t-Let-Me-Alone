using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//made in 202000730 by sanghun
//used in stage 2.
public class TreeScript : MonoBehaviour
{
    //have to know about position of player
    public Transform playerTransform;

    //to make it fall. change gravity scale 0 to 1
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 0;
        rigidBody.freezeRotation = true;
       
        //we use coroutine when we check position
        //you can question me if you want to know more about coroutine
        StartCoroutine(CheckCoroutine());
    }

    IEnumerator CheckCoroutine()
    {
        while(transform.position.x > playerTransform.position.x)
        {
            yield return new WaitForSeconds(0.5f);
        }
        rigidBody.gravityScale = 1;
    }
}
