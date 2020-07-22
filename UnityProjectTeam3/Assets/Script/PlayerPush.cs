using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float distance = 1f;
    public LayerMask boxMask;
    GameObject InteractObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);

        if (hit.collider != null && hit.collider.gameObject.tag=="InteractObj" &&Input.GetKeyDown(KeyCode.Z))
        {
            InteractObj = hit.collider.gameObject;

            InteractObj.GetComponent<FixedJoint2D>().enabled = true;
            InteractObj.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            InteractObj.GetComponent<FixedJoint2D>().enabled = false;
        }

    }
    private void OnDrawGizmos()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Gizmos.color = Color.yellow;
     
        if (horizontal <= 0)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + -Vector2.right * transform.localScale.x * distance);
        }
        else if (horizontal > 0)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
        }
    }
}
