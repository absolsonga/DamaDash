using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    public GameObject Explosion; // 유도탄이 터질때 이펙트 프리펩
    public GameObject target;

    public float MoveSpeed = 3f; // 이동 스피드
    public float RotateSpeed = 3f; // 회전 스피드 (가급적 이동 스피드랑 같거나 큰 수치로 한다)
    public float Tracking = 0.5f; // 트래킹 간격 (0.5초마다 타겟 방향으로 회전한다)
    float time = Time.time; // 타이머

    void Awake()
    {

    }

    void Update()
	{
        
        // 정면 방향으로 이동시킨다. 2D이기 때문에 Vector2.up을 사용했다.
        // 3D에선 Vector3.foward로 바꾸면 된다.
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);

        if (Time.time - time >= Tracking) // 트래킹할 시간이 됐는지 체크한다.
        {
            Vector2 dir = target.GetComponent<Transform>().position - transform.position; // 유도탄과 타겟 사이의 벡터값 구하기
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90; // 2D 각도값 구하기
            Quaternion tarRot = Quaternion.AngleAxis(angle, Vector3.forward); // 얻어진 2D 좌표계 각도를 Quaternion으로 변환한다.
            transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, RotateSpeed * Time.deltaTime); // 목표 각도로 서서히 이동시킨다.

            if (transform.rotation == tarRot) time = Time.time; // 목표각도까지 회전했으면 타이머를 리셋한다.
        }
    }

    void OnTriggerEnter2D(Collider2D col) // 유도탄의 BoxCollider가 다른 오브젝트와 충돌한 경우
    {
        if(col.name == "Monster")
        {
        
            // 폭발 이펙트 파티클 생성
            GameObject temp = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            Destroy(temp, 1f); // 파티클을 1초 후에 파괴한다.

            Destroy(gameObject); // 유도탄을 파괴한다.
        }
         
        }
    }
