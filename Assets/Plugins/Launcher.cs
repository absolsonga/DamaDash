using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Launcher : MonoBehaviour
{
    public GameObject Missile; // 미사일 프리펩

	void Start()
	{
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly); // DotTween 초기화
        transform.DOMoveX(5f, 4f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear); // 런쳐를 좌우로 반복 이동시킨다.
    }

    public void Shoot()
    {
        // 한번 쏠때마다 유도탄을 4개씩 생성한다.
        Instantiate(Missile, transform.GetChild(0).position, Quaternion.Euler(0f, 0f, 70f));
        Instantiate(Missile, transform.GetChild(0).position, Quaternion.Euler(0f, 0f, -70f));

        Instantiate(Missile, transform.GetChild(0).position, Quaternion.Euler(0f, 0f, 40f));
        Instantiate(Missile, transform.GetChild(0).position, Quaternion.Euler(0f, 0f, -40f));
    }
}
