using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*20200808 sanghun
 * 베어스크립트는 베어프리팹 자체에 넣는다.
 * 어느것과 부딪혔는지 체크하기위해서 OnCollision을 받아야되는데
 * , 그거는 다른스크립트에서 받을수가 없으니까 그렇다. 
 * 그래서 여기서는 퍼블릭으로 불변수들을 넣어두고,
 * 스테이지매니저에서만 받아가면 된다.
 */
public class BearScript : MonoBehaviour
{
    public bool isPlayerCol;//colliding player -> true
    public bool isSmallBoxCol; //collidingBox -> true 
    public bool isBeeCol;      //on forest4 there are bees // hey bee~~

    public Animator mAnim;

    // Start is called before the first frame update
    void Start()
    {
        //initialize
        isPlayerCol = false;
        isSmallBoxCol = false;
        isBeeCol = false;
        mAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        //부딪히면 머랑부딪혔는지 알려준다. 
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerCol = true;
            mAnim.SetTrigger("Attack");
        }
        if (collision.gameObject.CompareTag("InteractObj"))
        {
            isSmallBoxCol = true;
        }
        if (collision.gameObject.CompareTag("Bee"))
        {
            isBeeCol = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerCol = false;
        }
    }
}
