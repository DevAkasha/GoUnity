using System.Collections.Generic;

//소재철 튜터님 프레임워크 스크립트
public class PoolManager : Manager<PoolManager>
{
    Dictionary<string, Queue<PoolBase>> pools = new Dictionary<string, Queue<PoolBase>>();

    public T Spawn<T>(string id, PoolBase prefab) where T : PoolBase
    {
        if (pools[id].Count == 0)
        {
            for (int i = 0; i < 25; i++)
            {
                var obj = Instantiate(prefab, transform);
                pools[id].Enqueue(obj);
            }
        }
        var retObj = (T)pools[id].Dequeue();
        retObj.gameObject.SetActive(true);
        return retObj;
    }

    public void Release(PoolBase item)
    {
        item.gameObject.SetActive(false);
        item.transform.parent = transform;
        pools[item.name].Enqueue(item);
    }
}
