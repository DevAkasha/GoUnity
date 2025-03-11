using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//소재철 튜터님 프레임워크 스크립트
public class CanvasBase<T> : Manager<T> where T : CanvasBase<T>
{
    [SerializeField] protected List<Transform> parents;
    private IEnumerator Start() //이게 되네
    {
        yield return new WaitUntil(() => UIManager.IsInstance); //UImanager인스탄스까지 대기.싱글톤 패턴에 Laze<T> 이용하면 개선될지도.
        if (parents.Count > 0)
            UIManager.SetParents(parents);
    }
}