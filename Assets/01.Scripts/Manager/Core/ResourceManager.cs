using System.Collections.Generic;
using UnityEngine;

//소재철 튜터님 프레임워크 스크립트
public class ResourceManager : Manager<ResourceManager>
{
    public Dictionary<string, Object> pool = new Dictionary<string, Object>();

    public T LoadAsset<T>(string key) where T : UnityEngine.Object
    {
        if (pool.TryGetValue(key, out var obj))
        {
            return (T)obj;
        }
        else
        {
            var asset = Resources.Load<T>(key);
            if (asset != null)
                pool.Add(key, asset);
            return asset;
        }
    }

    public T InstantiateAsset<T>(string key, Transform parent = null) where T : Object
    {
        var asset = Instantiate(LoadAsset<T>(key), parent);
        return asset;
    }

    public T LoadAsset<T>() where T : MonoBehaviour
    {
        var key = typeof(T).ToString();
        if (pool.TryGetValue(key, out var mono))
        {
            return (T)mono;
        }
        else
        {
            var asset = Resources.Load<T>(key);
            if (asset != null)
                pool.Add(key, asset);
            return asset;
        }
    }

    public T InstantiateAsset<T>(Transform parent = null) where T : MonoBehaviour
    {
        var asset = Instantiate(LoadAsset<T>(), parent);
        return asset;
    }
}