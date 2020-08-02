using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneZone : MonoBehaviour
{
    public GameObject player;
    public bool isThrowPossible;    // 던져도 되는 조건을 만족하는지 다른 스크립트에서 bool 값을 바꿔줄 예정
                                    // 예를 들어, stage3에서 박스 뒤에 숨었는지, 차고가 열렸는지


    private void OnTriggerEnter2D(Collider2D collision) // player가 StoneZone에 들어갈 경우
    {
        if (collision.gameObject == player && isThrowPossible) // 테스트용. 조건은 스테이지마다 따로 추가.
        {
            GameObject.Find("Stone").GetComponent<ThrowStone>().Throw();    // 돌 던짐
            Destroy(this);  // 스크립트 파괴
        }
    }

}
