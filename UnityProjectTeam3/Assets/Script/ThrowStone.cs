using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowStone : MonoBehaviour
{
    public Transform Target;    // stage 각각의 player로 따로따로 입력해줘야 합니다
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public Transform Projectile;    // 발사체. stone
    private Transform mTr;

    void Awake()
    {
        mTr = transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Throw();
        }
    }

    public void Throw() // KidsCtrl에서 참조. 돌을 던져야 할 경우에 Throw를 실행함
    {
        StartCoroutine(SimulateProjectile());
    }


    IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(0.5f);  // 투사체 던지기 전에 기다림

        mTr.transform.position += new Vector3(0, 0, 10);    // 숨겨놨다가 앞으로 당겨옴

        float target_Distance = Vector3.Distance(Projectile.position, Target.position); // taget(player)와의 거리 계산
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);   // 각도(45도)에서 물체를 던지는데 필요한 속도 계산

        // 속도의 X, Y 추출
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);
        float flightDuration = target_Distance / Vx;    // 날아가는 시간 계산

        // taget까지 발사체 회전
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)    // 포물선 운동
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }

        Destroy(gameObject);  // 닿으면 파괴 // Game Over
    }


}
