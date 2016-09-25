using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    Transform TargetParent; // 하이어라키의 Targets 오브젝트
    Transform Target; // 유도탄이 따라갈 목표 오브젝트

    public GameObject Explosion; // 유도탄이 터질때 이펙트 프리펩

    public float MoveSpeed = 5f; // 이동 스피드
    public float RotateSpeed = 1f; // 회전 스피드 (가급적 이동 스피드랑 같거나 큰 수치로 한다)
    public float Tracking = 0.5f; // 트래킹 간격 (0.5초마다 타겟 방향으로 회전한다)
    float time = Time.time; // 타이머


    void Start()
	{
        TargetParent = GameObject.Find("Targets").transform;

        // 타겟을 랜덤하게 설정한다.
        Target = TargetParent.GetChild(Random.Range(0, TargetParent.childCount));
        SetNearbyTarget(); // 가장 가까운 타겟을 찾아준다.
    }

    void SetNearbyTarget() // 가장 가까운 거리에 있는 타겟 찾기
    {
        for (int i = 0; i < TargetParent.childCount; i++)
        {
            float dist = Vector2.Distance(transform.position, TargetParent.GetChild(i).position);

            if (dist < Vector2.Distance(transform.position, Target.position))
            {
                Target = TargetParent.GetChild(i);
            }
        }
    }

    void Update()
	{
        // 정면 방향으로 이동시킨다. 2D이기 때문에 Vector2.up을 사용했다.
        // 3D에선 Vector3.foward로 바꾸면 된다.
        transform.Translate(Vector2.up * MoveSpeed * Time.deltaTime);

        if (Time.time - time >= Tracking) // 트래킹할 시간이 됐는지 체크한다.
        {
            Vector2 dir = Target.position - transform.position; // 유도탄과 타겟 사이의 벡터값 구하기
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90; // 2D 각도값 구하기
            Quaternion tarRot = Quaternion.AngleAxis(angle, Vector3.forward); // 얻어진 2D 좌표계 각도를 Quaternion으로 변환한다.
            transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, RotateSpeed * Time.deltaTime); // 목표 각도로 서서히 이동시킨다.

            if (transform.rotation == tarRot) time = Time.time; // 목표각도까지 회전했으면 타이머를 리셋한다.
        }
    }

    void OnTriggerEnter2D(Collider col) // 유도탄의 BoxCollider가 다른 오브젝트와 충돌한 경우
    {
        Debug.Log(col.gameObject.name);
        /*
            // 폭발 이펙트 파티클 생성
            GameObject temp = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            Destroy(temp, 1f); // 파티클을 1초 후에 파괴한다.

            Destroy(gameObject); // 유도탄을 파괴한다.
         * */
        }
    }
