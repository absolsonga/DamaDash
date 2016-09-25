using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Target : MonoBehaviour
{
	void Start()
	{
        // 타겟을 좌우로 반복 이동한다. 시간을 랜덤하게 해서 서로 다른 속도로 설정한다.
        transform.DOMoveX(transform.position.x + Random.Range(4f, 6f), Random.Range(1f, 2f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
